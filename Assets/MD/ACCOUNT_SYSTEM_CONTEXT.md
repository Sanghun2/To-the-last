# Account System — 개발 컨텍스트 핸드오프

## 프로젝트 개요
Unity 모바일 게임의 계정 로그인 시스템. Google / Apple / Custom(이메일) / Guest 방식을 지원하는 구조를 설계 완료, 현재 SDK 연동 및 내부 로직 구현 단계.

---

## 확정된 아키텍처

### 설계 원칙
- `AccountManager`는 `AccountUI`를 **절대 참조하지 않음** (단방향 의존)
- UI → Manager: 직접 메서드 호출
- Manager → UI: `event`로만 통신
- 로그인 방식은 `IAccountMethod` 인터페이스로 추상화 (교체/추가 가능)
- 저장 로직은 `IAccountRepository`로 분리 (PlayerPrefs ↔ 서버 교체 가능)

### 클래스 구조
```
AccountManager          — 로그인 흐름 총괄, State/Process enum, event 발행
IAccountMethod          — 로그인 방식 추상화
  └ GoogleAccountMethod
  └ AppleAccountMethod   (#if UNITY_IOS 에서만 등록)
  └ CustomAccountMethod  (이메일/패스워드)
  └ GuestAccountMethod
AccountResult           — 성공/실패 결과 래퍼
UserData                — 유저 정보 DTO
IAccountRepository      — 저장/로드 추상화
  └ PlayerPrefsAccountRepository
AccountUI               — Manager 이벤트 구독, 로딩 UI 제어
AccountButton           — Init(method, manager), 버튼 1개 = 로그인 방식 1개
AccountButtonContainer  — IPool 기반 버튼 풀
```

---

## 확정 코드 전문

### IAccountMethod
```csharp
public interface IAccountMethod
{
    string MethodId  { get; }   // "google" | "apple" | "custom" | "guest"
    Sprite Icon      { get; }   // 버튼 아이콘
    bool IsAvailable { get; }   // 플랫폼 지원 여부
    bool IsSignedIn  { get; }   // 이미 로그인된 계정 있는지

    Task<AccountResult> SignInAsync();
    Task<AccountResult> SignUpAsync();
}
```

### AccountResult / UserData
```csharp
public class AccountResult
{
    public bool   IsSuccess    { get; private set; }
    public string ErrorMessage { get; private set; }
    public UserData User       { get; private set; }

    public static AccountResult Success(UserData user) =>
        new AccountResult { IsSuccess = true, User = user };

    public static AccountResult Failure(string error) =>
        new AccountResult { IsSuccess = false, ErrorMessage = error };
}

public class UserData
{
    public string UserId      { get; set; }
    public string DisplayName { get; set; }
    public string Email       { get; set; }
    public string Token       { get; set; }
}
```

### IAccountRepository
```csharp
public interface IAccountRepository
{
    void     SaveUser(UserData user);
    UserData LoadUser();
    void     ClearUser();
    bool     HasSavedUser();
}

public class PlayerPrefsAccountRepository : IAccountRepository
{
    private const string KEY = "account_user";

    public void SaveUser(UserData user)
    {
        PlayerPrefs.SetString(KEY, JsonUtility.ToJson(user));
        PlayerPrefs.Save();
    }
    public UserData LoadUser()
    {
        if (!PlayerPrefs.HasKey(KEY)) return null;
        return JsonUtility.FromJson<UserData>(PlayerPrefs.GetString(KEY));
    }
    public void ClearUser()      => PlayerPrefs.DeleteKey(KEY);
    public bool HasSavedUser()   => PlayerPrefs.HasKey(KEY);
}
```

### AccountManager
```csharp
public class AccountManager : MonoBehaviour, IInitializable
{
    public enum State   { Wait, Processing }
    public enum Process { None, SignIn, SignUp, Done }

    public event Action<State>   OnStateChanged;
    public event Action<Process> OnProcessChanged;
    public event Action<UserData> OnSignedIn;
    public event Action          OnSignedOut;

    public IReadOnlyList<IAccountMethod> Methods => _methods;
    public UserData  CurrentUser  { get; private set; }
    public bool      IsInit       { get; private set; }

    private readonly List<IAccountMethod> _methods = new();
    private IAccountRepository _repository;

    public void Init()
    {
        if (IsInit) return;
        _repository = new PlayerPrefsAccountRepository();

        RegisterMethod(new CustomAccountMethod());
        RegisterMethod(new GoogleAccountMethod());
        RegisterMethod(new GuestAccountMethod());
#if UNITY_IOS
        RegisterMethod(new AppleAccountMethod());
#endif
        if (_repository.HasSavedUser())
            _ = TryAutoLoginAsync();

        IsInit = true;
    }

    public void Release()
    {
        _methods.Clear();
        IsInit = false;
    }

    public async Task<AccountResult> SignInOrUpAsync(string methodId)
    {
        var method = GetMethod(methodId);
        if (method == null) return AccountResult.Failure($"Method not found: {methodId}");

        return method.IsSignedIn
            ? await SignInAsync(methodId)
            : await SignUpAsync(methodId);
    }

    public async Task<AccountResult> SignInAsync(string methodId)
    {
        var method = GetMethod(methodId);
        if (method == null) return AccountResult.Failure($"Method not found: {methodId}");

        SetState(State.Processing);
        SetProcess(Process.SignIn);

        var result = await method.SignInAsync();

        if (result.IsSuccess)
        {
            CurrentUser = result.User;
            _repository.SaveUser(result.User);
            SetProcess(Process.Done);
            OnSignedIn?.Invoke(CurrentUser);
        }
        SetState(State.Wait);
        return result;
    }

    public async Task<AccountResult> SignUpAsync(string methodId)
    {
        var method = GetMethod(methodId);
        if (method == null) return AccountResult.Failure($"Method not found: {methodId}");

        SetState(State.Processing);
        SetProcess(Process.SignUp);

        var result = await method.SignUpAsync();

        if (result.IsSuccess)
        {
            CurrentUser = result.User;
            _repository.SaveUser(result.User);
            SetProcess(Process.Done);
            OnSignedIn?.Invoke(CurrentUser);
        }
        SetState(State.Wait);
        return result;
    }

    public void SignOut()
    {
        CurrentUser = null;
        _repository.ClearUser();
        SetProcess(Process.None);
        OnSignedOut?.Invoke();
    }

    private async Task TryAutoLoginAsync()
    {
        // TODO: 저장된 토큰 유효성 서버 검증 추가 필요
        var saved = _repository.LoadUser();
        if (saved == null) return;
        CurrentUser = saved;
        SetState(State.Wait);
        OnSignedIn?.Invoke(CurrentUser);
    }

    private void RegisterMethod(IAccountMethod method)
    {
        if (method.IsAvailable) _methods.Add(method);
    }

    private IAccountMethod GetMethod(string methodId) =>
        _methods.Find(m => m.MethodId == methodId);

    private void SetState(State s)   { OnStateChanged?.Invoke(s); }
    private void SetProcess(Process p) { OnProcessChanged?.Invoke(p); }
}
```

### AccountUI
```csharp
public class AccountUI : UIBase
{
    [SerializeField] LoadingUIBase          loadingUI;
    [SerializeField] AccountButtonContainer buttonContainer;
    [SerializeField] AccountManager         accountManager; // Inspector 직접 연결

    public override void InitUI()
    {
        if (IsInit) return;
        _isInit = true;

        buttonContainer.Clear();
        foreach (var method in accountManager.Methods)
        {
            var button = buttonContainer.GetObj();
            button.Init(method, accountManager);
        }
    }

    private void OnEnable()
    {
        accountManager.OnProcessChanged += HandleProcessChanged;
        accountManager.OnStateChanged   += HandleStateChanged;
    }

    private void OnDisable()
    {
        accountManager.OnProcessChanged -= HandleProcessChanged;
        accountManager.OnStateChanged   -= HandleStateChanged;
    }

    private void HandleStateChanged(AccountManager.State state)
    {
        switch (state)
        {
            case AccountManager.State.Wait:       loadingUI?.StopLoading();  break;
            case AccountManager.State.Processing: loadingUI?.StartLoading(); break;
        }
    }

    private void HandleProcessChanged(AccountManager.Process process)
    {
        // TODO: Process.Done 시 UI 닫기, 에러 토스트 등 처리 필요
    }
}
```

### AccountButton
```csharp
public class AccountButton : ButtonBase, IPool
{
    public bool IsActive => IsOpened;

    [SerializeField] Image iconImage;

    private IAccountMethod _method;
    private AccountManager _manager;

    public void Init(IAccountMethod method, AccountManager manager)
    {
        if (IsInit) return;
        _isInit  = true;
        _method  = method;
        _manager = manager;
        iconImage.sprite = method.Icon;
    }

    public void Return()   => CloseUI();
    public void Activate() => OpenUI();

    protected override async void ButtonAction()
    {
        await _manager.SignInOrUpAsync(_method.MethodId);
        // 결과는 AccountManager event로 AccountUI가 처리
    }
}
```

---

## 미구현 목록 (TODO)

### 높은 우선순위
- [ ] `GoogleAccountMethod.SignInAsync()` — Google Play Games SDK 또는 Firebase Auth 연동
- [ ] `AppleAccountMethod.SignInAsync()` — Sign in with Apple (Unity 패키지) 연동
- [ ] `CustomAccountMethod` — 이메일/패스워드 입력 UI 연결 및 서버 API 호출
- [ ] `GuestAccountMethod` — 게스트 UUID 발급 및 저장

### 중간 우선순위
- [ ] `TryAutoLoginAsync()` — 저장된 토큰을 서버에 검증하는 로직
- [ ] `HandleProcessChanged(Process.Done)` — 로그인 성공 후 UI 닫기 / 씬 전환
- [ ] 로그인 실패 시 에러 메시지 토스트 표시
- [ ] `AccountButton` 재사용(Pool Return) 타이밍 정리

### 낮은 우선순위
- [ ] `ServerAccountRepository` 구현 (현재는 PlayerPrefs만)
- [ ] 토큰 만료 처리 및 자동 갱신
- [ ] 계정 연동 (Guest → Google 업그레이드 등)

---

## 주의사항 / 설계 결정 기록

| 결정 | 이유 |
|------|------|
| `AccountData` 클래스 삭제 | `IAccountMethod`의 불필요한 래퍼였음. `Icon`, `IsSignedIn`을 인터페이스로 흡수 |
| `AccountManager`에서 `AccountUI` SerializeField 제거 | 순환 참조 방지. Manager는 UI를 몰라야 함 |
| `FindAnyObjectByType` 제거 | 느리고 불안정. Inspector SerializeField로 교체 |
| `OnActionCompleted` event 제거 | 구독자 없는 dead event. Manager의 OnStateChanged로 통합 |
| Apple 로그인 `#if UNITY_IOS` 조건부 등록 | Android에서 AppleMethod가 등록되지 않도록 |
| `async void ButtonAction()` 유지 | Unity 버튼 콜백 특성상 불가피. 내부에서 try-catch 추가 권장 |

using BilliotGames;
using UnityEngine;
using UnityEngine.Rendering;

public class TimeManager : MonoBehaviour, IInitializable
{
    public const int DAY_VALUE = 86400;
    public const int HOUR_VALUE = 3600;
    public const int MINUTE_VALUE = 60;

    private Timer MainTimer
    {
        get
        {
            if (_mainTimer == null) {
                _mainTimer = FindAnyObjectByType<Timer>(FindObjectsInactive.Include);
            }

            return _mainTimer;
        }
    }

    public bool IsInit => isInit;

    public delegate void TimeHandler(int day, int hour, int minute, int deltaMinutes);
    public event TimeHandler OnTimeChanged;
    public event TimeHandler OnDayChanged;

    [SerializeField] Timer _mainTimer;

    [SerializeField] int day;
    [SerializeField] int hour;
    [SerializeField] int minute;
    private long prevSeconds;
    private bool isInit;
    
    public void ChangeTime(float deltaTime) {
        MainTimer.ChangeTime(deltaTime);
    }
    public void PauseTime(bool pause) {
        MainTimer.Pause(pause);
    }
    private void ChangeTime(int day, int hour, int minute) {
        long second = day * DAY_VALUE + hour * HOUR_VALUE + minute * MINUTE_VALUE;
        MainTimer.InitTime(second);
    }
    private void ConvertTime(float currentSeconds, float _) {
        long seconds = (long)currentSeconds; // 정확한 time을 위해 long 타입의 elapsed time system으로 구현하고, 지금 연구과정에 대해 노션 정리
        if (seconds != prevSeconds) {
            int day = (int)(seconds / DAY_VALUE);
            int hour = (int)((seconds % DAY_VALUE) / HOUR_VALUE);
            int minute = (int)((seconds % HOUR_VALUE) / MINUTE_VALUE);
            int deltaMinute = 0;

            if (this.minute != minute) {
                deltaMinute = ((minute+ MINUTE_VALUE) - this.minute) % MINUTE_VALUE;
                OnTimeChanged?.Invoke(day, hour, minute, deltaMinute);
                this.minute = minute;
            }

            if (this.hour != hour) {
                this.hour = hour;
                OnTimeChanged?.Invoke(day, hour, minute, deltaMinute);
            }

            if (this.day != day) {
                OnDayChanged?.Invoke(day, hour, minute, deltaMinute);
                this.day = day;
            }

            prevSeconds = seconds;
        }
    }

    private void Reset() {
        _mainTimer = GetComponentInChildren<Timer>();
    }

    public void Init() {
        MainTimer.OnTimeChanged += ConvertTime;

        // init
        ChangeTime(1, 6, 0);
        OnTimeChanged?.Invoke(day, hour, minute, 0);
        OnDayChanged?.Invoke(day, hour, minute, 0);
        isInit = true;
    }

    public void Release() {

    }
}

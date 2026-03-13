using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public struct FloatingTextContext
{
    public string Text => text;
    public Sprite Icon => icon;
    public Vector2 Position => position;
    public FloatingText.PositionType PositionType => positionType;
    public FloatingText.TextType TextType => textType;
    public Action Callback => callback;


    public FloatingTextContext(string text, Vector2 position, FloatingText.TextType textType, FloatingText.PositionType positionType = FloatingText.PositionType.World, Action callback = null) {
        this.text = text;
        this.icon = null;
        this.position = position;
        this.positionType = positionType;
        this.textType = textType;
        this.callback = callback;
    }

    public FloatingTextContext(string text, Sprite icon, Vector2 position, FloatingText.TextType textType, Action callback = null) {
        this.text = text;
        this.icon = icon;
        this.position = position;
        this.positionType = FloatingText.PositionType.World;
        this.textType = textType;
        this.callback = callback;
    }

    private string text;
    private Sprite icon;
    private Vector2 position;
    private FloatingText.PositionType positionType;
    private FloatingText.TextType textType;
    private Action callback;
}


public class FloatingText : UIBase, IPool
{
    public enum PositionType
    {
        Local,
        World,
    }
    public enum TextType
    {
        Damage,
        Heal,
        Buff,
        DeBuff,
    }

    public bool IsActive => IsOpened;
    protected RectTransform TargetRect
    {
        get
        {
            if (_targetRect == null) {
                InitUI();
                _targetRect = group.GetComponent<RectTransform>();
            }

            return _targetRect;
        }
    }

    [SerializeField] protected TextMeshProUGUI valueText;
    [SerializeField] protected TextAnimatorBase textAnimator;
    private CanvasGroup group;
    private RectTransform _targetRect; 
    
    private Action _cachedOnComplete;
    private Action _pendingCallback;

    public override void InitUI() {
        if (IsInit) return;

        _cachedOnComplete = OnAnimationComplete;
        if (textAnimator == null) textAnimator = GetComponent<TextAnimatorBase>();
        if (group == null) group = GetComponentInChildren<CanvasGroup>();

        _isInit = true;
    }

    private void OnAnimationComplete() {
        Return();
        _pendingCallback?.Invoke();
        _pendingCallback = null;
    }

    #region Pool

    public void Init() {
        InitUI();
    }
    public void Activate() {
        OpenUI();
    }
    public void Return() {
        CloseUI();
    }

    #endregion

    public virtual FloatingText ShowText(in FloatingTextContext context) {
        valueText.text = context.Text;

        if (IsOpened == false) OpenUI();

        SetPosition(context.Position, context.PositionType);

        _pendingCallback = context.Callback;


        var target = new AnimationTarget();
        CreateAnimationTarget(ref target);
        textAnimator.AnimateText(target, _cachedOnComplete);

        return this;
    }

    public FloatingText SetColor(Color color) {
        valueText.color = color;
        return this;
    }

    protected virtual void CreateAnimationTarget(ref AnimationTarget target) {
        target.text = valueText;
        target.group = group;
        target.rect = TargetRect;
    }

    private void Reset() {
        if (valueText == null) {
            valueText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
    private void Awake() {
        InitUI();
    }
    private void SetPosition(Vector2 position, PositionType positionType) {
        switch (positionType) {
            case PositionType.Local:
                TargetRect.anchoredPosition = position;
                break;
            case PositionType.World:
                transform.position = position;
                break;
            default:
                break;
        }
    }
}

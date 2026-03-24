using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TraitUI : UIBase, IPool
{
    public enum State {
        None,
        Selected,
    }
    public bool IsActive => IsOpened;
    public State CurrentState
    {
        get => currentState;
        set
        {
            var prevState = currentState;
            currentState = value;
            if (currentState != prevState) {
                OnStateChanged?.Invoke(currentState);
            }
        }
    }
    public string TraitID => trait.Data.ID;
    public Trait Trait => trait;
    private RectTransform Rect
    {
        get
        {
            if (_rect == null) {
                _rect = GetComponent<RectTransform>();
            }

            return _rect;
        }
    }

    [SerializeField] Image iconImage;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] Button selectButton;
    [SerializeField] Button descripitonButton;
    private State currentState;
    private Trait trait;
    private RectTransform _rect;

    public event Action<Trait> OnDescriptionTouched;
    public event Action<TraitUI> OnSelectTouched;
    public event Action<State> OnStateChanged;
 
    public void InitUI(Trait trait) {
        Init();

        this.trait = trait;
        iconImage.sprite = trait.Data.IconImage;
        nameText.text = trait.Data.DisplayText;
        descripitonButton.onClick.RemoveAllListeners();
        selectButton.onClick.RemoveAllListeners();
        costText.text = trait.Data.Cost.ToString();

        descripitonButton.onClick.AddListener(TouchDescription);
        selectButton.onClick.AddListener(TouchSelect);
        currentState = State.None;
    }
    public void ClearEvents() {
        OnDescriptionTouched = null;
        OnSelectTouched = null;
    }

    public void SetContainer(Transform containerTr, int order=-1) {
        transform.SetParent(containerTr);
        if (order != -1) {
            transform.SetSiblingIndex(order);
        }
    }
    public void SetUISize(float textWidth) {
        Rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, textWidth);
    }

    private void TouchSelect() {
        OnSelectTouched?.Invoke(this);
    }
    private void TouchDescription() {
        if (trait != null) {
            OnDescriptionTouched?.Invoke(trait);
        }
        else {
            Debug.LogError($"<color=red>trait is null</color>");
        }
    }

    #region Pool
    public void Init() {
        if (IsInit) return;

        InitUI();

        _isInit = true;
    }
    public void Activate() {
        OpenUI();
    }
    public void Return() {
        CloseUI();
    }

    #endregion
}

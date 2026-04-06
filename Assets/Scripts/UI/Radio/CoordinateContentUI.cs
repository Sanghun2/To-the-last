using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoordinateContentUI : UIBase, IPool
{
    public bool IsActive => IsOpened;
    public float TargetHz => targetHz;

    [SerializeField] Image locationImage;
    [SerializeField] TextMeshProUGUI locationNameText;
    [SerializeField] float targetHz;

    public void InitContent(string locationName, float targetHz, Sprite iconImage=null) {
        locationNameText.text = locationName;
        this.targetHz = targetHz;
        SetIcon(iconImage);
    }
    public bool TryCheckCoordinate(float confirmedCoordinate) {
        return Mathf.Approximately(confirmedCoordinate, targetHz);
    }


    private void SetIcon(Sprite iconImage) {
        locationImage.sprite = iconImage;
        locationImage.gameObject.SetActive(iconImage != null);
    }

    #region Pool

    public void Activate() {
        OpenUI();
    }
    public void Init() {
        InitUI();
    }
    public void Return() {
        CloseUI();
    }

    #endregion
}

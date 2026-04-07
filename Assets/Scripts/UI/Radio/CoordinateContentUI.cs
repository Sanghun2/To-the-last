using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoordinateContentUI : UIBase, IPool
{
    public string LocationUID => locationUID;
    public bool IsActive => IsOpened;
    public float TargetHz => targetHz;

    [SerializeField] Image locationImage;
    [SerializeField] TextMeshProUGUI locationNameText;
    [SerializeField] TextMeshProUGUI hzText;
    private float targetHz;
    private string locationUID;

    public void InitContent(CoordinateData coordinateData) {
        locationUID = coordinateData.LocationUID;
        locationNameText.text = coordinateData.LocationName;
        SetHz(coordinateData.TargetHz);
        SetIcon(coordinateData.IconImage);
    }

    private void SetHz(float targetHz) {
        this.targetHz = targetHz;
        hzText.SetText("{0} Hz", targetHz);
    }

    public bool TryCompareCoordinate(float confirmedCoordinate) {
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
        targetHz = -1;
    }

    #endregion
}

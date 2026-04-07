using System;
using System.Collections.Generic;
using System.Linq;
using BilliotGames;
using UnityEngine;

public class RadioPopUpUI : StructureUIBase
{
    [SerializeField] HzViewer hzViewer;
    [SerializeField] RotaryDial rotaryDial;
    [SerializeField] CustomButton confirmButton;
    [SerializeField] CoordinateContentUIContainer coordinateContentUIContainer;
    [SerializeField] BatteryUIBase batteryUI;
    [SerializeField] Battery battery = new Battery();
    [SerializeField] Hz hz = new Hz();
    private List<CoordinateData> availableCoordinateList = new List<CoordinateData>();

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();

        coordinateContentUIContainer.Clear();

        hzViewer.InitUI();
        rotaryDial.InitUI();

        confirmButton.Init();
        confirmButton.SetButtonAction(CheckAvailableLocation);

        batteryUI.InitUI();
        battery.Init();

        // init 시 연결하는게 아니라 별도의 설치됐을 때 active 로직 필요
        Managers.Time.OnTimeChanged -= battery.ConsumeValue;
        Managers.Time.OnTimeChanged += battery.ConsumeValue;

        hz.InitHz();

        _isInit = true;
    }

    private void OnEnable() {
        rotaryDial.OnValueChanged -= hz.UpdateHz;
        rotaryDial.OnValueChanged += hz.UpdateHz;

        hz.OnHzChanged -= hzViewer.UpdateHz;
        hz.OnHzChanged += hzViewer.UpdateHz;

        battery.OnValueChanged -= batteryUI.UpdateGaugeUI;
        battery.OnValueChanged += batteryUI.UpdateGaugeUI;

        hzViewer.UpdateHz(hz.CurrentHz);
        batteryUI.UpdateGaugeUI(battery.CurrentValue, battery.MaxValue);
    }
    private void OnDisable() {
        rotaryDial.OnValueChanged -= hz.UpdateHz;
        hz.OnHzChanged -= hzViewer.UpdateHz;
        battery.OnValueChanged -= batteryUI.UpdateGaugeUI;
    }

    public void AddCoordinate(CoordinateData coordinateData) {
        var contentUI = coordinateContentUIContainer.GetObj();
        contentUI.transform.SetAsFirstSibling();
        contentUI.InitContent(coordinateData);
        availableCoordinateList.Add(coordinateData);
    }
    public void RemoveCoordinate(CoordinateData coordinateData) {
        var targetIndex = availableCoordinateList.FindIndex(x => x.LocationUID.Equals(coordinateData.LocationUID));
        var targetData = availableCoordinateList[targetIndex];
        CoordinateContentUI contentUI = coordinateContentUIContainer.FindContent(x => x.LocationUID.Equals(coordinateData.LocationUID));
        contentUI.Return();
        availableCoordinateList.RemoveAt(targetIndex);
    }

    private void CheckAvailableLocation() {
        Managers.Sound.PlaySound(Define.Sound.CLICKED);

        if (battery.IsEmpty) return;

        for (int i = 0; i < availableCoordinateList.Count; i++) {
            CoordinateData coordinateData = availableCoordinateList[i];
            if (coordinateData.IsHzMatched(hz.CurrentHz)) {
                Debug.Log($"<color=cyan>hz matched. hz? {coordinateData.TargetHz} location? ({coordinateData.LocationName}) pos? {coordinateData.AnchoredPosition}</color>");

                // 오픈된 지역 메세지 발행

                Managers.Location.UnlockLocation(coordinateData);
                RemoveCoordinate(coordinateData);
                return;
            }
        }
    }

#if TEST || UNITY_EDITOR
    [ContextMenu("Add Test Location")]
    private void TestLocation() {
        var newCoordinate = Managers.Location.CreateNewLocationCoordinate();
        AddCoordinate(newCoordinate);

        Debug.Log($"hz? {newCoordinate.TargetHz}");
    }
#endif
}


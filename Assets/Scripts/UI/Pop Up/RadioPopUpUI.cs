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
    [SerializeField] Hz hz = new Hz();
    private List<CoordinateData> availableCoordinateList = new List<CoordinateData>();

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();

        coordinateContentUIContainer.Clear();

        hzViewer.InitUI();
        rotaryDial.InitUI();

        confirmButton.Init();
        confirmButton.SetButtonAction(CheckAvailableHz);

        hz.InitHz();

        _isInit = true;
    }

    private void OnEnable() {
        rotaryDial.OnValueChanged -= hz.UpdateHz;
        rotaryDial.OnValueChanged += hz.UpdateHz;

        hz.OnHzChanged -= hzViewer.UpdateHz;
        hz.OnHzChanged += hzViewer.UpdateHz;

        hzViewer.UpdateHz(hz.CurrentHz);
    }
    private void OnDisable() {
        rotaryDial.OnValueChanged -= hz.UpdateHz;
        hz.OnHzChanged -= hzViewer.UpdateHz;
    }

    public void AddCoordinate(CoordinateData coordinateData) {
        availableCoordinateList.Add(coordinateData);
    }
    public void RemoveCoordinate(CoordinateData coordinateData) {
        availableCoordinateList.Remove(coordinateData);
    }

    private void CheckAvailableHz() {
        Managers.Sound.PlaySound(Define.Sound.CLICKED);
        for (int i = 0; i < availableCoordinateList.Count; i++) {
            var coordinate = availableCoordinateList[i];
            if (coordinate.IsHzMatched(hz.CurrentHz)) {
                Debug.Log($"<color=cyan>hz matched. hz? {coordinate.TargetHz} location? ({coordinate.LocationName})</color>");

                // 오픈된 지역 메세지 발행

                Managers.Location.CreateLocation(coordinate);
                return;
            }
        }
    }

#if TEST || UNITY_EDITOR
    [ContextMenu("Add Test Location")]
    private void TestLocation() {
        Managers.SD.TryGetContainer<LocationSD>(out var container);
        var locationSDs = container.SDDict.Values.Where(l => !l.ID.Equals(Define.Tag.BASEMENT)).ToList();
        var targetLocation = locationSDs[UnityEngine.Random.Range(0, locationSDs.Count)];
        var targetHz = Mathf.Round(UnityEngine.Random.Range(80f, 120f) * 10f) / 10f;
        AddCoordinate(new CoordinateData(
            $"{targetLocation.ID}-{Guid.NewGuid()}",
            targetLocation.ID,
            $"성모 {targetLocation.DisplayText}",
            Vector2.zero,
            targetHz
            ));

        Debug.Log($"hz? {targetHz}");
    }
#endif
}

[Serializable]
public class Hz
{
    public float CurrentHz
    {
        get => _currentHz;
        private set
        {
            _currentHz = Mathf.Round(value * 10f) / 10f;
            OnHzChanged?.Invoke(_currentHz);
        }
    }

    [SerializeField] private float hzModifier = 0.05f;
    [SerializeField] private float defaultHz = 100f;

    private float _baseHz;
    private float _currentHz;

    public event Action<float> OnHzChanged;

    public void InitHz() {
        _baseHz = defaultHz;
        CurrentHz = defaultHz;
    }

    public void UpdateHz(float value, float _) {
        CurrentHz = _baseHz + value * hzModifier;
    }

    public float GetHzModifier() => hzModifier;
}
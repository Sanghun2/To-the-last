using System;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class StatGaugeUI : StatUIBase
{
    [Header("[  Options  ]")]
    [SerializeField]
    [Range(0, 6)] int floatPoints;

    [Space]
    [Header("[  Assigns  ]")]
    [SerializeField] protected Image statBackgroundImage;
    [SerializeField] protected Image statFillImage;
    [SerializeField] protected Button infoButton;


    public override void UpdateUI(Value<float> value) {
        if (value.MaxValue == 0) { Debug.LogAssertion($"({statSD.ID}) max value 0. func returned"); return; }
        statFillImage.fillAmount = value.CurrentValue / value.MaxValue;
    }

    private void Reset() {
        if (infoButton == null) {
            infoButton = GetComponentInChildren<Button>();
        }
    }

    public override void InitUI() {
        if (IsInit) return;
        base.InitUI();

        // ui init
        statBackgroundImage.sprite = statSD.Image;
        statFillImage.sprite = statSD.Image;

        // info pop up init
        var popUpData = new InfomationPopUpData(
            statSD.DisplayText,
            statSD.Description,
            new ActionData[] {
                new ActionData("확인", () => {
                    Managers.Player.PlayerData.UnregisterEvent(UpdateSubText, StatType, Define.StatDetail.current);
                    Managers.UI.CloseUI<InfomationPopUpUI>();
                })
            },
            image:statSD.Image
            );

        var infoPopUp = Managers.UI.GetUI<InfomationPopUpUI>();
        var playerData = Managers.Player.PlayerData;
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(() => {
            if (infoPopUp.IsOpened) return;
            infoPopUp.InitPopUp(popUpData);
            infoPopUp.OpenUI();
            playerData.RegisterEvent(UpdateSubText, StatType, Define.StatDetail.current);
            var statValue = playerData.GetStatValue(StatType);
            if (statValue != null) {
                UpdateSubText((Value<float>)statValue);
            }
        });

        _isInit = true;
    }

    public void UpdateSubText(Value<float> value) {
        var infoPopUp = Managers.UI.GetUI<InfomationPopUpUI>();
        string format = floatPoints == 0 ? "N0" : $"N{floatPoints}";
        switch (StatType) {
            case Define.Stat.Hp:
            case Define.Stat.Hunger:
            case Define.Stat.Thirst:
            case Define.Stat.Mental:
                infoPopUp.SetSubText($"{value.CurrentValue.ToString(format)}/{value.MaxValue.ToString(format)}");
                break;
            case Define.Stat.Temperature:
                infoPopUp.SetSubText($"{value.CurrentValue.ToString(format)}℃");
                break;
            default:
                break;
        }
    }
}

using System;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class StatGaugeUI : StatUIBase
{
    [SerializeField] protected Image statBackgroundImage;
    [SerializeField] protected Image statFillImage;
    [SerializeField] protected Button infoButton;

    public override void UpdateUI(Value<float> value) {
        if (value.MaxValue == 0) { Debug.LogAssertion($"max value 0. func returned"); return; }
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
        statBackgroundImage.sprite = statSD.IconImage;
        statFillImage.sprite = statSD.IconImage;

        // info pop up init
        var popUpData = new InfomationPopUpData(
            statSD.DisplayName,
            statSD.Description,
            new ActionData[] {
                new ActionData("확인", () => {
                    Managers.Player.Player.UnregisterEvent(StatType, UpdateSubText);
                    Managers.UI.CloseUI<InfomationPopUpUI>();
                })
            },
            image:statSD.IconImage
            );

        var infoPopUp = Managers.UI.GetUI<InfomationPopUpUI>();
        var player = Managers.Player.Player;
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(() => {
            infoPopUp.OpenUI();
            infoPopUp.InitPopUp(popUpData);
            player.RegisterEvent(StatType, UpdateSubText);
            var statValue = player.GetStatValue(StatType);
            if (statValue != null) {
                UpdateSubText((Value<float>)statValue);
            }
        });

        _isInit = true;
    }

    public void UpdateSubText(Value<float> value) { 
        var infoPopUp = Managers.UI.GetUI<InfomationPopUpUI>();
        switch (StatType) {
            case Define.Stat.Hp:
            case Define.Stat.Hungriness:
            case Define.Stat.Thirst:
            case Define.Stat.Mental:
                infoPopUp.SetSubText($"{value.CurrentValue}/{value.MaxValue}");
                break;
            case Define.Stat.Temperture:
                infoPopUp.SetSubText($"{value.CurrentValue}℃");
                break;
            default:
                break;
        }
    }
}

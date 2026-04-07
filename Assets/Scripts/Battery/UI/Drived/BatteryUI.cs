using System;
using UnityEngine;
using UnityEngine.UI;

public class BatteryUI : BatteryUIBase
{
    [SerializeField] Color fullAmountColor;
    [SerializeField] Color lowAmountColor;
    [SerializeField] Image guageFillAmountImage;
    [SerializeField] CustomButton infoButton;
    private InfomationPopUpData infoPopUpData;

    public override void InitUI() {
        if (IsInit) return;

        base.InitUI();

        infoButton.Init();
        infoButton.SetButtonAction(ShowInfo);
        infoPopUpData = new InfomationPopUpData(
            "배터리",
            "기계 장치를 작동시키기 위한 배터리다. 다 쓰면 새로운 건전지를 구해 교체해야 한다.",
            new ActionData[] {
                new ActionData("확인", () => Managers.UI.CloseUI<InfomationPopUpUI>())
            }
            );

        _isInit = true;
    }

    public override void UpdateGaugeUI(float currentValue, float maxValue) {
        var rate = currentValue / maxValue;
        guageFillAmountImage.fillAmount = rate; 
        guageFillAmountImage.color = Color.Lerp(lowAmountColor, fullAmountColor, rate);
    }

    private void ShowInfo() {
        var infoUI = Managers.UI.GetUI<InfomationPopUpUI>();
        infoUI.InitPopUp(infoPopUpData);
        Managers.UI.OpenUI(infoUI);
    }
}

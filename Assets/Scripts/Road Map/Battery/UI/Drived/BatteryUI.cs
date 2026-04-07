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
        base.InitUI();

        infoButton.SetButtonAction(ShowInfo);
        infoPopUpData = new InfomationPopUpData(
            "배터리",
            "기계 장치를 작동시키기 위한 배터리다.",
            new ActionData[] {
                new ActionData("확인", () => Managers.UI.CloseUI<InfomationPopUpUI>())
            }
            );
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

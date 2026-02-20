using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class StatGaugeUI : StatUIBase
{
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

        var popUpData = new InfomationPopUpData(
            statSD.DisplayName,
            statSD.Description,
            new ActionData[] {
                new ActionData("확인", () => Managers.UI.CloseUI<InfomationPopUpUI>())
            });
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(() => {

            Managers.UI.OpenUI<InfomationPopUpUI>().InitPopUp(popUpData);
        });

        _isInit = true;
    }
}

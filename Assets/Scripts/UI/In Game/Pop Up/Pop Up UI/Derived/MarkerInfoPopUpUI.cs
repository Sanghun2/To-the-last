using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MarkerInfoPopUpUI : PopUpUIBase<MarkerPopUpDataBase>
{
    [SerializeField] protected TextMeshProUGUI progressText;
    [SerializeField] protected AffinityUIBase affinityUI;
    [SerializeField] protected TextMeshProUGUI moveTimeExpectationText;

    public override void InitPopUp(MarkerPopUpDataBase popUpData) {
        base.InitPopUp(popUpData);
        //LocationData locationData = popUpData.Location.Data;

        //int maxProgress = LocationEventList?.Count ?? -1;
        SetProgressUI(popUpData as IProgressContent);
        SetAffinityUI(popUpData as IAffinityContent);
        SetMoveExpectaitonTimeUI(popUpData as IMoveExpectationTimeContent);
        
        //LocationBase destination = popUpData.Location;
        //LocationBase currentLocation = LocationUtility.FindLocation(Managers.Player.PlayerData.CurrentLocationID);
    }

    private void SetAffinityUI(IAffinityContent affinityContent) {
        affinityUI.gameObject.SetActive(affinityContent != null);
        if (affinityContent == null) return;
        affinityUI.UpdateUI(affinityContent.CurrentAffinity, affinityContent.MaxAffinity);
    }

    private void SetMoveExpectaitonTimeUI(IMoveExpectationTimeContent moveExpectationTimeContent) {
        moveTimeExpectationText.gameObject.SetActive(moveExpectationTimeContent != null);
        if (moveExpectationTimeContent == null) return;
        

        LocationBase currentLocation = moveExpectationTimeContent.StartLocation;
        LocationBase destination = moveExpectationTimeContent.EndLocation;
        bool isSamePosition = currentLocation.Equals(destination);
        moveTimeExpectationText.gameObject.SetActive(!isSamePosition);
        if (isSamePosition) return;

        var time = LocationUtility.CalculateDistance(currentLocation.Data.AnchoredPosition, destination.Data.AnchoredPosition).ConvertToTime();
        moveTimeExpectationText.SetText($"{time.hour}시간 {time.minutes}분");
    }

    private void SetProgressUI(IProgressContent progressContent) {
        progressText.gameObject.SetActive(progressContent != null);
        if (progressContent == null) return;

        float currentProgress = progressContent.CurrentProgress;
        float maxProgress = progressContent.MaxProgress;

        progressText.SetText($"진행도 {currentProgress}/{maxProgress}");
    }
}

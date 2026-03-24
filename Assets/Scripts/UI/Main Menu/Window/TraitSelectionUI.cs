using System;
using System.Collections.Generic;
using BilliotGames;
using TMPro;
using UnityEngine;

public class TraitSelectionUI : UIBase
{
    [SerializeField] TraitUIContainer traitListContainer;
    [SerializeField] TraitUIContainer selectListContainer;
    [SerializeField] TraitDescriptionUI descriptionUI;
    [SerializeField] TraitPointView traitPointView;

    [Space]
    [SerializeField] RectSizeGetter regionRectGetter;
    [SerializeField] RectTransform leftRegion;
    [SerializeField] RectTransform rightRegion;

    [Space]
    [SerializeField] RectSizeGetter leftContainerSizeGetter;
    [SerializeField] RectSizeGetter rightContainerSizeGetter;

    private float cachedRegionRectWidth;
    private float cachedLeftContainerRectWidth;

    public override void InitUI() {
        if (IsInit) return;

        traitListContainer.InitUI();
        selectListContainer.InitUI();

        Managers.Trait.OnTraitPointChanged -= UpdateTraitPointText;
        Managers.Trait.OnTraitPointChanged += UpdateTraitPointText;

        CloseUI();

        _isInit = true;
    }
    public void ClearContainers() {
        InitUI();
        ReassignParent(traitListContainer);
        ReassignParent(selectListContainer);
    }

    public void InitTraitList(IReadOnlyList<Trait> traits) {
        InitUI();
        for (int i = 0; i < traits.Count; i++) {
            var trait = traits[i];
            var traitUI = traitListContainer.GetOrCreateObj(i);
            traitUI.ClearEvents();
            traitUI.InitUI(trait);
            traitUI.SetUISize(CalculateTextWidth(cachedLeftContainerRectWidth));
            traitUI.OnDescriptionTouched += descriptionUI.ShowDescription;
            traitUI.OnSelectTouched += ToggleContainer;
        }
    }


    public void UpdateTraitPointText(int point) {
        InitUI();
        traitPointView.SetPointText(point);
    }


    protected override void OnOpen() {
        cachedRegionRectWidth = regionRectGetter.Width;
        float padding = 8;
        ResizeRect(cachedRegionRectWidth - padding);
        cachedLeftContainerRectWidth = leftContainerSizeGetter.Width;
    }

    private void ResizeRect(float targetWidth) {
        leftRegion.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth / 2);
        rightRegion.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth / 2);
    }

    private void ToggleContainer(TraitUI traitUI) {
        switch (traitUI.CurrentState) {
            case TraitUI.State.None:
                traitUI.CurrentState = TraitUI.State.Selected;
                traitUI.SetContainer(selectListContainer.ContainerTr, 0);
                Managers.Trait.SelectTrait(traitUI.Trait);
                Managers.Trait.ChangeTraitPoint(-traitUI.Trait.Data.Cost);
                break;
            case TraitUI.State.Selected:
                traitUI.CurrentState = TraitUI.State.None;
                traitUI.SetContainer(traitListContainer.ContainerTr, CalulateOrder(traitUI));
                Managers.Trait.UnselectTrait(traitUI.Trait);
                Managers.Trait.ChangeTraitPoint(traitUI.Trait.Data.Cost);
                break;
            default:
                break;
        }

    }
    private void ReassignParent(TraitUIContainer container) {
        for (int i = 0; i < traitListContainer.ContentCount; i++) {
            var ui = traitListContainer.GetOrCreateObj(i);
            ui.transform.SetParent(traitListContainer.ContainerTr);
            ui.Return();
        }
    }
    private int CalulateOrder(TraitUI traitUI) {
        var container = traitListContainer.transform;
        var uiCount = container.childCount;

        int targetIndex = 0;
        for (int i = 0; i < uiCount; i++) {
            TraitUI targetUI = container.GetChild(i).GetComponent<TraitUI>();
            if (targetUI.IsActive == false) {
                targetIndex = i;
                break;
            }

            if (traitUI.TraitID.CompareTo(targetUI.TraitID) <= 0) {
                return i;
            }
        }

        return targetIndex;
    }
    private float CalculateTextWidth(float parentWidth) {
        float widthWithOutScrollbar = parentWidth - 15;
        return widthWithOutScrollbar - 27f;
    }
}

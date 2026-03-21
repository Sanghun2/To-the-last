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

    public override void InitUI() {
        if (IsInit) return;

        traitListContainer.InitUI();
        selectListContainer.InitUI();

        Managers.Trait.OnTraitPointChanged -= UpdateTraitPointText;
        Managers.Trait.OnTraitPointChanged += UpdateTraitPointText;

        _isInit = true;
    }
    public void ClearContainers() {
        ReassignParent(traitListContainer);
        ReassignParent(selectListContainer);
    }

    public void InitTraitList(IReadOnlyList<Trait> traits) {
        for (int i = 0; i < traits.Count; i++) {
            var trait = traits[i];
            var traitUI = traitListContainer.GetOrCreateObj(i);
            traitUI.ClearEvents();
            traitUI.InitUI(trait);
            traitUI.OnDescriptionTouched += descriptionUI.ShowDescription;
            traitUI.OnSelectTouched += ToggleContainer;
        }
    }

    public IReadOnlyList<Trait> GetSelectedTraits() {
        var container = selectListContainer.transform;
        var childCount = container.childCount;
        List<Trait> resultTraitList = new List<Trait>(childCount);
        for (int i = 0; i < childCount; i++) {
            TraitUI traitUI = container.GetChild(i).GetComponent<TraitUI>();
            if (traitUI.IsActive == false) break;

            if (traitUI.CurrentState == TraitUI.State.Selected) {
                resultTraitList.Add(traitUI.Trait);
            }
        }

        return resultTraitList;
    }


    public void UpdateTraitPointText(int point) {
        traitPointView.SetPointText(point);
    }


    private void ToggleContainer(TraitUI traitUI) {
        switch (traitUI.CurrentState) {
            case TraitUI.State.None:
                traitUI.CurrentState = TraitUI.State.Selected;
                traitUI.SetContainer(selectListContainer.ContainerTr);
                Managers.Trait.ChangeTraitPoint(-traitUI.Trait.Data.Cost);
                break;
            case TraitUI.State.Selected:
                traitUI.CurrentState = TraitUI.State.None;
                traitUI.SetContainer(traitListContainer.ContainerTr, CalulateOrder(traitUI));
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
}

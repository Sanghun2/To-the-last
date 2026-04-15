using System.Collections.Generic;
using UnityEngine;

public class TraitSelectionProcess : ProcessBase<TraitSelectProcessContext>
{
    public TraitSelectionProcess(ProcessContextBuilder<TraitSelectProcessContext> contextBuilder)
        : base(contextBuilder) {
    }

    public override bool CanComplete() {
        var result = Managers.Trait.RemainTraitPoint >= 0;
        if (!result) { Debug.Log($"특성 결정 불가"); }
        return result;
    }

    protected override void OnCleared() {
        Managers.UI.CloseUI<TraitSelectionUI>();
    }

    protected override void OnComplete() {
        var selectedTrits = Managers.Trait.GetSelectedTraits();
        Managers.Player.PlayerData.SetTraits(selectedTrits);
        Managers.UI.CloseUI<TraitSelectionUI>();
    }

    protected override void OnExecute(TraitSelectProcessContext context) {
        Managers.UI.OpenUI<GameBootStrapUI>();
        Managers.UI.OpenUI<TraitSelectionUI>();

        Managers.Trait.InitTraitDataFromPlayerData();
    }
}

public class TraitSelectProcessContext : ProcessContext
{
    public IReadOnlyList<Trait> SelectedTraits => selectedTraits;

    private IReadOnlyList<Trait> selectedTraits;

    public TraitSelectProcessContext SetSelectedTraits(IReadOnlyList<Trait> selectedTraits) {
        this.selectedTraits = selectedTraits;
        return this;
    }
}

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TraitSelectProcess : Process<TraitSelectProcessContext>
{
    public TraitSelectProcess(ProcessContextBuilder<TraitSelectProcessContext> contextBuilder) : base(contextBuilder) {
    }

    protected override void OnCleared() {
        Managers.UI.CloseUI<TraitSelectionUI>();
    }

    protected override void OnComplete() {
        Managers.UI.CloseUI<TraitSelectionUI>();
    }

    protected override void OnExecuteAsync(TraitSelectProcessContext context) {
        Managers.UI.OpenUI<GameBootStrapUI>();
        Managers.UI.OpenUI<TraitSelectionUI>();
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

using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TraitSelectProcess : Process<TraitSelectProcessContext>
{
    public TraitSelectProcess(ProcessContextBuilder<TraitSelectProcessContext> contextBuilder) : base(contextBuilder) {
    }

    public override UniTask ExecuteProcessAsync(TraitSelectProcessContext context, CancellationToken cancellationToken) {
        throw new System.NotImplementedException();
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

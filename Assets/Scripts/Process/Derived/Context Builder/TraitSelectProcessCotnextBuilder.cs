using UnityEngine;

public class TraitSelectProcessCotnextBuilder : ProcessContextBuilder<TraitSelectProcessContext>
{
    public override TraitSelectProcessContext BuildTypedContext() {
        var context = new TraitSelectProcessContext()
            .SetSelectedTraits(Managers.Trait.GetSelectedTraits());

        return context;
    }
}

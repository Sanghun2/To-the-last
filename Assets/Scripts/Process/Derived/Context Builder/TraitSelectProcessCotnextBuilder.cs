using UnityEngine;

public class TraitSelectProcessCotnextBuilder : ProcessContextBuilder<TraitSelectProcessContext>
{
    public override TraitSelectProcessContext BuildProcessContext() {
        var context = new TraitSelectProcessContext()
            .SetSelectedTraits(Managers.Trait.GetSelectedTraits());

        return context;
    }
}

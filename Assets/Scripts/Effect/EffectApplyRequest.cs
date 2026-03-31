using System.Collections.Generic;
using UnityEngine;

public class EffectApplyRequest : IApplyContext
{
    public Effect Effect => effect;
    public Entity Caster => caster;
    public IReadOnlyList<Entity> Targets => targets;

    private Effect effect;
    private Entity caster;
    private IReadOnlyList<Entity> targets;

    public EffectApplyRequest(Effect effect, Entity caster, IReadOnlyList<Entity> targets=null) {
        this.effect = effect;
        this.caster = caster;
        this.targets = targets;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class BattleContext : IBattleContext
{
    public BattleEntity Caster => caster;
    public IReadOnlyList<BattleEntity> Targets => targets;
    public EffectSD EffectSD => effectSD;

    public BattleContext(EffectSD effectSD, BattleEntity caster, BattleEntity target=null) {
        targets.Clear();
        targets.Add(target);
        this.caster = caster;
        this.effectSD = effectSD;
    }

    private EffectSD effectSD;
    private BattleEntity caster;
    private List<BattleEntity> targets = new List<BattleEntity>();
}

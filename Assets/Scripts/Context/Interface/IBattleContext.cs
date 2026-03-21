using System.Collections.Generic;

public interface IBattleContext : IContext
{
    BattleEntity Caster { get; }
    IReadOnlyList<BattleEntity> Targets { get; }
}

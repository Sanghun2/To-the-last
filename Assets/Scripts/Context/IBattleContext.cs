using System.Collections.Generic;

public interface IBattleContext
{
    BattleEntity Caster { get; }
    List<BattleEntity> Targets { get; }
}

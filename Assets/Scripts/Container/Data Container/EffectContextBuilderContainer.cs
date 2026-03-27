using BillotGames;
using UnityEngine;

public class EffectContextBuilderContainer : TypeRegistry<EffectDataBase, EffectContextBuilderBase>
{
    public EffectContextBuilderContainer() {
        Register<StatModifyEffectData>(new StatModifyEffectContextBuilder());
    }
}

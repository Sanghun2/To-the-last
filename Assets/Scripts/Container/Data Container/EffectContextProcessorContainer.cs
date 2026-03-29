using BilliotGames;
using UnityEngine;

public class EffectContextProcessorContainer : TypeRegistry<EffectContextBase, EffectContextProcessorBase>
{
    public EffectContextProcessorContainer() {
        Register<StatModifyEffectContext>(new StatModifyEffectContextProcessor());
    }
}

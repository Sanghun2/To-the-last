using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class EffectProcessorRegistry : TypeRegistry<IEffect, IEffectHandler>
{
    public readonly BattleEffectHandler DefaultProcessor = new BattleEffectHandler();

    public EffectProcessorRegistry() {
        Register<EffectSD>(new BattleEffectHandler());
    }
}

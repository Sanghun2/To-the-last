using System;
using System.Collections.Generic;
using BillotGames;
using UnityEngine;

public class EffectProcessorRegistry : TypeRegistry<IEffect, IEffectHandler>
{
    public readonly BattleEffectHandler DefaultProcessor = new BattleEffectHandler();

    public EffectProcessorRegistry() {
        Register<EffectSD>(new BattleEffectHandler());
    }
}

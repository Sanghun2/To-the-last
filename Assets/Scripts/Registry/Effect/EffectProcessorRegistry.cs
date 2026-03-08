using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectProcessorRegistry : TypeRegistry<IEffect, EffectProcessor>
{
    public readonly EffectProcessor DefaultProcessor = new EffectSDProcessor();

    public EffectProcessorRegistry() {
        Register<EffectSD>(new EffectSDProcessor());
    }
}

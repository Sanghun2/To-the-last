using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectProcessorRegistry : TypeRegistry<IEffect, EffectProcessor>
{
    public EffectProcessorRegistry() {
        Register<EffectSD>(new EffectSDProcessor());
    }
}

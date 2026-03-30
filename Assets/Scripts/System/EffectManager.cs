using System;
using System.Threading.Tasks;
using BilliotGames;
using Cysharp.Threading.Tasks;
using UnityEngine;

public sealed class EffectManager
{
    private EffectDataParserContainer effectDataParserContainer = new EffectDataParserContainer();
    private EffectContextBuilderContainer effectContextBuilderContainer = new EffectContextBuilderContainer();
    private EffectContextProcessorContainer effectContextProcessorContainer = new EffectContextProcessorContainer();

    public void ApplyEffect(Effect effect) {
        if (!effectDataParserContainer.TryGet(effect.EffectSD, out EffectDataParserBase dataParser)) { LogError($"no ({effect.GetType()}) data parser exist"); return; }
        if (!dataParser.TryParse(effect, out EffectDataBase effectData)) { LogError($"({effect.GetType()}) failed to parse data"); return; }

        ApplyEffect(effectData);
    }

    public void ApplyEffect(EffectDataBase effectData) {
        // Efffect Data -> Effect Context
        if (!effectContextBuilderContainer.TryGet(effectData, out EffectContextBuilderBase contextBuilder)) { LogError($"no ({effectData.GetType()}) context builder exist"); return; }
        if (!contextBuilder.TryBuildContext(effectData, out EffectContextBase effectContext)) { LogError($"({effectData.GetType()}) context build failed"); return; }

        // Effect Context -> Apply
        if (!effectContextProcessorContainer.TryGet(effectContext, out EffectContextProcessorBase contextProcessor)) { LogError($"({effectContext.GetType()}) processor is not exist"); return; }
        contextProcessor.ApplyEffect(effectContext);
    }

    private void LogError(string message) {
        Debug.LogError($"<color=red>{message}</color>");
    }
}

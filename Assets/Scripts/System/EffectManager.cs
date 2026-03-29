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

    public async UniTask ApplyEffect(EffectSD effectSD) {
        if (!effectDataParserContainer.TryGet(effectSD, out EffectDataParserBase dataParser)) { LogError($"no ({effectSD.GetType()}) data parser exist"); return; }
        if (!dataParser.TryParse(effectSD, out EffectDataBase effectData)) { LogError($"({effectSD.GetType()}) failed to parse data"); return; }

        await ApplyEffect(effectData);
    }

    public async UniTask ApplyEffect(EffectDataBase effectData) {
        // Efffect Data -> Effect Context
        if (!effectContextBuilderContainer.TryGet(effectData, out EffectContextBuilderBase contextBuilder)) { LogError($"no ({effectData.GetType()}) context builder exist"); return; }
        if (!contextBuilder.TryBuildContext(effectData, out EffectContextBase effectContext)) { LogError($"({effectData.GetType()}) context build failed"); return; }

        // Effect Context -> Apply
        if (!effectContextProcessorContainer.TryGet(effectContext, out EffectContextProcessorBase contextProcessor)) { LogError($"({effectContext.GetType()}) processor is not exist"); return; }
        await contextProcessor.ApplyEffect(effectContext);
    }

    private void LogError(string message) {
        Debug.LogError($"<color=red>{message}</color>");
    }
}

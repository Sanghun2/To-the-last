using System;
using UnityEngine;

public class SelectActionRegistries
{
    private SelectActionContextGeneratorRegistry _selectActionContexts = new SelectActionContextGeneratorRegistry();
    private SelectActionProcessorRegistry _selectActionProcessors = new SelectActionProcessorRegistry();

    internal bool TryGetContextGenerator(SelectionSD selectionSD, out SelectActionContextGenerator contextGenerator) {
        return _selectActionContexts.TryGet(selectionSD, out contextGenerator);
    }

    internal bool TryGenerateSelectAction(SelectionSD selectionSD, SelectActionContext context, out ActionData selectActionData) {
        return _selectActionProcessors.TryGenerateActionData(selectionSD, context, out selectActionData);
    }
}

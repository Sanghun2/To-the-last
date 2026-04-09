using UnityEngine;

public sealed class SelectionBuildContext
{
    public SelectionDataBase SelectionData => selectionData;
    public SelectionRunnerDataBase SelectionRunnerDataBase => selectionRunnerData;

    private SelectionDataBase selectionData;
    private SelectionRunnerDataBase selectionRunnerData;

    public SelectionBuildContext(SelectionData selectionData, SelectionRunnerDataBase selectionRunnerData) {
        this.selectionData = selectionData;
        this.selectionRunnerData = selectionRunnerData;
    }
}

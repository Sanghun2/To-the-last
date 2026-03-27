using UnityEngine;

public abstract class SelectActionContextBase : ActionContextBase
{
    public SelectionDataBase SelectionData => selectionData;
    public int JobDuration => jobDuration;

    [SerializeField] SelectionDataBase selectionData;
    [SerializeField] int jobDuration;

    public SelectActionContextBase(SelectionDataBase selectionData, int jobDuration) {
        this.selectionData = selectionData;
        this.jobDuration = jobDuration;
    }
}

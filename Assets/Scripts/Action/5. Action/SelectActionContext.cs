using UnityEngine;

public class SelectActionContext : ActionContextBase
{
    public SelectionDataBase SelectionData => selectionData;
    public int JobDuration => jobDuration;

    [SerializeField] SelectionDataBase selectionData;
    [SerializeField] int jobDuration;

    public SelectActionContext(SelectionDataBase selectionData, int jobDuration) {
        this.selectionData = selectionData;
        this.jobDuration = jobDuration;
    }
}

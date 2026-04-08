using UnityEngine;

public abstract class SelectActionContextBase : ActionContextBase
{
    public SelectionRunnerDataBase SelectionRunnerData => selectionRunnerData;
    public int JobDuration => jobDuration;

    [SerializeField] SelectionRunnerDataBase selectionRunnerData;
    [SerializeField] int jobDuration;

    public SelectActionContextBase(SelectionRunnerDataBase selectionRunnerData, int jobDuration) {
        this.selectionRunnerData = selectionRunnerData;
        this.jobDuration = jobDuration;
    }
}

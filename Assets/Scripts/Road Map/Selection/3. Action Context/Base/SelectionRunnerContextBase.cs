using UnityEngine;

public abstract class SelectionRunnerContextBase
{
    public int JobDuration => jobDuration;

    [SerializeField] int jobDuration;

    public SelectionRunnerContextBase(int jobDuration) {
        this.jobDuration = jobDuration;
    }
}

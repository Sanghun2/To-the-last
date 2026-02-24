using UnityEngine;

public class SelectionActionContext : ActionContext
{
    public SelectionSD SelectionSD => selectionSD;
    public int JobDuration => jobDuration;

    [SerializeField] SelectionSD selectionSD;
    [SerializeField] int jobDuration;

    public SelectionActionContext(SelectionSD selectionSD, int jobDuration) {
        this.selectionSD = selectionSD;
        this.jobDuration = jobDuration;
    }   
}
public abstract class SelectionActionFactory : ActionFactory
{
    public override ActionData CreateAction(ActionContext context) {
        return CreateAction((SelectionActionContext)context);
    }
    public abstract ActionData CreateAction(SelectionActionContext context);
}

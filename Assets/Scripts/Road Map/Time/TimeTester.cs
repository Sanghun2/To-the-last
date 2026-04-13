using UnityEngine;

public class TimeTester : MonoBehaviour
{
    [SerializeField] int delayTime;
    [SerializeField] string testText;

    [ContextMenu("Register Job")]
    public void RegisterDelayJob() {
        var job = new Job(delayTime, onComplete:() => {
            Debug.Log($"<color=yellow>delay called. {testText}</color>");
        });
        Managers.Job.RegisterDelayedJob(job);
    }
}

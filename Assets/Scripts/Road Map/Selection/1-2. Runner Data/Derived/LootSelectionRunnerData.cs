using UnityEngine;

public class LootSelectionRunnerData : SelectionRunnerDataBase
{
    public int RequireMinutes { get; }
    private LootSelectionRunnerSD RunnerSD { get; }

    public LootSelectionRunnerData(LootSelectionRunnerSD runnerSD, int requireMinutes) {
        RunnerSD = runnerSD;
        RequireMinutes = requireMinutes;
    }
}

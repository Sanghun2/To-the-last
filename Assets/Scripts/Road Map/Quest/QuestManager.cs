using System.Collections.Generic;

public sealed class QuestManager : IInitializable
{
    private Dictionary<string, Quest> activeQuests = new Dictionary<string, Quest>();
    private HashSet<string> completedQuests = new HashSet<string>();

    public bool IsInit => _isInit;
    private bool _isInit;

    public void Init() {
        if (IsInit) return;

        _isInit = true;
    }
    public void Release() {
        _isInit = false;
    }

    public void PublishQuest(Quest quest) {
        if (activeQuests.TryAdd(quest.ID, quest)) {

        }
    }
    public void UnpublishQuest(string questID) {
        if (activeQuests.TryGetValue(questID, out var targetQuest)) {
            targetQuest.Cancel();
        }

        activeQuests.Remove(questID);
    }

    public bool TryComplete(Quest quest) {
        return TryComplete(quest.ID);
    }
    public bool TryComplete(string questID) {
        if (!activeQuests.TryGetValue(questID, out var quest)) return false;
        if (!completedQuests.Add(questID)) return false;

        activeQuests.Remove(questID); 
        return true;
    }

    private void RegisterCompleteQuest(string questID) {
        completedQuests.Add(questID);
    }
}

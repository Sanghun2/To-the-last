using System;
using BilliotGames;
using UnityEngine;


public class EventHistory
{
    Validator<string> executedEvents = new Validator<string>();

    public event Action<string> OnEventRegistered;

    public void RegisterEvent(string eventID) {
        if (executedEvents.TryAddValue(eventID)) {
            OnEventRegistered?.Invoke(eventID);
        }

        Debug.LogError($"<color=red>({eventID})는 이미 실행된 이벤트</color>");
    }

    public bool IsExecutedEvent(string eventID) {
        return executedEvents.IsValid(eventID);
    }
}

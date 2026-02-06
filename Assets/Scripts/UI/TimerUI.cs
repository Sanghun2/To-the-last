using System;
using BilliotGames;
using TMPro;
using UnityEngine;

public class TimerUI : UIBase, IInitializable
{
    [SerializeField] TextMeshProUGUI dayText;
    [SerializeField] TextMeshProUGUI timeText;

    private int prevDay;

    public void Init() {
        Managers.Time.OnTimeChanged += UpdateTime;
        Managers.Time.OnDayChanged += UpdateDay;
    }

    public void Release() {

    }

    private void UpdateDay(int day, int hour, int minute, int deltaMinutes) {
        dayText.text = $"Day {day}";
    }

    private void UpdateTime(int day, int hour, int minute, int deltaMinutes) {
        timeText.text = $"{hour:00}:{minute:00}";
    }
}

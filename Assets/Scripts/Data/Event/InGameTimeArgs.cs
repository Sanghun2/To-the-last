using UnityEngine;

public readonly struct InGameTimeArgs
{
    public readonly int day;
    public readonly int hour;
    public readonly int minute;

    public InGameTimeArgs(int day, int hour, int minute) {
        this.day = day;
        this.hour = hour;
        this.minute = minute;
    }
}

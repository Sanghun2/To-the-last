using UnityEngine;

public static class TimeUtility
{
    public static int ToMinutes(int day, int hour, int minute) {
        return day * 24 * 60 + hour * 60 + minute;
    }
    public static (int days, int hours, int minutes) FromMinutes(int totalMinutes) {
        int days = totalMinutes / (24 * 60);
        int hours = (totalMinutes % (24 * 60)) / 60;
        int minutes = totalMinutes % 60;
        return (days, hours, minutes);
    }
}

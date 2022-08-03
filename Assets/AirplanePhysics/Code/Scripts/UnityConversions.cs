public static class UnityConversions
{
    public static float PoundsToKilograms(this float pounds)
    {
        return pounds * 0.453592f;
    }

    public static float MetersPerSecondToMilesPeHour(this float MeterPerSecond)
    {
        return MeterPerSecond * 2.23694f;
    }
    
    public static float MilesPerHourToMitersPerSecond(this float MilesPerHour)
    {
        return MilesPerHour * 0.44704f;
    }
}
namespace nicorueda
{
    public static class Time
    {
        public static int hoursToMinutes(int h)
        {
            return h * 60;
        }

        public static int minutesToSeconds(int m)
        {
            return m * 60;
        }

        public static int howManySeconds(int h = 0, int m = 0, int s = 0)
        {
            int hoursInSeconds = hoursToMinutes(h);
            int minutesInSconds = minutesToSeconds(m + hoursInSeconds);
            return s + minutesInSconds;
        }
    }
}
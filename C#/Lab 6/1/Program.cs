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

    public class Date
    {
        int hours;
        int minutes;
        int seconds;


        public Date(int h, int m, int s)
        {
            hours = h; minutes = m; seconds = s;
        }
    }

    class Program
    {
        static int min = 0;
        static int max = 99999999;
        static void Main()
        {
            int hours = Input.readInt("Enter the number of hours: ", min, max);
            int minutes = Input.readInt("Enter the number of minutes: ", min, max);
            int seconds = Input.readInt("Enter the number of seconds: ", min, max);
            int result = Time.howManySeconds(hours, minutes, seconds);
            Console.WriteLine(result);
        }
    }
}

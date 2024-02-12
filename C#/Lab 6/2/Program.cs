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


        public static int minutesToHours(int h, ref int res)
        {
            res = h % 60;
            return h / 60;
        }

        public static int howManySeconds(int h = 0, int m = 0, int s = 0)
        {
            int hoursInSeconds = hoursToMinutes(h);
            int minutesInSconds = minutesToSeconds(m + hoursInSeconds);
            return s + minutesInSconds;
        }

        public static int secondsToMinutes(int s, ref int res)
        {
            res = s % 60;
            return s / 60;
        }
    }


    public class Date
    {
        public int _hours;
        public int _minutes;
        public int _seconds;

        public Date(int h = 0, int m = 0, int s = 0)
        {
            _hours = h; _minutes = m; _seconds = s;
        }

        public void Convert()
        {
            if (_seconds / 60 >= 1)
            {
                int res = 0;
                int newMinutes = Time.secondsToMinutes(_seconds, ref res);
                _seconds = res;
                _minutes += newMinutes;

            }

            if (_minutes / 60 > 1)
            {
                int res = 0;
                int newHours = Time.minutesToHours(_minutes, ref res);
                _minutes = res;
                _hours += newHours;
            }

        }


        public void Print()
        {
            Console.WriteLine($"This is equal to {_hours} hours, {_minutes} minutes and {_seconds} seconds");
        }

    }

    class Program
    {

        static int min = 0;
        static int max = 99999999;
        static void Main()
        {
            Console.WriteLine("Reverse Countdown");
            int seconds = Input.readInt("Enter the number of seconds: ", min, max);
            Date toTransform = new Date(0, 0, seconds);
            toTransform.Convert();
            toTransform.Print();
        }
    }
}

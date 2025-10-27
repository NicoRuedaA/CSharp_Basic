namespace nicorueda
{

    public class Input
    {

        public static string readString(string prompt)
        {
            string result;
            do
            {
                Console.Write(prompt);
                result = Console.ReadLine();
            } while (result == "");
            return result;
        }


        public static char readChar(string prompt)
        {
            string stringAux;
            stringAux = readString(prompt);
            char[] characters = stringAux.ToCharArray();

            return characters[0];
        }


        public static int readInt(string prompt, int min, int max)
        {
            int result = -1;
            do
            {
                try
                {
                    string intString = readString(prompt);
                    result = int.Parse(intString);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Only numbers please");
                }
            } while (result < min || result > max);
            return result;
        }
    }

}

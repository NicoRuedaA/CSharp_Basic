using System;


namespace Cine;


class Program
{
    const int MIN_VALUE=0, MAX_VALUE=100;

    static string readString(string prompt) { 
        string result;
        do { 
            Console.Write(prompt);
            result = Console.ReadLine();
        } while (result == ""); 
        return result;
    }



    static int readInt(string prompt) {
        int result=-1;
        do { 
            try{
            string intString = readString(prompt);
            result = int.Parse(intString);
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
                Console.WriteLine("Only numbers please");
            }
        } while (result is < MIN_VALUE or > MAX_VALUE); 
        return result;
    }

    private static void showBillboard(){
        Console.WriteLine("Welcome to our Multiplex \nWe are presently showing:\n1. Rush (15)\n2. How I Live Now (15)\n3. Thor: The Dark World (12A)\n4. Filth (18)\n5. Planes (U)");
    }

    private static int enterAge(string filmName, int minAge){
        int age = readInt("Enter your age: ");

        if(age<minAge){
            Console.WriteLine("Not enough age for " + filmName);
        }

        else{
            Console.WriteLine("Enough old for " + filmName);
        }

        return age;
    }

    private static void selectFilm(){

        int _age, _filmSelected, minAge=0;
        string _filmName;

        do{
            _filmName = "";
            _filmSelected = readInt("Enter the number of the film you wish to see: ");
        switch (_filmSelected){
            case 1: 
                minAge = 15;
                _filmName = "Rush";
                _age = enterAge(_filmName, minAge);
                break;
            case 2:
                minAge = 15;
                _filmName = "How I love now";
                _age = enterAge(_filmName, minAge);
                break;
            case 3:
                minAge = 12;
                _filmName = "Thor";
                _age = enterAge(_filmName, minAge);
                break;
            case 4:
                minAge = 18;
                _filmName = "Filth";
                _age = enterAge(_filmName, minAge);
                break;
            case 5:
                _filmName = "Planes";
                _age = enterAge(_filmName, minAge);
                break;
            case 0:
                Console.WriteLine("Bye!");
                break;
            default: 
                Console.WriteLine("Film not found");
                break;
        }

        }while (_filmSelected != 0);
    }

    static void Main(){
        showBillboard();
        selectFilm();
    }
}

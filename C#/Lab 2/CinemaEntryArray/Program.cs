using System;


namespace Cine;


class Program
{

    static int FILM_ARRAY_SIZE = 5;
    static string[] _film_array = new string[FILM_ARRAY_SIZE];

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
        for (int i = 0; i < _film_array.Length; i++)
        {
            Console.WriteLine(i+1 + " " + _film_array[i] + ".");
        }
    }
        

    private static int enterAge(){
        int age = readInt("Enter your age: ");
        return age;
    }

    private static void determineIsPossibleViewFilm(int age, int filmSelected){
        string film = _film_array[filmSelected];
        int minAge;

        if (film.EndsWith("(18A)")){
            minAge = 18;
        }
        else if(film.EndsWith("(15A)")){
            minAge = 15;
        }
        else minAge = 12;

        if(age>=minAge){

                Console.WriteLine("You can view the film");
            }
            else Console.WriteLine("Your view is not possible");
    }

    private static void selectFilm(){
        int _age, _filmSelected;

        do{
            _filmSelected = readInt("Enter the number of the film you wish to see: ") ;
            if(_filmSelected>FILM_ARRAY_SIZE || _filmSelected <0) Console.WriteLine("FILM DOESNT EXIST");
            else if(_filmSelected == 0) continue;
            else{
                _age = enterAge();
                determineIsPossibleViewFilm(_age, _filmSelected - 1);
            }
            }while (_filmSelected != 0);
        Console.WriteLine("Exit film selection");
        }

        
    
    private static void EnterFilms()
    {
        int displayNumber, i=0;
        string auxFilm = "";
        do
        {
            displayNumber = i+1;
            //Console.Write("Enter the name of film number " + displayNumber + ": ");
            //_film_array[i] = readString("Enter the name of film number " + displayNumber + ": ");
            auxFilm = readString("Enter the name of film number " + displayNumber + ": ").TrimEnd();
            if (auxFilm.EndsWith("(12A)") || auxFilm.EndsWith("(15A)") || auxFilm.EndsWith("(18A)")){
                _film_array[i] =  auxFilm;
                i++;
            }
            else Console.WriteLine("Error, no correct format");
        } while (i<FILM_ARRAY_SIZE);
    }


    static void Main(){
        FILM_ARRAY_SIZE = readInt("Enter initial films number: ");
        _film_array = new string [FILM_ARRAY_SIZE];

        EnterFilms();

        showBillboard();
        
        selectFilm();
        Console.WriteLine("Bye!");
    }

}

using System;

class Team{
    static int _teamSize = 3, MIN_VALUE = 0, MAX_VALUE = 500;
    static player[] _players = new player[_teamSize];

    struct player{
        internal string name;
        internal int score;
    }


    static string readString(string prompt) { 
        string result;
        do { 
            Console.Write(prompt);
            result = Console.ReadLine();
        } while (result == ""); 
        return result;
    }


    static char readChar(string prompt) { 
        char result;
        string stringAux;
        stringAux = readString(prompt);
        char[] characters = stringAux.ToCharArray();
        return characters[0];
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
            if(result < MIN_VALUE) Console.WriteLine("No negatives values");
            else if(result > MAX_VALUE) Console.WriteLine("No values above 500");
        } while (result < MIN_VALUE || result > MAX_VALUE);
        return result;
    }


    static void insertPlayer(int arrayPlace){
        _players[arrayPlace].name = readString($"Enter player {arrayPlace + 1} name:");
        _players[arrayPlace].score = readInt($"Enter {arrayPlace + 1} score:");
    }


    static bool compareStrings(string a, string b){
        bool result = false;
        if (a.CompareTo(b) < 0)
        {
            result = true;
        }
        return result;
    }


    static void sortByName( player[] array){
        Array.Sort(array, (p1, p2) => string.Compare(p1.name, p2.name));
    }


    static void sortByScore(player[] array){
        Array.Sort(array, (p1, p2) => p1.score.CompareTo(p2.score));
    }

    static void showTeam(player[] array){
        for (int i = 0; i < _teamSize; i++) Console.WriteLine($"Player {array[i].name} with {array[i].score} score");
    }


    static void insertTeam(){
        Console.WriteLine("Insert our team:");
        for (int i = 0; i < _teamSize; i++) insertPlayer(i);
    }

    static void orderTeam(out char _selectionCrit){
        do{
            _selectionCrit = readChar("Sort team by names or scores (N/S)");
        }while ((_selectionCrit != 'N') &&  (_selectionCrit != 'S'));


        switch(_selectionCrit){
            case 'N':
                sortByName(_players);
            break;
            case 'S':
                sortByScore(_players);
            break;
        }
    }


    static void Main(){
        char _selectionCrit;

        insertTeam();

        orderTeam(out _selectionCrit);

        showTeam(_players);
    }
}
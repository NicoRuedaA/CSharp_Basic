using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Compression;
using System.Numerics;
using System.Runtime.Intrinsics.Arm;

namespace HyperSpaceCheeseGame // Note: actual namespace depends on the project name.
{
    internal class Program
    {

        struct Player
        {
            internal string name;
            internal string token;
            internal Vector2 vec;
        }





        static Vector2 winPos = new Vector2(7, 7);


        const int TABLE_HIGH = 8, TABLE_LENGTH = 8;

        static int[,] _board = new int[,] {
            {3, 4, 4, 4, 4, 4, 4, 1},

            {3, 4, 4, 4, 4, 4, 4, 4},

            {3, 3, 3, 3, 3, 4, 3, 4},

            {3, 1, 4, 4, 4, 4, 1, 4},

            {3, 3, 2, 2, 3, 3, 3, 4},

            {3, 3, 4, 3, 3, 3, 4, 4},

            {3, 2, 2, 2, 2, 2, 2, 1},

            {3, 2, 2, 2, 2, 2, 2, 0}
        };

        static string[,] boardTable = new string[TABLE_LENGTH, TABLE_HIGH];

        static string readString(string prompt)
        {
            string result;
            do
            {
                Console.Write(prompt);
                result = Console.ReadLine();
            } while (result == "");
            return result;
        }


        static char readChar(string prompt)
        {
            string stringAux;
            stringAux = readString(prompt);
            char[] characters = stringAux.ToCharArray();

            return characters[0];
        }


        static int readInt(string prompt)
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
            } while (result is < 0 or > 4);
            return result;
        }












        static void DecideIfMove(out char _selectionCrit)
        {
            do
            {
                _selectionCrit = readChar("Wanna move? (N/S)");
            } while ((_selectionCrit != 'N') && (_selectionCrit != 'S'));
        }







        static void PrepareGame(ref Player[] players)
        {
            PreparePlayers(ref players);

            for (int i = 0; i < TABLE_HIGH; i++)
            {
                for (int j = 0; j < TABLE_LENGTH; j++)
                {
                    boardTable[i, j] = GetCellType(new Vector2(i, j));
                }
            }
        }

        static void PreparePlayers(ref Player[] players)
        {
            int numPlayers = readInt("How many players?: ");
            players = new Player[numPlayers];

            for (int i = 0; i < players.Length; i++)
            {
                players[i].name = readString($"Insert player number {i + 1} name: ");
                players[i].token = players[i].name[0].ToString();
                players[i].vec = new Vector2(0, 0);
            }
        }

        static string GetCellType(Vector2 pos)
        {
            string result;
            switch (_board[(int)pos.X, (int)pos.Y])
            {
                case 1:
                    result = "↓";
                    break;
                case 2:
                    result = "←";
                    break;
                case 3:
                    result = "↑";
                    break;
                case 4:
                    result = "→";
                    break;
                case 0:
                    result = "W";
                    break;
                default:
                    result = "?";
                    break;
            }
            return result;
        }


        private static void PlayRound(Player[] players, ref bool end)
        {


            for (int i = 0; i < players.Length; i++)
            {
                PlayerTurn(ref players, ref players[i]);
                if (CheckIfWin(ref players[i]))
                {
                    end = true;
                    break;
                }
                //else DecideIfMove(out critKeepMoving);
            }

            string aux = readString("Enter any character for continue: ");
        }


        static void PlayerTurn(ref Player[] players, ref Player actualPlayer)
        {
            int diceResult = RollDices(actualPlayer);
            Move(ref players, ref actualPlayer, diceResult);
            PrintTable();
        }


        static int RollDices(Player player)
        {
            int min = 1, max = 7, result;
            Random random = new Random();
            result = random.Next(min, max);
            Console.WriteLine($"{player.name} roll result is " + result);
            return result;
        }

        private static void Move(ref Player[] players, ref Player player, int diceResult)
        {

            Vector2 newPos;

            //si no está inicializado iniciamos el juego en 0,0
            if (player.vec == new Vector2(-1, -1))
            {
                player.vec = new Vector2(0, 0);
            }
            else boardTable[(int)player.vec.X, (int)player.vec.Y] = GetCellType(player.vec);

            Console.WriteLine($"Player {player.name} position actual is {player.vec}");
            Console.WriteLine($"{player.vec} is a cell of type {_board[(int)player.vec.X, (int)player.vec.Y]}");

            int actualPosCellType = _board[(int)player.vec.X, (int)player.vec.Y];
            Vector2 direction = new Vector2(0, 0);
            switch (actualPosCellType)
            {
                case 1:
                    direction = new Vector2(0, -1);
                    break;
                case 2:
                    direction = new Vector2(-1, 0);
                    break;
                case 3:
                    direction = new Vector2(0, 1);
                    break;
                case 4:
                    direction = new Vector2(1, 0);
                    break;
                default:
                    newPos = new Vector2(0, 0);
                    Console.WriteLine("ERROR. Invalid Cell tpye");
                    break;
            }

            newPos = player.vec + direction * diceResult;
            if (newPos.Y <= 0) newPos.Y = 0;
            else if (newPos.X < 0) newPos.X = 0;
            else if (newPos.Y >= TABLE_HIGH) newPos.Y = TABLE_HIGH - 1;
            else if (newPos.X >= TABLE_LENGTH) newPos.X = TABLE_LENGTH - 1;
            player.vec = newPos;
            CheckIfCellisEmpty(ref players, ref player, ref newPos);

            boardTable[(int)player.vec.X, (int)player.vec.Y] = player.token.ToString();
            Console.WriteLine("New pos is " + player.vec);
        }

        static void CheckIfCellisEmpty(ref Player[] players, ref Player player, ref Vector2 cellPos)
        {


            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].token == player.token)
                {

                    continue;
                }

                else if (players[i].vec == cellPos)
                {


                    Console.WriteLine($"Token of {players[i].name} moved");
                    PushToken(ref players, ref players[i]);
                }
            }
        }

        static void PushToken(ref Player[] players, ref Player player)
        {

            Console.WriteLine(player.name);
            Console.WriteLine($"Position pre push is {player.vec}");

            Vector2 oldPos = player.vec;
            Vector2 newPos;
            boardTable[(int)oldPos.X, (int)oldPos.Y] = GetCellType(oldPos);

            //si la ficha esta arriba del todo
            if (player.vec.Y >= TABLE_HIGH - 1)
            {
                //si está en la esquian sup izquierda
                if (player.vec.X == 0)
                {

                    newPos = new Vector2(player.vec.X + 1, player.vec.Y);

                    Console.WriteLine("token pushed DOWN");

                }
                else
                {
                    newPos = new Vector2(player.vec.X - 1, player.vec.Y);

                    Console.WriteLine("token pushed DOWN");
                }


            }
            else
            {

                newPos = new Vector2(player.vec.X, player.vec.Y + 1);
                Console.WriteLine($"New position is {newPos}");
                Console.WriteLine("token pushed UP");
            }

            player.vec = newPos;

            CheckIfCellisEmpty(ref players, ref player, ref newPos);
            boardTable[(int)player.vec.X, (int)player.vec.Y] = player.token.ToString();
            Console.WriteLine($"Position post push is {player.vec}");



        }

        static bool CheckIfWin(ref Player player)
        {
            if (player.vec == winPos)
            {
                Console.WriteLine("Game ENDED");
            }
            return player.vec == winPos;
        }




        static void DecideIfNewGame(out char c)
        {

            do
            {
                c = readChar("New game? (N/S)");
            } while ((c != 'N') && (c != 'S'));
        }

        static void PrintTable()
        {
            for (int j = TABLE_HIGH - 1; j >= 0; j--)
            {
                for (int i = 0; i < TABLE_LENGTH; i++)
                {
                    Console.Write(boardTable[i, j].PadRight(5));
                }
                Console.WriteLine();
            }
        }



        static void Main()
        {
            //IMPONER QUE NO PUEDAN HABER 2 TOKENS IGUALES. GENERAR ALEAORIO SI COMIENZAN POR EL MISMO STRIGN HASTA GENERAR 1 VALOR DIFERENTE

            int numPlayers = 0;
            Player[] players = new Player[numPlayers];
            char critNewGame;
            bool gameEnded;
            do
            {
                gameEnded = false;
                PrepareGame(ref players);
                do
                {
                    //round = 1turn.player.
                    //2 players = 1 round of 2 turns
                    PlayRound(players, ref gameEnded);
                } while (!gameEnded);
                DecideIfNewGame(out critNewGame);
            } while (critNewGame != 'N');
        }
    }
}
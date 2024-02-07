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
            char result;
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


        static void PrintTable(Player player)
        {



            for (int j = TABLE_HIGH - 1; j >= 0; j--)
            {
                for (int i = 0; i < TABLE_LENGTH; i++)
                {
                    //    Console.Write($" {boardTable[i, j]}");
                    if (player.vec == new Vector2(i, j))
                    {
                        Console.Write(player.token.PadRight(5));
                    }
                    else Console.Write(boardTable[i, j].PadRight(5));
                }
                Console.WriteLine();
            }
        }

        static int RollDices(Player player)
        {
            int min = 1, max = 7, result;
            Random random = new Random();
            result = random.Next(min, max);
            Console.WriteLine($"{player} roll result is " + result);
            return result;
        }

        private static void Move(ref Player player, int diceResult)
        {
            Console.WriteLine($"Player {player.name} position actual is {player.vec}");
            Console.WriteLine($"{player.vec} is a cell of type {_board[(int)player.vec.X, (int)player.vec.Y]}");

            int actualPosCellType = _board[(int)player.vec.X, (int)player.vec.Y];
            Vector2 direction = new Vector2(0, 0);
            switch (actualPosCellType)
            {
                case 1:
                    direction = new Vector2(0, -1);
                    player.vec = player.vec + direction * diceResult;
                    if (player.vec.Y <= 0) player.vec.Y = 0;
                    break;
                case 2:
                    direction = new Vector2(-1, 0);
                    player.vec = player.vec + direction * diceResult;
                    if (player.vec.X < 0) player.vec.X = 0;
                    break;
                case 3:

                    direction = new Vector2(0, 1);
                    player.vec = player.vec + direction * diceResult;
                    if (player.vec.Y >= TABLE_HIGH) player.vec.Y = TABLE_HIGH - 1;
                    break;
                case 4:
                    direction = new Vector2(1, 0);
                    player.vec = player.vec + direction * diceResult;
                    if (player.vec.X >= TABLE_LENGTH) player.vec.X = TABLE_LENGTH - 1;
                    break;
                default:
                    Console.WriteLine("ERROR. Invalid Cell tpye");
                    break;
            }


            Console.WriteLine("New pos is " + player.vec);
        }

        static void DecideIfMove(out char _selectionCrit)
        {
            do
            {
                _selectionCrit = readChar("Wanna move? (N/S)");
            } while ((_selectionCrit != 'N') && (_selectionCrit != 'S'));
        }


        static void PreparePlayers(ref int players, ref Player[] names, ref Vector2[] playersPos)
        {
            players = readInt("How many players?: ");
            names = new Player[players];
            for (int i = 0; i < players; i++)
            {
                names[i].name = readString($"Insert player number {i + 1} name: ");
                names[i].token = names[i].name[0].ToString();
                names[i].vec = new Vector2(0, 0);
                playersPos = new Vector2[names.Length];
            }
        }


        static void PrepareGame(ref int players, ref Player[] names, ref Vector2[] playersPos)
        {
            PreparePlayers(ref players, ref names, ref playersPos);

            for (int i = 0; i < TABLE_HIGH; i++)
            {
                for (int j = 0; j < TABLE_LENGTH; j++)
                {
                    switch (_board[i, j])
                    {
                        case 1:
                            boardTable[i, j] = "↓";
                            break;
                        case 2:
                            boardTable[i, j] = "←";
                            break;
                        case 3:
                            boardTable[i, j] = "↑";
                            break;
                        case 4:
                            boardTable[i, j] = "→";
                            break;
                        case 0:
                            boardTable[i, j] = "W";
                            break;
                        default:
                            boardTable[i, j] = "?";
                            break;
                    }
                }
            }
        }


        static Vector2 winPos = new Vector2(7, 7);

        static bool CheckIfWin(ref Player player)
        {
            if (player.vec == winPos)
            {
                Console.WriteLine("Game ENDED");
            }
            return player.vec == winPos;
        }


        static void PlayerTurn(ref Player player, ref Vector2[] playersPos)
        {
            int diceResult = RollDices(player);
            Move(ref player, diceResult);
            PrintTable(player);
        }

        static void DecideIfNewGame(out char c)
        {

            do
            {
                c = readChar("New game? (N/S)");
            } while ((c != 'N') && (c != 'S'));
        }

        private static void PlayRound(Player[] players, ref bool end, ref Vector2[] playersPos)
        {


            for (int i = 0; i < players.Length; i++)
            {


                PlayerTurn(ref players[i], ref playersPos);
                if (CheckIfWin(ref players[i]))
                {
                    end = true;
                    break;
                }
                //else DecideIfMove(out critKeepMoving);
            }

            string aux = readString("Enter any character for continue: ");
        }

        static void Main()
        {

            //PONER PLAYERS POSITION INICIAL A -1, -1 Y AL JUGAR SI SU POSICION ES -1, -1 HACER QUE SEA IGUAL A 0,0
            //PARA SIMULAR "PONERLAS EN JUEGO"

            int nextMove, numPlayers = 0;

            Player[] players = new Player[numPlayers];
            Vector2[] playerPositions = new Vector2[0];
            Player actualPlayer;
            char critKeepMoving, critNewGame;
            bool gameEnded;
            do
            {
                gameEnded = false;
                PrepareGame(ref numPlayers, ref players, ref playerPositions);
                do
                {
                    PlayRound(players, ref gameEnded, ref playerPositions);

                } while (!gameEnded);

                DecideIfNewGame(out critNewGame);
            } while (critNewGame != 'N');


            /*
            do
                {
                   
                } while (critKeepMoving == 'S');
                */
        }
    }
}
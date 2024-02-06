using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Compression;
using System.Numerics;

namespace HyperSpaceCheeseGame // Note: actual namespace depends on the project name.
{
    internal class Program
    {

        const int TABLE_HIGH = 8, TABLE_LENGTH = 8;

        static int[,] _board = new int[,] {

            {3,3,3,3,3,3,3,3}, // row 7 

            {4,4,3,1,3,3,2,2}, // row 6 

            {4,4,3,4,2,4,2,2}, // row 5 

            {4,4,3,4,3,3,2,2}, // row 4 

            {4,4,3,4,3,3,2,2}, // row 3 

            {4,4,4,4,3,3,2,2}, // row 2 
                                                                        
            {4,4,3,1,3,4,2,2}, // row 1 
            
            {1,4,4,4,4,4,1,0}, // row 0 
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


        static void PrintTable(Vector2 pos)
        {
            /*for (int i = TABLE_LENGTH; i >= 0; i--)
            {

                for (int j = -1; j < TABLE_LENGTH; j++)
                {
                    if (i == TABLE_LENGTH) Console.Write(j.ToString().PadRight(5));



                    else if (j == -1) Console.Write(i.ToString().PadRight(5));


                    //else 
                    else if (pos == new Vector2(i, j))
                    {
                        Console.Write("O".PadRight(5));
                    }
                    else Console.Write(boardTable[i, j].PadRight(5));
                }

                Console.WriteLine();
            }
            
                    static int[,] _board = new int[,] {

            {3,3,3,3,3,3,3,3}, // row 7 

            {4,4,3,1,3,3,2,2}, // row 6 

            {4,4,3,4,2,4,2,2}, // row 5 

            {4,4,3,4,3,3,2,2}, // row 4 

            {4,4,3,4,3,3,2,2}, // row 3 

            {4,4,4,4,3,3,2,2}, // row 2 
                                                                        
            {4,4,3,1,3,4,2,2}, // row 1 
            
            {1,4,4,4,4,4,1,0}, // row 0 
            };*/


            for (int i = 0; i <= TABLE_LENGTH; i++)
            {

                for (int j = -1; j < TABLE_LENGTH; j++)
                {
                    if (i == TABLE_LENGTH) Console.Write(j.ToString().PadRight(5));



                    else if (j == -1) Console.Write(i.ToString().PadRight(5));


                    //else 
                    else if (pos == new Vector2(i, j))
                    {
                        Console.Write("O".PadRight(5));
                    }
                    else Console.Write(boardTable[i, j].PadRight(5));
                }

                Console.WriteLine();
            }

        }

        static int RollDices()
        {
            int min = 1, max = 7, result;
            Random random = new Random();
            result = random.Next(min, max);
            Console.WriteLine("Roll result is " + result);
            return result;
        }

        private static void Move(ref Vector2 pos, int diceResult)
        {
            Console.WriteLine($"Position actual is {pos}");
            Console.WriteLine($"{pos} is a cell of type {_board[(int)pos.X, (int)pos.Y]}");

            int actualPosCellType = _board[(int)pos.X, (int)pos.Y];
            Vector2 direction = new Vector2(0, 0);
            switch (actualPosCellType)
            {
                case 1:
                    direction = new Vector2(0, -1);
                    pos = pos + direction * diceResult;
                    if (pos.Y <= 0) pos.Y = 0;
                    break;
                case 2:
                    direction = new Vector2(-1, 0);
                    pos = pos + direction * diceResult;
                    if (pos.X < 0) pos.X = 0;
                    break;
                case 3:
                    pos = pos + new Vector2(0, 1);
                    Console.WriteLine(pos);
                    /*direction = new Vector2(0, 1);
                    pos = pos + direction * diceResult;
                    if (pos.Y >= TABLE_HIGH) pos.Y = TABLE_HIGH - 1;*/
                    break;
                case 4:
                    direction = new Vector2(1, 0);
                    pos = pos + direction * diceResult;
                    if (pos.X > TABLE_LENGTH) pos.X = TABLE_LENGTH - 1;
                    break;
                default:
                    Console.WriteLine("ERROR. Invalid Cell tpye");
                    break;

                    Console.WriteLine($"New pos = {pos}");

            }


            Console.WriteLine("New pos is " + pos);
        }

        static void DecideIfMove(out char _selectionCrit)
        {
            do
            {
                _selectionCrit = readChar("Wanna move? (N/S)");
            } while ((_selectionCrit != 'N') && (_selectionCrit != 'S'));
        }


        static void PreapreGame()
        {
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


        static void CheckIfWin(Vector2 pos)
        {

        }


        static void PlayerTurn(ref Vector2 pos)
        {
            int diceResult = RollDices();
            Move(ref pos, diceResult);
            PrintTable(pos);
            CheckIfWin(pos);
            //    DecideIfMove(out critKeepMoving} while (critKeepMoving == 'S');
        }


        static void Main()
        {
            Vector2 startingPos = new Vector2(1, 0);
            Vector2 actualPos = startingPos;
            int nextMove;
            char critKeepMoving;


            PreapreGame();
            PrintTable(actualPos);
            PlayerTurn(ref startingPos);
            //);



            //decidit quien gana
            //determinar gandaor alllegar al final

        }
    }
}
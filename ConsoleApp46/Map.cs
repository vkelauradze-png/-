using System;

namespace ConsoleApp46
{

    /// <summary>
    /// класс отображения карты
    /// </summary>
    public class Map
    {

        /// <summary>
        /// выводит карту с символами
        /// </summary>
        /// <param name="mas">массив символов</param>
        /// <param name="a">номер игрока</param>
        /// <param name="ch">символ игрока</param>
        static public void GetMap(char[,] mas, int a, char ch)
        {
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                Console.SetCursorPosition(60 * (a - 1), i);
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    if (i == 12 & j == 12)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(ch + " ");
                        Console.ResetColor();
                    }
                    else
                    {
                        if (mas[i, j] == (char)1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(mas[i, j] + " ");
                            Console.ResetColor();
                        }
                        else if (mas[i, j] == (char)3)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(mas[i, j] + " ");
                            Console.ResetColor();
                        }
                        else if (mas[i, j] == 'U')
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(mas[i, j] + " ");
                            Console.ResetColor();
                        }
                        else if (mas[i, j] == (char)0177)
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write(mas[i, j] + " ");
                            Console.ResetColor();
                        }
                        else if (mas[i, j] == (char)19)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(mas[i, j] + " ");
                            Console.ResetColor();
                        }
                        else if (mas[i, j] == 'T')
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write(mas[i, j] + " ");
                            Console.ResetColor();
                        }
                        else if (mas[i, j] == 'R')
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(mas[i, j] + " ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(mas[i, j] + " ");
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// заполняет массив символами в соответствии с определенными правилами
        /// </summary>
        /// <param name="mas">массив символов</param>
        /// <param name="p">персонаж</param>
        static public void Array(char[,] mas, Person p)
        {
            Random rnd = new Random();
            for (int i = 0; i < mas.GetLength(0); i++)
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    int count = rnd.Next(100);

                    mas[i, j] = '.';
                    if (count >= 40 && count <= 40)
                        mas[i, j] = (char)1;
                    if (count >= 98)
                        mas[i, j] = (char)3;
                    if (count >= 30 && count < 40)
                    {
                        int X = i;
                        int Y = j;
                        for (int t = 0; t < 10; t++)
                        {
                            mas[X++, Y++] = (char)0177;
                            if (X > mas.GetLength(0) - 1 || Y > mas.GetLength(1) - 1)
                                break;
                        }
                    }
                    if (p.levelWorld > 1)
                        mas[mas.GetLength(0) / 4, mas.GetLength(1) / 2] = (char)19;
                    if (count < 30)
                        mas[i, j] = 'T';
                    if (count == 70)
                        mas[i, j] = 'R';
                }
        }
    }
}
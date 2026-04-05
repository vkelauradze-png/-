using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp46
{

    /// <summary>
    /// представляет карту комнаты
    /// </summary>
    internal class RoomMap
    {

        /// <summary>
        /// создание массива с символами
        /// </summary>
        /// <param name="mas">массив символов</param>
        /// <param name="ch">символ игрока</param>
        static public void Array(char[,] mas, char ch)
        {
            Random rnd = new Random();

            for (int i = 0; i < mas.GetLength(0); i++)
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    int count = rnd.Next(100);

                    mas[i, j] = '.';
                    if (count >= 50 && count < 52)
                        mas[i, j] = '$';
                    if (count >= 53 && count < 54)
                        mas[i, j] = '@';
                }
            for (int i = 0; i < mas.GetLength(0); i++)
                mas[0, i] = mas[24, i] = mas[i, 0] = mas[i, 24] = '#';

            mas[1, 23] = 'E';
            mas[23, 1] = ch;
        }

        /// <summary>
        /// отображение карты
        /// </summary>
        /// <param name="mas">массив символов</param>
        /// <param name="ch">символ игрока</param>
        /// <param name="pnum">номер игрока</param>
        static public void GetMap(char[,] mas, char ch, int pnum)
        {
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                Console.SetCursorPosition(60 * (pnum - 1), i);
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    if (mas[i, j] == ch)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(ch + " ");
                        Console.ResetColor();
                    }
                    else if (mas[i, j] == '$')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(mas[i, j] + " ");
                        Console.ResetColor();
                    }
                    else if (mas[i, j] == '@')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write(mas[i, j] + " ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(mas[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}

using System;
using System.IO;
using System.Threading;

namespace ConsoleApp46
{
    public class Program
    {
        public static string Player1Name;
        public static string Player2Name;

        static void Main(string[] args)
        {
            Console.Title = "Roguelike Game";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========================================");
            Console.WriteLine("           ДОБРО ПОЖАЛОВАТЬ В ИГРУ!      ");
            Console.WriteLine("========================================");
            Console.ResetColor();

            Player1Name = GetValidatedInput("введите никнейм первого игрока: ");
            Player2Name = GetValidatedInput("введите никнейм второго игрока: ");

            Person p1 = new Person(100, Player1Name);
            Person p2 = new Person(100, Player2Name);

            int def = 25;

            char[,] v1 = new char[def, def];
            char[,] v2 = new char[def, def];

            ConsoleKeyInfo CKI;

            try
            {
                Map.Array(v1, p1);
                Thread.Sleep(100);
                Map.Array(v2, p2);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Ошибка при генерации карты: {ex.Message}");
                Console.ResetColor();
                Console.WriteLine("Нажмите любую клавишу для выхода...");
                Console.ReadKey(true);
                return;
            }

            Console.Clear();

            Map.GetMap(v1, 1, '0');
            Map.GetMap(v2, 2, '8');

            p1.GetCharacter(1);
            p2.GetCharacter(2);

            string errorMessage = string.Empty;

            while (p1.HP > 0 && p2.HP > 0)
            {
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    Console.SetCursorPosition(0, 30);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMessage.PadRight(50));
                    Console.ResetColor();
                }

                CKI = Console.ReadKey(true);
                Console.Clear();

                try
                {
                    ProcessKeyPress(CKI, v1, v2, p1, p2);
                    errorMessage = string.Empty;
                }
                catch (InvalidKeyException ex)
                {
                    errorMessage = ex.Message;
                }
                catch (IndexOutOfRangeException ex)
                {
                    errorMessage = "Ошибка: выход за границы карты!";
                }
                catch (Exception ex)
                {
                    errorMessage = $"Непредвиденная ошибка: {ex.Message}";
                }

                try
                {
                    Map.GetMap(v1, 1, '0');
                    Map.GetMap(v2, 2, '8');

                    p1.GetCharacter(1);
                    p2.GetCharacter(2);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Ошибка отображения: {ex.Message}");
                    Console.ResetColor();
                }
            }

            Console.Clear();
            if (p1.HP <= 0 && p2.HP <= 0)
            {
                Console.WriteLine("Ничья! Оба игрока погибли.");
            }
            else if (p1.HP <= 0)
            {
                Console.WriteLine($"🏆 Победитель: {p2.NamePerson}! 🏆");
            }
            else
            {
                Console.WriteLine($"🏆 Победитель: {p1.NamePerson}! 🏆");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey(true);
        }

        private static string GetValidatedInput(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                input = "Аноним";
                Console.WriteLine($"Имя не введено. Используем: {input}");
            }

            if (input.Length > 20)
                input = input.Substring(0, 20);

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                input = input.Replace(c.ToString(), "");
            }

            return input.Trim();
        }

        static void ProcessKeyPress(ConsoleKeyInfo CKI, char[,] v, char[,] v2, Person p1, Person p2)
        {
            if (v == null || v2 == null || p1 == null || p2 == null)
                throw new ArgumentNullException();

            switch (CKI.Key)
            {
                case ConsoleKey.UpArrow:
                    if (DrawMap(v, v2, p1, p2, '0', 1, 11))
                        Moving.Move(v, 1);
                    break;
                case ConsoleKey.DownArrow:
                    if (DrawMap(v, v2, p1, p2, '0', 1, 13))
                        Moving.Move(v, 2);
                    break;
                case ConsoleKey.LeftArrow:
                    if (DrawMap(v, v2, p1, p2, '0', 1, 12, 11))
                        Moving.Move(v, 3);
                    break;
                case ConsoleKey.RightArrow:
                    if (DrawMap(v, v2, p1, p2, '0', 1, 12, 13))
                        Moving.Move(v, 4);
                    break;
                case ConsoleKey.Spacebar:
                    Moving.BreakTree(v);
                    break;
                case ConsoleKey.Z:
                    Moving.DeliteEnemies(v);
                    break;
                case ConsoleKey.X:
                    Moving.Boomb(v);
                    break;
                case ConsoleKey.W:
                    if (DrawMap(v2, v, p2, p1, '8', 2, 11))
                        Moving.Move(v2, 1);
                    break;
                case ConsoleKey.S:
                    if (DrawMap(v2, v, p2, p1, '8', 2, 13))
                        Moving.Move(v2, 2);
                    break;
                case ConsoleKey.A:
                    if (DrawMap(v2, v, p2, p1, '8', 2, 12, 11))
                        Moving.Move(v2, 3);
                    break;
                case ConsoleKey.D:
                    if (DrawMap(v2, v, p2, p1, '8', 2, 12, 13))
                        Moving.Move(v2, 4);
                    break;
                case ConsoleKey.Backspace:
                    Moving.BreakTree(v2);
                    break;
                case ConsoleKey.Home:
                    Moving.DeliteEnemies(v2);
                    break;
                case ConsoleKey.Delete:
                    Moving.Boomb(v2);
                    break;
                case ConsoleKey.Escape:
                    Console.Clear();
                    Console.WriteLine("Игра прервана. Нажмите любую клавишу для выхода...");
                    Console.ReadKey(true);
                    Environment.Exit(0);
                    break;
                default:
                    throw new InvalidKeyException($"Ошибка: клавиша '{CKI.Key}' недопустима в игре!");
            }
        }

        static public bool DrawMap(char[,] v, char[,] v2, Person p, Person p2, char ch, int pnum, int A = 12, int B = 12)
        {
            if (v == null || p == null) return false;
            if (A < 0 || A >= v.GetLength(0) || B < 0 || B >= v.GetLength(1)) return false;

            Ivent iv = new Ivent(p);
            return iv.CheckIvent(ref p.levelWorld, v, v2, A, B, ch, pnum, p2);
        }
    }
}
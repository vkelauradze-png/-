using System;
using System.Threading;

namespace ConsoleApp46
{

    /// <summary>
    /// класс, представляющий программу игры
    /// </summary>
    public class Program
    {   /// <summary>
        /// сила игрока
        /// </summary>
        public int Strength { get; private set; } = 10;
        public void UpgradeStrength()
        {
            Strength += 2;
        }
        /// <summary>
        /// имя игрока
        /// </summary>
        public static string a;

        /// <summary>
        /// имя игрока
        /// </summary>
        public static string b;

        /// <summary>
        /// основной метод программы
        /// </summary>
        /// <param name="args">аргументы командной строки</param>
        static void Main(string[] args)
        {
            Console.Write("введите никнейм первого игрока: ");
            a = Console.ReadLine();
            Console.Write("введите никнейм второго игрока: ");
            b = Console.ReadLine();

            Person p1 = new Person(100, a);
            Person p2 = new Person(100, b);

            int def = 25;

            char[,] v1 = new char[def, def];
            char[,] v2 = new char[def, def];


            ConsoleKeyInfo CKI;

            Map.Array(v1, p1);
            Thread.Sleep(100);
            Map.Array(v2, p2);

            Console.Clear();

            Map.GetMap(v1, 1, '0');
            Map.GetMap(v2, 2, '8');

            p1.GetCharacter(1);
            p2.GetCharacter(2);


            string errorMessage = string.Empty;
            while (p1.HP > 0 || p2.HP > 0)
            {
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    Console.SetCursorPosition(0, 25);
                    Console.WriteLine(errorMessage);
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

                Map.GetMap(v1, 1, '0');
                Map.GetMap(v2, 2, '8');

                p1.GetCharacter(1);
                p2.GetCharacter(2);
            }
            WinORLose.Losei();
        }

        /// <summary>
        /// обработка нажатия клавиши
        /// </summary>
        /// <param name="CKI">информация о нажатой клавише</param>
        /// <param name="v">массив символов игрока 1</param>
        /// <param name="v2">массив символов игрока 2</param>
        /// <param name="p1">первый игрок</param>
        /// <param name="p2">второй игрок</param>
        static void ProcessKeyPress(ConsoleKeyInfo CKI, char[,] v, char[,] v2, Person p1, Person p2)
        {
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

                //2 игрок
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
                default:
                    throw new InvalidKeyException("Ошибка: нажата недопустимая клавиша!");
            }
        }

        /// <summary>
        /// проверка игрового события и отображение на поле
        /// </summary>
        /// <param name="v">массив символов игрока 1</param>
        /// <param name="v2">массив символов игрока 2</param>
        /// <param name="p">первый игрок</param>
        /// <param name="p2">второй игрок</param>
        /// <param name="ch">символ игрока</param>
        /// <param name="pnum">номер игрока</param>
        /// <param name="A">координата A</param>
        /// <param name="B">оордината B</param>
        /// <returns>Возвращает true, если было обнаружено событие, иначе false</returns>
        static public bool DrawMap(char[,] v, char[,] v2, Person p, Person p2, char ch, int pnum, int A = 12, int B = 12)
        {
            Ivent iv = new Ivent(p);
            return iv.CheckIvent(ref p.levelWorld, v, v2, A, B, ch, pnum, p2);
        }
    }
}

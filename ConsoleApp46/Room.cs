using System;

namespace ConsoleApp46
{

    /// <summary>
    /// класс комнаты
    /// </summary>
    public class Room
    {

        /// <summary>
        /// создает новый экземпляр комнаты и управляет игровым процессом
        /// </summary>
        /// <param name="p">первый игрок</param>
        /// <param name="ch">символ игрока</param>
        /// <param name="pnum">номер игрока</param>
        /// <param name="p1">второй игрок</param>
        /// <param name="v2">массив символов второго игрока</param>
        public Room(Person p, char ch, int pnum, Person p1, char[,] v2)
        {

            int def = 25;
            char[,] v = new char[def, def];

            ConsoleKeyInfo CKI;

            RoomMap.Array(v, ch);


            string errorMessage = string.Empty;

            char ch2;
            if (ch == '0')
                ch2 = '8';
            else
                ch2 = '0';

            int pnum2;
            if (pnum == 1)
                pnum2 = 2;
            else
                pnum2 = 1;

            RoomMap.GetMap(v, ch, pnum);
            Map.GetMap(v2, pnum2, ch2);

            p.GetCharacter(pnum);
            p1.GetCharacter(pnum2);

            while (p.HP > 0)
            {
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    Console.SetCursorPosition(60 * (pnum - 1), 25);
                    Console.WriteLine(errorMessage);
                }

                CKI = Console.ReadKey();
                Console.Clear();

                int _i = 0, _j = 0;
                try
                {
                    ProcessKeyPress(CKI, v, v2, p, p1, ref _i, ref _j, pnum, ch);
                    errorMessage = string.Empty;
                }
                catch (InvalidKeyException ex)
                {
                    errorMessage = ex.Message;
                }

                RoomMap.GetMap(v, ch, pnum);
                Map.GetMap(v2, pnum2, ch2);

                p.GetCharacter(pnum);
                p1.GetCharacter(pnum2);

                if (v[1, 23] == ch)
                    break;
            }
            if (p.HP <= 0)
            {
                Console.Clear();
                WinORLose.Losei();
            }
            Console.Clear();
        }

        /// <summary>
        /// обработка нажатия клавиши
        /// </summary>
        /// <param name="CKI">нажатая клавиша</param>
        /// <param name="v">массив символов игрока 1</param>
        /// <param name="v2">массив символов игрока 2</param>
        /// <param name="p">персонаж 1</param>
        /// <param name="p2">персонаж 2</param>
        /// <param name="_i">координата i</param>
        /// <param name="_j">координата j</param>
        /// <param name="pnum">номер игрока</param>
        /// <param name="ch">символ игрока</param>
        static void ProcessKeyPress(ConsoleKeyInfo CKI, char[,] v, char[,] v2, Person p, Person p1, ref int _i, ref int _j, int pnum, char ch)
        {
            if (pnum == 1)
                switch (CKI.Key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            Moving.MovePerson(ref v, CKI.Key, p, ref p.levelWorld, ref _i, ref _j, ch, pnum);
                            Moving.MoveEnemy(ref v, _i, _j, p, ch);
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            Moving.MovePerson(ref v, CKI.Key, p, ref p.levelWorld, ref _i, ref _j, ch, pnum);
                            Moving.MoveEnemy(ref v, _i, _j, p, ch);
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        {
                            Moving.MovePerson(ref v, CKI.Key, p, ref p.levelWorld, ref _i, ref _j, ch, pnum);
                            Moving.MoveEnemy(ref v, _i, _j, p, ch);
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            Moving.MovePerson(ref v, CKI.Key, p, ref p.levelWorld, ref _i, ref _j, ch, pnum);
                            Moving.MoveEnemy(ref v, _i, _j, p, ch);
                            break;
                        }

                    case ConsoleKey.W:
                        if (Program.DrawMap(v2, v, p1, p, '8', 2, 11))
                            Moving.Move(v2, 1);
                        break;
                    case ConsoleKey.S:
                        if (Program.DrawMap(v2, v, p1, p, '8', 2, 13))
                            Moving.Move(v2, 2);
                        break;
                    case ConsoleKey.A:
                        if (Program.DrawMap(v2, v, p1, p, '8', 2, 12, 11))
                            Moving.Move(v2, 3);
                        break;
                    case ConsoleKey.D:
                        if (Program.DrawMap(v2, v, p1, p, '8', 2, 12, 13))
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

                }
            else if (pnum == 2)
                switch (CKI.Key)
                {
                    case ConsoleKey.W:
                        {
                            Moving.MovePerson(ref v, CKI.Key, p, ref p.levelWorld, ref _i, ref _j, ch, pnum);
                            Moving.MoveEnemy(ref v, _i, _j, p, ch);
                            break;
                        }
                    case ConsoleKey.S:
                        {
                            Moving.MovePerson(ref v, CKI.Key, p, ref p.levelWorld, ref _i, ref _j, ch, pnum);
                            Moving.MoveEnemy(ref v, _i, _j, p, ch);
                            break;
                        }
                    case ConsoleKey.A:
                        {
                            Moving.MovePerson(ref v, CKI.Key, p, ref p.levelWorld, ref _i, ref _j, ch, pnum);
                            Moving.MoveEnemy(ref v, _i, _j, p, ch);
                            break;
                        }
                    case ConsoleKey.D:
                        {
                            Moving.MovePerson(ref v, CKI.Key, p, ref p.levelWorld, ref _i, ref _j, ch, pnum);
                            Moving.MoveEnemy(ref v, _i, _j, p, ch);
                            break;
                        }

                    case ConsoleKey.UpArrow:
                        if (Program.DrawMap(v2, v, p1, p, '0', 1, 11))
                            Moving.Move(v2, 1);
                        break;
                    case ConsoleKey.DownArrow:
                        if (Program.DrawMap(v2, v, p1, p, '0', 1, 13))
                            Moving.Move(v2, 2);
                        break;
                    case ConsoleKey.LeftArrow:
                        if (Program.DrawMap(v2, v, p1, p, '0', 1, 12, 11))
                            Moving.Move(v2, 3);
                        break;
                    case ConsoleKey.RightArrow:
                        if (Program.DrawMap(v2, v, p1, p, '0', 1, 12, 13))
                            Moving.Move(v2, 4);
                        break;
                    case ConsoleKey.Spacebar:
                        Moving.BreakTree(v2);
                        break;
                    case ConsoleKey.Z:
                        Moving.DeliteEnemies(v2);
                        break;
                    case ConsoleKey.X:
                        Moving.Boomb(v2);
                        break;
                }
        }
    }
}

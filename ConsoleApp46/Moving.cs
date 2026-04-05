using System;
using System.Reflection.Emit;

namespace ConsoleApp46
{

    /// <summary>
    /// класс для управления движением
    /// </summary>
    public class Moving
    {

        /// <summary>
        /// метод для перемещения объектов по массиву в зависимости от нажатой клавиши
        /// </summary>
        /// <param name="mas">массив символов</param>
        /// <param name="a">направление перемещения (1 - вверх, 2 - вниз, 3 - влево, 4 - вправо)</param>
        static public void Move(char[,] mas, int a)
        {
            switch (a)
            {
                case 1:
                    for (int i = mas.GetLength(0) - 1; i > 0; i--)
                    {
                        for (int j = 0; j < mas.GetLength(1); j++)
                        {
                            char temp = mas[i - 1, j];
                            mas[i - 1, j] = mas[i, j];
                            mas[i, j] = temp;
                            if (i == (mas.GetLength(0) - 1) / 2 && j == (mas.GetLength(1) - 1) / 2)
                                mas[i + 1, j] = '.';
                        }
                        WinORLose.WinI(mas);
                    }
                    break;


                case 2:
                    for (int i = 0; i < mas.GetLength(0) - 1; i++)
                    {
                        for (int j = 0; j < mas.GetLength(1); j++)
                        {
                            char temp = mas[i + 1, j];
                            mas[i + 1, j] = mas[i, j];
                            mas[i, j] = temp;
                            if (i == (mas.GetLength(0) - 1) / 2 && j == (mas.GetLength(1) - 1) / 2)
                                mas[i - 1, j] = '.';
                        }
                        WinORLose.WinI(mas);
                    }
                    break;

                case 3:
                    for (int i = 0; i < mas.GetLength(0); i++)
                    {
                        char temp = mas[i, mas.GetLength(1) - 1];
                        for (int j = mas.GetLength(1) - 1; j > 0; j--)
                        {
                            mas[i, j] = mas[i, j - 1];
                            if (i == (mas.GetLength(0) - 1) / 2 && j == (mas.GetLength(1) - 1) / 2)
                                mas[i, j + 1] = '.';
                        }
                        mas[i, 0] = temp;
                        WinORLose.WinI(mas);
                    }
                    break;

                case 4:
                    for (int i = 0; i < mas.GetLength(0); i++)
                    {
                        char temp = mas[i, 0];
                        for (int j = 0; j < mas.GetLength(1) - 1; j++)
                        {
                            mas[i, j] = mas[i, j + 1];
                            if (i == (mas.GetLength(0) - 1) / 2 && j == (mas.GetLength(1) - 1) / 2)
                                mas[i, j - 1] = '.';
                        }
                        mas[i, mas.GetLength(1) - 1] = temp;
                        WinORLose.WinI(mas);
                    }
                    break;
            }
        }

        /// <summary>
        /// метод для "разрушения" деревьев в указанной точке в массиве
        /// </summary>
        /// <param name="map">массив символов</param>
        static public void BreakTree(char[,] map)
        {
            int x = map.GetLength(0) / 2;
            int y = map.GetLength(1) / 2;
            if (Person.CountBreakTreesm == 1 || Person.CountBreakTreesm == 2)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx != 0 || dy != 0)
                        {
                            int targetX = x + dx;
                            int targetY = y + dy;

                            if (targetX >= 0 && targetX < map.GetLength(0) && targetY >= 0 && targetY < map.GetLength(1))
                            {
                                if (Person.CountBreakTreesm == 1)
                                    if ((Math.Abs(dx) + Math.Abs(dy)) % 2 == 1 && map[targetX, targetY] == 'T')
                                    {
                                        map[targetX, targetY] = '.';
                                    }
                                if (Person.CountBreakTreesm == 2)
                                    if (map[targetX, targetY] == 'T')
                                    {
                                        map[targetX, targetY] = '.';
                                    }
                            }
                        }
                    }
                }
            }
            else if (Person.CountBreakTreesm == 3)
            {
                for (int dx = -2; dx <= 2; dx++)
                {
                    for (int dy = -2; dy <= 2; dy++)
                    {
                        if (dx != 0 || dy != 0)
                        {
                            int targetX = x + dx;
                            int targetY = y + dy;

                            if (targetX >= 0 && targetX < map.GetLength(0) && targetY >= 0 && targetY < map.GetLength(1))
                            {
                                if (Math.Abs(dx) <= 2 && Math.Abs(dy) <= 2 && (dx != 0 || dy != 0) && map[targetX, targetY] == 'T')
                                {
                                    map[targetX, targetY] = '.';
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// метод для удаления врагов в указанной точке в массиве
        /// </summary>
        /// <param name="map">массив символов</param>
        static public void DeliteEnemies(char[,] map)
        {
            int x = map.GetLength(0) / 2;
            int y = map.GetLength(1) / 2;

            if (Person.CountDeliteEnemiesm == 1 || Person.CountDeliteEnemiesm == 2)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx != 0 || dy != 0)
                        {
                            int targetX = x + dx;
                            int targetY = y + dy;

                            if (targetX >= 0 && targetX < map.GetLength(0) && targetY >= 0 && targetY < map.GetLength(1))
                            {
                                if (Person.CountDeliteEnemiesm == 1)
                                    if ((Math.Abs(dx) + Math.Abs(dy)) % 2 == 1 && map[targetX, targetY] == (char)1)
                                    {
                                        map[targetX, targetY] = '.';
                                    }
                                if (Person.CountDeliteEnemiesm == 2)
                                    if (map[targetX, targetY] == (char)1)
                                    {
                                        map[targetX, targetY] = '.';
                                    }
                            }
                        }
                    }
                }
            }
            else if (Person.CountDeliteEnemiesm == 3)
            {
                for (int dx = -2; dx <= 2; dx++)
                {
                    for (int dy = -2; dy <= 2; dy++)
                    {
                        if (dx != 0 || dy != 0)
                        {
                            int targetX = x + dx;
                            int targetY = y + dy;

                            if (targetX >= 0 && targetX < map.GetLength(0) && targetY >= 0 && targetY < map.GetLength(1))
                            {
                                if (Math.Abs(dx) <= 2 && Math.Abs(dy) <= 2 && (dx != 0 || dy != 0) && map[targetX, targetY] == (char)1)
                                {
                                    map[targetX, targetY] = '.';
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// метод для "взрыва" всего в указанной точке
        /// </summary>
        /// <param name="map">массив символов</param>
        static public void Boomb(char[,] map)
        {
            int x = map.GetLength(0) / 2;
            int y = map.GetLength(1) / 2;
            int distance = 0;
            if (Person.CountBoombm == 1)
            {
                distance = 2;
            }
            else if (Person.CountBoombm == 2)
            {
                distance = 3;
            }
            else if (Person.CountBoombm == 3)
            {
                distance = 4;
            }

            for (int dx = -distance; dx <= distance; dx++)
            {
                for (int dy = -distance; dy <= distance; dy++)
                {
                    int targetX = x + dx;
                    int targetY = y + dy;
                    if (targetX >= 0 && targetX < map.GetLength(0) && targetY >= 0 && targetY < map.GetLength(1))
                    {
                        if (Math.Abs(dx) <= distance && Math.Abs(dy) <= distance && (dx != 0 || dy != 0))
                        {
                            map[targetX, targetY] = '.';
                        }
                    }
                }
            }
        }

        /// <summary>
        /// перемещает персонажа по карте и обрабатывает взаимодействие с объектами на карте
        /// </summary>
        /// <param name="map">игровая карта</param>
        /// <param name="key">клавиша нажатая игроком</param>
        /// <param name="hero">персонаж</param>
        /// <param name="levelWorld">уровень мира</param>
        /// <param name="_i">координата i игрока после перемещения</param>
        /// <param name="_j">координата j игрока после перемещения</param>
        /// <param name="ch">символ игрока на карте</param>
        /// <param name="pnum">номер игрока</param>
        static public void MovePerson(ref char[,] map, ConsoleKey key, Person hero, ref int levelWorld, ref int _i, ref int _j, char ch, int pnum)
        {
            char[,] newMap = new char[map.GetLength(0), map.GetLength(1)];
            int newX, newY;

            Array.Copy(map, newMap, map.Length);

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == ch)
                    {
                        newX = i;
                        newY = j;
                        if (pnum == 1)
                            switch (key)
                            {
                                case ConsoleKey.UpArrow:
                                    newX = (i - 1 + map.GetLength(0)) % map.GetLength(0);
                                    break;
                                case ConsoleKey.DownArrow:
                                    newX = (i + 1) % map.GetLength(0);
                                    break;
                                case ConsoleKey.LeftArrow:
                                    newY = (j - 1 + map.GetLength(1)) % map.GetLength(1);
                                    break;
                                case ConsoleKey.RightArrow:
                                    newY = (j + 1) % map.GetLength(1);
                                    break;
                                case ConsoleKey.Z:
                                    DeliteEnemies(map);
                                    break;
                            }
                        else if (pnum == 2)
                            switch (key)
                            {
                                case ConsoleKey.W:
                                    newX = (i - 1 + map.GetLength(0)) % map.GetLength(0);
                                    break;
                                case ConsoleKey.S:
                                    newX = (i + 1) % map.GetLength(0);
                                    break;
                                case ConsoleKey.A:
                                    newY = (j - 1 + map.GetLength(1)) % map.GetLength(1);
                                    break;
                                case ConsoleKey.D:
                                    newY = (j + 1) % map.GetLength(1);
                                    break;
                                case ConsoleKey.E:
                                    DeliteEnemies(map);
                                    break;
                            }

                        if (newMap[newX, newY] == '.' || newMap[newX, newY] == 'E')
                        {
                            newMap[newX, newY] = map[i, j];
                            newMap[i, j] = '.';
                        }
                        else if (newMap[newX, newY] == '$')
                        {
                            newMap[newX, newY] = map[i, j];
                            newMap[i, j] = '.';
                            hero.AddCoins(10);
                        }
                        else if (newMap[newX, newY] == '@')
                        {
                            newMap[newX, newY] = map[i, j];
                            newMap[i, j] = '.';
                            new Battle(hero, map, levelWorld);
                        }
                        else if (newMap[newX, newY] == 'T')
                        {
                            return;
                        }
                        _i = newX;
                        _j = newY;
                    }
                }
            }
            Array.Copy(newMap, map, map.Length);
        }

        /// <summary>
        /// метод перемещения врага по карте
        /// </summary>
        /// <param name="map">карта</param>
        /// <param name="_i">координата X врага</param>
        /// <param name="_j">координата Y врага</param>
        /// <param name="hero">игрок</param>
        /// <param name="ch">символ игрока</param>
        static public void MoveEnemy(ref char[,] map, int _i, int _j, Person hero, char ch)
        {
            char[,] newMap = new char[map.GetLength(0), map.GetLength(1)];
            Array.Copy(map, newMap, map.Length);

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == '@')
                    {
                        int diffX = _i - i;
                        int diffY = _j - j;

                        if (newMap[i + 1, j] == ch || newMap[i - 1, j] == ch || newMap[i, j + 1] == ch || newMap[i, j - 1] == ch)
                        {
                            new Battle(hero, map, hero.levelWorld);
                            newMap[i, j] = '.';
                        }
                        else
                        {
                            if (diffX > 0 && newMap[i + 1, j] == '.')
                            {
                                newMap[i + 1, j] = map[i, j];
                                newMap[i, j] = '.';
                            }
                            else if (diffX < 0 && newMap[i - 1, j] == '.')
                            {
                                newMap[i - 1, j] = map[i, j];
                                newMap[i, j] = '.';
                            }
                            else if (diffY > 0 && newMap[i, j + 1] == '.')
                            {
                                newMap[i, j + 1] = map[i, j];
                                newMap[i, j] = '.';
                            }
                            else if (diffY < 0 && newMap[i, j - 1] == '.')
                            {
                                newMap[i, j - 1] = map[i, j];
                                newMap[i, j] = '.';
                            }
                        }
                    }
                }
            }
            Array.Copy(newMap, map, map.Length);
        }
    }
}
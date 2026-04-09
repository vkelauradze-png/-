using System;
using System.Collections.Generic;
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
                    }
                    break;
            }
            WinORLose.WinI(mas);
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
        /// поддерживаем список врагов
        /// </summary>
        private static List<(int X, int Y)> enemies = new List<(int, int)>();

        static public void UpdateEnemiesList(char[,] map, char ch)
        {
            enemies.Clear();
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    if (map[i, j] == '@')
                        enemies.Add((i, j));
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
        static public void MovePerson(ref char[,] map, ConsoleKey key, Person hero,
    ref int levelWorld, char ch, int pnum)
        {
            char[,] newMap = new char[map.GetLength(0), map.GetLength(1)];
            Array.Copy(map, newMap, map.Length);

            int oldX = hero.PosX;
            int oldY = hero.PosY;
            int newX = oldX;
            int newY = oldY;

            if (pnum == 1)
                switch (key)
                {
                    case ConsoleKey.UpArrow: newX = (oldX - 1 + map.GetLength(0)) % map.GetLength(0); break;
                    case ConsoleKey.DownArrow: newX = (oldX + 1) % map.GetLength(0); break;
                    case ConsoleKey.LeftArrow: newY = (oldY - 1 + map.GetLength(1)) % map.GetLength(1); break;
                    case ConsoleKey.RightArrow: newY = (oldY + 1) % map.GetLength(1); break;
                    case ConsoleKey.Z: DeliteEnemies(map); return;
                }
            else if (pnum == 2)
                switch (key)
                {
                    case ConsoleKey.W: newX = (oldX - 1 + map.GetLength(0)) % map.GetLength(0); break;
                    case ConsoleKey.S: newX = (oldX + 1) % map.GetLength(0); break;
                    case ConsoleKey.A: newY = (oldY - 1 + map.GetLength(1)) % map.GetLength(1); break;
                    case ConsoleKey.D: newY = (oldY + 1) % map.GetLength(1); break;
                    case ConsoleKey.E: DeliteEnemies(map); return;
                }

            if (newMap[newX, newY] == '.' || newMap[newX, newY] == 'E')
            {
                newMap[newX, newY] = map[oldX, oldY];
                newMap[oldX, oldY] = '.';
                hero.PosX = newX;
                hero.PosY = newY;
            }
            else if (newMap[newX, newY] == '$')
            {
                newMap[newX, newY] = map[oldX, oldY];
                newMap[oldX, oldY] = '.';
                hero.AddCoins(10);
                hero.PosX = newX;
                hero.PosY = newY;
            }
            else if (newMap[newX, newY] == '@')
            {
                newMap[newX, newY] = map[oldX, oldY];
                newMap[oldX, oldY] = '.';
                new Battle(hero, map, levelWorld);
                hero.PosX = newX;
                hero.PosY = newY;
            }
            else if (newMap[newX, newY] == 'T')
            {
                return;
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
        static public void MoveEnemy(ref char[,] map, int playerX, int playerY, Person hero, char ch)
        {
            char[,] newMap = new char[map.GetLength(0), map.GetLength(1)];
            Array.Copy(map, newMap, map.Length);

            foreach (var enemy in enemies)
            {
                int i = enemy.X;
                int j = enemy.Y;

                if (Math.Abs(playerX - i) + Math.Abs(playerY - j) == 1)
                {
                    new Battle(hero, map, hero.levelWorld);
                    newMap[i, j] = '.';
                }
                else
                {
                    int newX = i, newY = j;
                    if (Math.Abs(playerX - i) > Math.Abs(playerY - j))
                        newX += (playerX > i) ? 1 : -1;
                    else
                        newY += (playerY > j) ? 1 : -1;

                    if (newX >= 0 && newX < map.GetLength(0) && newY >= 0 && newY < map.GetLength(1)
                        && newMap[newX, newY] == '.')
                    {
                        newMap[newX, newY] = map[i, j];
                        newMap[i, j] = '.';
                    }
                }
            }

            Array.Copy(newMap, map, map.Length);
            UpdateEnemiesList(map, ch); // обновляем список после движения
        }
    }
}
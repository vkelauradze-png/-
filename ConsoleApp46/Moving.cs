using System;
using System.Collections.Generic;

namespace ConsoleApp46
{
    /// <summary>
    /// класс для управления движением
    /// </summary>
    public class Moving
    {
        private static List<(int X, int Y)> enemies = new List<(int, int)>();

        /// <summary>
        /// метод для перемещения объектов по массиву в зависимости от нажатой клавиши
        /// </summary>
        /// <param name="mas">массив символов</param>
        /// <param name="a">направление перемещения (1 - вверх, 2 - вниз, 3 - влево, 4 - вправо)</param>
        static public void Move(char[,] mas, int a)
        {
            if (mas == null) throw new ArgumentNullException(nameof(mas));

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
            if (map == null) throw new ArgumentNullException(nameof(map));

            int x = map.GetLength(0) / 2;
            int y = map.GetLength(1) / 2;
            int radius = 1; // Базовый радиус

            for (int dx = -radius; dx <= radius; dx++)
            {
                for (int dy = -radius; dy <= radius; dy++)
                {
                    if (dx == 0 && dy == 0) continue;

                    int targetX = x + dx;
                    int targetY = y + dy;

                    if (targetX >= 0 && targetX < map.GetLength(0) &&
                        targetY >= 0 && targetY < map.GetLength(1))
                    {
                        if (map[targetX, targetY] == 'T')
                        {
                            map[targetX, targetY] = '.';
                        }
                    }
                }
            }
        }

        /// <summary>
        /// обновляет список врагов
        /// </summary>
        /// <param name="map">массив символов</param>
        /// <param name="ch">символ игрока</param>
        static public void UpdateEnemiesList(char[,] map, char ch)
        {
            if (map == null) return;

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
            if (map == null) throw new ArgumentNullException(nameof(map));

            int x = map.GetLength(0) / 2;
            int y = map.GetLength(1) / 2;
            int radius = 1; // Базовый радиус

            for (int dx = -radius; dx <= radius; dx++)
            {
                for (int dy = -radius; dy <= radius; dy++)
                {
                    if (dx == 0 && dy == 0) continue;

                    int targetX = x + dx;
                    int targetY = y + dy;

                    if (targetX >= 0 && targetX < map.GetLength(0) &&
                        targetY >= 0 && targetY < map.GetLength(1))
                    {
                        if (map[targetX, targetY] == (char)1)
                        {
                            map[targetX, targetY] = '.';
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
            if (map == null) throw new ArgumentNullException(nameof(map));

            int x = map.GetLength(0) / 2;
            int y = map.GetLength(1) / 2;
            int distance = 2; // Базовое расстояние взрыва

            for (int dx = -distance; dx <= distance; dx++)
            {
                for (int dy = -distance; dy <= distance; dy++)
                {
                    if (dx == 0 && dy == 0) continue;

                    int targetX = x + dx;
                    int targetY = y + dy;

                    if (targetX >= 0 && targetX < map.GetLength(0) &&
                        targetY >= 0 && targetY < map.GetLength(1))
                    {
                        map[targetX, targetY] = '.';
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
        /// <param name="ch">символ игрока на карте</param>
        /// <param name="pnum">номер игрока</param>
        static public void MovePerson(ref char[,] map, ConsoleKey key, Person hero,
            ref int levelWorld, char ch, int pnum)
        {
            // Проверка на null
            if (map == null) throw new ArgumentNullException(nameof(map));
            if (hero == null) throw new ArgumentNullException(nameof(hero));

            // Проверка индексов
            if (hero.PosX < 0 || hero.PosX >= map.GetLength(0) ||
                hero.PosY < 0 || hero.PosY >= map.GetLength(1))
            {
                // Сброс позиции в безопасное место
                hero.PosX = map.GetLength(0) / 2;
                hero.PosY = map.GetLength(1) / 2;
            }

            char[,] newMap = new char[map.GetLength(0), map.GetLength(1)];
            Array.Copy(map, newMap, map.Length);

            int oldX = hero.PosX;
            int oldY = hero.PosY;
            int newX = oldX;
            int newY = oldY;

            // Обработка движения в зависимости от номера игрока
            if (pnum == 1)
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        newX = (oldX - 1 + map.GetLength(0)) % map.GetLength(0);
                        break;
                    case ConsoleKey.DownArrow:
                        newX = (oldX + 1) % map.GetLength(0);
                        break;
                    case ConsoleKey.LeftArrow:
                        newY = (oldY - 1 + map.GetLength(1)) % map.GetLength(1);
                        break;
                    case ConsoleKey.RightArrow:
                        newY = (oldY + 1) % map.GetLength(1);
                        break;
                    case ConsoleKey.Z:
                        DeliteEnemies(map);
                        return;
                    case ConsoleKey.X:
                        Boomb(map);
                        return;
                    case ConsoleKey.Spacebar:
                        BreakTree(map);
                        return;
                }
            }
            else if (pnum == 2)
            {
                switch (key)
                {
                    case ConsoleKey.W:
                        newX = (oldX - 1 + map.GetLength(0)) % map.GetLength(0);
                        break;
                    case ConsoleKey.S:
                        newX = (oldX + 1) % map.GetLength(0);
                        break;
                    case ConsoleKey.A:
                        newY = (oldY - 1 + map.GetLength(1)) % map.GetLength(1);
                        break;
                    case ConsoleKey.D:
                        newY = (oldY + 1) % map.GetLength(1);
                        break;
                    case ConsoleKey.E:
                        DeliteEnemies(map);
                        return;
                    case ConsoleKey.Delete:
                        Boomb(map);
                        return;
                    case ConsoleKey.Backspace:
                        BreakTree(map);
                        return;
                }
            }

            // Проверка границ
            if (newX < 0 || newX >= map.GetLength(0) ||
                newY < 0 || newY >= map.GetLength(1))
            {
                return;
            }

            // Обработка взаимодействия с объектами
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
                Console.WriteLine($"💰 +10 монет! Всего: {hero.Coins}");
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
                // Дерево - нельзя пройти
                return;
            }

            Array.Copy(newMap, map, map.Length);
        }

        /// <summary>
        /// метод перемещения врага по карте
        /// </summary>
        /// <param name="map">карта</param>
        /// <param name="playerX">координата X игрока</param>
        /// <param name="playerY">координата Y игрока</param>
        /// <param name="hero">игрок</param>
        /// <param name="ch">символ игрока</param>
        static public void MoveEnemy(ref char[,] map, int playerX, int playerY, Person hero, char ch)
        {
            if (map == null) throw new ArgumentNullException(nameof(map));
            if (hero == null) throw new ArgumentNullException(nameof(hero));

            char[,] newMap = new char[map.GetLength(0), map.GetLength(1)];
            Array.Copy(map, newMap, map.Length);

            foreach (var enemy in enemies)
            {
                int i = enemy.X;
                int j = enemy.Y;

                // Проверка границ
                if (i < 0 || i >= map.GetLength(0) || j < 0 || j >= map.GetLength(1))
                    continue;

                // Если враг рядом с игроком - битва
                if (Math.Abs(playerX - i) + Math.Abs(playerY - j) == 1)
                {
                    new Battle(hero, map, hero.levelWorld);
                    newMap[i, j] = '.';
                }
                else
                {
                    int newX = i, newY = j;

                    // Движение к игроку
                    if (Math.Abs(playerX - i) > Math.Abs(playerY - j))
                        newX += (playerX > i) ? 1 : -1;
                    else
                        newY += (playerY > j) ? 1 : -1;

                    // Проверка возможности движения
                    if (newX >= 0 && newX < map.GetLength(0) &&
                        newY >= 0 && newY < map.GetLength(1) &&
                        newMap[newX, newY] == '.')
                    {
                        newMap[newX, newY] = map[i, j];
                        newMap[i, j] = '.';
                    }
                }
            }

            Array.Copy(newMap, map, map.Length);
            UpdateEnemiesList(map, ch);
        }
    }
}
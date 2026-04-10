using System;
using System.Threading;

namespace ConsoleApp46
{
    /// <summary>
    /// класс, представляющий событие в игре
    /// </summary>
    public class Ivent
    {
        Person hero;

        /// <summary>
        /// создает новый экземпляр класса Event с заданным персонажем
        /// </summary>
        /// <param name="Hero">персонаж</param>
        public Ivent(Person Hero)
        {
            hero = Hero ?? throw new ArgumentNullException(nameof(Hero));
        }

        /// <summary>
        /// проверяет событие и выполняет соответствующее действие
        /// </summary>
        public bool CheckIvent(ref int levelWorld, char[,] mas, char[,] mas2, int A, int B, char ch, int pnum, Person hero2)
        {
            if (mas == null) return false;
            if (A < 0 || A >= mas.GetLength(0) || B < 0 || B >= mas.GetLength(1)) return false;

            char key = mas[A, B];

            switch (key)
            {
                case (char)1:
                    Battle battle = new Battle(hero, mas, levelWorld);
                    break;
                case (char)3:
                    Heart heart = new Heart(hero, mas);
                    break;
                case 'U':
                    levelWorld++;
                    Portal portal = new Portal(hero, mas);
                    break;
                case (char)19:
                    Forge forge = new Forge(hero);
                    break;
                case (char)0177:
                    return false;
                case 'T':
                    return false;
                case 'R':
                    if (hero2 != null && !hero2.InRoom)
                    {
                        hero.InRoom = true;
                        new Room(hero, ch, pnum, hero2, mas2);
                        hero.InRoom = false;
                    }
                    else
                        return false;
                    break;
                default:
                    break;
            }
            return true;
        }
    }

    /// <summary>
    /// класс для события битвы
    /// </summary>
    public class Battle : Ivent
    {
        private static readonly Random _globalRandom = new Random();
        [ThreadStatic]
        private static Random _localRandom;

        private static Random GetRandom()
        {
            if (_localRandom == null)
            {
                lock (_globalRandom)
                {
                    _localRandom = new Random(_globalRandom.Next());
                }
            }
            return _localRandom;
        }

        public Battle(Person Hero, char[,] mas, int levelWorld) : base(Hero)
        {
            if (Hero == null) return;

            Console.Clear();
            Person Enemy = new Person(levelWorld * 10, "Враг");
            Random rnd = GetRandom();

            while (Enemy.HP > 0 && Hero.HP > 0)
            {
                int Shot = rnd.Next(10);
                Enemy.ReceiveDamage(Shot);
                Shot = rnd.Next(10);
                Hero.ReceiveDamage(Shot + levelWorld * 5);
                Thread.Sleep(100);
            }

            if (Enemy.HP <= 0 && Hero.HP > 0)
            {
                Hero.AddCoins(rnd.Next(100));
                Console.WriteLine($"Победа! Получено {rnd.Next(100)} монет");
                Thread.Sleep(1500);
            }
            else if (Hero.HP <= 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Поражение в бою!");
                Console.ResetColor();
                Thread.Sleep(1500);
                WinORLose.Losei();
            }
        }
    }

    /// <summary>
    /// класс события для кузницы
    /// </summary>
    public class Forge : Ivent
    {
        public Forge(Person Hero) : base(Hero)
        {
            if (Hero == null) return;

            Console.Clear();
            Console.WriteLine("========== КУЗНИЦА ==========");
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Увеличить радиус удаления врагов - 250 монет (Нажмите 1)");
            Console.WriteLine("2. Увеличить радиус ломания деревьев - 350 монет (Нажмите 2)");
            Console.WriteLine("3. Увеличить взрыв бомбы - 400 монет (Нажмите 3)");
            Console.WriteLine("Для выхода нажмите Enter");
            Console.WriteLine($"Оставшиеся деньги: {Hero.Coins}");
            Console.WriteLine("==============================");

            ConsoleKeyInfo key;
            string errorMessage = string.Empty;

            while (true)
            {
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    Console.SetCursorPosition(0, 25);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMessage.PadRight(50));
                    Console.ResetColor();
                }

                key = Console.ReadKey(true);
                Console.Clear();

                if (key.Key == ConsoleKey.Enter)
                    break;

                try
                {
                    ProcessKeyPress(key, Hero);
                    errorMessage = string.Empty;
                    Console.WriteLine("========== КУЗНИЦА ==========");
                    Console.WriteLine("Выберите действие:");
                    Console.WriteLine("1. Увеличить радиус удаления врагов - 250 монет (Нажмите 1)");
                    Console.WriteLine("2. Увеличить радиус ломания деревьев - 350 монет (Нажмите 2)");
                    Console.WriteLine("3. Увеличить взрыв бомбы - 400 монет (Нажмите 3)");
                    Console.WriteLine("Для выхода нажмите Enter");
                    Console.WriteLine($"Оставшиеся деньги: {Hero.Coins}");
                    Console.WriteLine("==============================");
                }
                catch (InvalidKeyException ex)
                {
                    errorMessage = ex.Message;
                }
                catch (Exception ex)
                {
                    errorMessage = $"Ошибка: {ex.Message}";
                }
            }
            Console.Clear();
        }

        static void ProcessKeyPress(ConsoleKeyInfo CKI, Person Hero)
        {
            if (Hero == null) throw new ArgumentNullException(nameof(Hero));
            if (Hero.Coins < 0) throw new InvalidOperationException("Количество монет не может быть отрицательным");

            switch (CKI.Key)
            {
                case ConsoleKey.Enter:
                    return;
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    if (Hero.Coins >= 250)
                    {
                        if (Hero.CountBoombm >= 3)
                        {
                            Console.WriteLine("Достигнут максимальный уровень улучшения!");
                        }
                        else
                        {
                            Hero.Boombm();
                            Hero.DeliteCoins(250);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Радиус удаления врагов увеличен! Текущий радиус = {Hero.CountBoombm}");
                            Console.WriteLine($"Оставшиеся деньги: {Hero.Coins}");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Недостаточно монет! Нужно 250, у вас {Hero.Coins}");
                        Console.ResetColor();
                    }
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    if (Hero.Coins >= 350)
                    {
                        if (Hero.CountBreakTreesm >= 3)
                        {
                            Console.WriteLine("Достигнут максимальный уровень улучшения!");
                        }
                        else
                        {
                            Hero.BreakTreesm();
                            Hero.DeliteCoins(350);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Радиус ломания деревьев увеличен! Текущий радиус = {Hero.CountBreakTreesm}");
                            Console.WriteLine($"Оставшиеся деньги: {Hero.Coins}");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Недостаточно монет! Нужно 350, у вас {Hero.Coins}");
                        Console.ResetColor();
                    }
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    if (Hero.Coins >= 400)
                    {
                        if (Hero.CountDeliteEnemiesm >= 3)
                        {
                            Console.WriteLine("Достигнут максимальный уровень улучшения!");
                        }
                        else
                        {
                            Hero.DeliteEnemiesm();
                            Hero.DeliteCoins(400);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Сила удаления врагов увеличена! Текущий уровень = {Hero.CountDeliteEnemiesm}");
                            Console.WriteLine($"Оставшиеся деньги: {Hero.Coins}");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Недостаточно монет! Нужно 400, у вас {Hero.Coins}");
                        Console.ResetColor();
                    }
                    break;
                default:
                    throw new InvalidKeyException($"Ошибка: клавиша '{CKI.Key}' недопустима в кузнице!");
            }
        }
    }

    /// <summary>
    /// класс события для здоровья
    /// </summary>
    public class Heart : Ivent
    {
        public Heart(Person Hero, char[,] mas) : base(Hero)
        {
            if (Hero == null) return;

            Console.Clear();
            int healAmount = 10;
            Hero.Heal(healAmount);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"❤️ Вы восстановили {healAmount} HP! Текущее здоровье: {Hero.HP}/{Hero.MaxHP}");
            Console.ResetColor();
            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// класс события для портала
    /// </summary>
    public class Portal : Ivent
    {
        public Portal(Person Hero, char[,] mas) : base(Hero)
        {
            if (Hero == null || mas == null) return;

            int coinsFound = 0;
            for (int i = 0; i < mas.GetLength(0); i++)
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    if (mas[i, j] == (char)3)
                    {
                        Hero.AddCoins(100);
                        coinsFound += 100;
                    }
                }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"🌀 Вы прошли через портал на уровень {Hero.levelWorld}!");
            if (coinsFound > 0)
            {
                Console.WriteLine($"💰 Найдено {coinsFound} монет!");
            }
            Console.ResetColor();
            Thread.Sleep(1500);

            Hero.GetCurrentHP();
            Map.Array(mas, Hero);
        }
    }
}
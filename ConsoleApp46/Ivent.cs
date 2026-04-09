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
            hero = Hero;
        }

        /// <summary>
        /// проверяет событие и выполняет соответствующее действие
        /// </summary>
        /// <param name="levelWorld">уровень мира</param>
        /// <param name="mas">массив с символами</param>
        /// <param name="mas2">дополнительный массив символов</param>
        /// <param name="A">координата A</param>
        /// <param name="B">координата B</param>\
        /// <param name="ch">символ</param>
        /// <param name="pnum">номер игрока</param>
        /// <param name="hero2">второй игрок</param>
        /// <returns>возвращает true, если событие обработано, иначе false</returns>
        public bool CheckIvent(ref int levelWorld, char[,] mas, char[,] mas2, int A, int B, char ch, int pnum, Person hero2)
        {
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
                    if (!hero2.InRoom)
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
        /// <summary>
        /// создает новый экземпляр класса Battle с заданным персонажем, массивом символов и уровнем мира
        /// </summary>
        /// <param name="Hero">персонаж</param>
        /// <param name="mas">массив символов</param>
        /// <param name="levelWorld">уровень мира</param>
        public Battle(Person Hero, char[,] mas, int levelWorld) : base(Hero)
        {
            Console.Clear();
            Person Enemy = new Person(levelWorld * 10);
            Random rnd = new Random();

            while (Enemy.HP > 0 && Hero.HP > 0)
            {
                int Shot = rnd.Next(10);
                Enemy.ReceiveDamage(Shot);
                Shot = rnd.Next(10);
                Hero.ReceiveDamage(Shot + levelWorld * 5);
            }
            if (Enemy.HP < Hero.HP)
                Hero.AddCoins(rnd.Next(100));
            else
            {
                Console.Clear();
                Console.WriteLine($"Поражение");
            }
        }
    }

    /// <summary>
    /// класс события для кузницы
    /// </summary>
    public class Forge : Ivent
    {

        /// <summary>
        /// создает новый экземпляр класса Forge с заданным персонажем
        /// </summary>
        /// <param name="Hero">персонаж</param>
        public Forge(Person Hero) : base(Hero)
        {
            Console.WriteLine("Выберите действие");
            Console.WriteLine("1. Увеличить радиус удаления врагов - 250 робаксов(Чтобы ей воспользоваться нажмите Z)");
            Console.WriteLine("2. Увеличить радиус ломания деревьев - 350 робаксов(Чтобы ей воспользоваться нажмите SpaceBar)");
            Console.WriteLine("3. Увеличить взрыв бомбы - 300 робаксов(Чтобы ей воспользоваться нажмите X)");
            Console.WriteLine("Для выхода нажмите Enter");
            Console.WriteLine($"Оставшиеся деньги {Hero.Coins}");

            ConsoleKeyInfo key;
            string errorMessage = string.Empty;

            while (true)
            {
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    Console.SetCursorPosition(0, 25);
                    Console.WriteLine(errorMessage);
                }

                key = Console.ReadKey();
                Console.Clear();
                if (key.Key == ConsoleKey.Enter)
                    break;
                try
                {
                    ProcessKeyPress(key, Hero);
                    errorMessage = string.Empty;
                }
                catch (InvalidKeyException ex)
                {
                    errorMessage = ex.Message;
                }
            }
            Console.Clear();
        }

        /// <summary>
        /// обработка нажатия клавиши
        /// </summary>
        /// <param name="CKI">нажатая клавиша</param>
        /// <param name="Hero">персонаж</param>
        static void ProcessKeyPress(ConsoleKeyInfo CKI, Person Hero)
        {
            switch (CKI.Key)
            {
                case ConsoleKey.Enter:
                    return;
                case ConsoleKey.NumPad1:
                    if (Hero.Coins > 250)
                    {
                        if (Person.CountBoombm == 3)
                        {
                            Console.WriteLine("А больше нельзя увеличивать");
                        }
                        else
                        {
                            Hero.Boombm();
                            Hero.DeliteCoins(250);
                            Console.WriteLine($"Радиус удаления врагов увеличен, текущий радиус = {Hero.DeliteEnemies}");
                            Console.WriteLine($"Оставшиеся деньги {Hero.Coins}");
                        }
                    }
                    else
                        Console.WriteLine("Недостаточно деняк");
                    break;
                case ConsoleKey.NumPad2:
                    if (Hero.Coins > 350)
                    {
                        if (Person.CountBreakTreesm == 3)
                        {
                            Console.WriteLine("А больше нельзя увеличивать");
                        }
                        else
                        {
                            Hero.BreakTreesm();
                            Hero.DeliteCoins(350);
                            Console.WriteLine($"Радиус ломания деревьев увеличен, текущий радиус ломания = {Hero.BreakTrees}");
                            Console.WriteLine($"Оставшиеся деньги {Hero.Coins}");
                        }
                    }
                    else
                        Console.WriteLine("Недостаточно деняк");
                    break;
                case ConsoleKey.NumPad3:
                    if (Hero.Coins > 400)
                    {
                        if (Person.CountDeliteEnemiesm == 3)
                        {
                            Console.WriteLine("А больше нельзя увеличивать");
                        }
                        else
                        {
                            Hero.DeliteEnemiesm();
                            Hero.DeliteCoins(400);
                            Console.WriteLine($"Сила откидывания врагов, текущая сила = {Hero.DeliteEnemies}");
                            Console.WriteLine($"Оставшиеся деньги {Hero.Coins}");
                        }
                    }
                    else
                        Console.WriteLine("Недостаточно деняк");
                    break;
                default:
                    throw new InvalidKeyException("Ошибка: нажата недопустимая клавиша!");
            }
        }
    }

    /// <summary>
    /// класс события для здоровья
    /// </summary>
    public class Heart : Ivent
    {

        /// <summary>
        /// создает новый экземпляр класса Heart с заданным персонажем и массивом символов
        /// </summary>
        /// <param name="Hero">персонаж</param>
        /// <param name="mas">массив символов</param>
        public Heart(Person Hero, char[,] mas) : base(Hero)
        {
            Console.Clear();
            Hero.Heal(10);
        }
    }

    /// <summary>
    /// класс события для портала
    /// </summary>
    public class Portal : Ivent
    {

        /// <summary>
        /// создает новый экземпляр класса Portal с заданным персонажем и массивом символов
        /// </summary>
        /// <param name="Hero">персонаж</param>
        /// <param name="mas">массив символов</param>
        public Portal(Person Hero, char[,] mas) : base(Hero)
        {
            for (int i = 0; i < mas.GetLength(0); i++)
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    if (mas[i, j] == (char)3)
                        Hero.AddCoins(100);
                }
            Hero.GetCurrentHP();
            Map.Array(mas, Hero);
        }
    }
}

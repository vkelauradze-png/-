using System;

namespace ConsoleApp46
{

    /// <summary>
    /// класс, представляющий персонажа игры
    /// </summary>
    public class Person
    {

        /// <summary>
        /// уровень мира
        /// </summary>
        public int levelWorld = 1;

        /// <summary>
        /// флаг указывающий, находится ли персонаж в комнате
        /// </summary>
        public bool InRoom = false;
        /// <summary>
        /// счетчик уровеня радиуса ломания деревьев
        /// </summary>
        public static int CountBreakTreesm = 1;

        /// <summary>
        /// счетчик уровеня радиуса взрыва бомбы
        /// </summary>
        public static int CountBoombm = 1;

        /// <summary>
        /// счетчик уровеня радиуса удаления врагов
        /// </summary>
        public static int CountDeliteEnemiesm = 1;

        /// <summary>
        /// максимальное здоровье персонажа
        /// </summary>
        public int MaxHP { get; private set; }

        /// <summary>
        /// текущее здоровье персонажа
        /// </summary>
        public int HP { get; private set; }

        /// <summary>
        /// Мощность бомбы
        /// </summary>
        public int Boomb { get; private set; }

        /// <summary>
        /// Мощность ломания деревьев
        /// </summary>
        public int BreakTrees { get; private set; }

        /// <summary>
        /// Мощность удаления врагов
        /// </summary>
        public int DeliteEnemies { get; private set; }

        /// <summary>
        /// количество монет у персонажа
        /// </summary>
        public int Coins { get; private set; }

        /// <summary>
        /// имя персонажа
        /// </summary>
        public string NamePerson { get; private set; }

        /// <summary>
        /// конструктор персонажа
        /// </summary>
        /// <param name="HP">здоровье персонажа</param>
        /// <param name="Name">имя персонажа</param>
        public Person(int HP = 100, string Name = "Враг")
        {
            NamePerson = Name;
            this.HP = HP;
            MaxHP = HP;
            Boomb = 0;
            BreakTrees = 0;
            DeliteEnemies = 0;
            Coins = 0;
        }
        /// <summary>
        /// сила игрока
        /// </summary>
        public int Strength { get; private set; } = 10;
        public void UpgradeStrength()
        {
            Strength += 2;
        }
        /// <summary>
        /// позиция игрока
        /// </summary>
        public int PosX { get; set; }
        public int PosY { get; set; }
        /// <summary>
        /// увеличивает уровень навыка "взрыва" бомбой
        /// </summary>
        public void Boombm()
        {
            CountBoombm++;
        }

        /// <summary>
        /// увеличивает уровень навыка "разрушения" деревьев
        /// </summary>
        public void BreakTreesm()
        {
            CountBreakTreesm++;
        }

        /// <summary>
        /// увеличивает уровень навыка "удаления" врагов
        /// </summary>
        public void DeliteEnemiesm()
        {
            CountDeliteEnemiesm++;
        }

        /// <summary>
        /// получение урона персонажем
        /// </summary>
        /// <param name="damage">количество урона</param>
        public void ReceiveDamage(int damage)
        {
            HP -= damage;
        }

        /// <summary>
        /// добавление монет персонажу
        /// </summary>
        /// <param name="amount">количество монет</param>
        public void AddCoins(int amount)
        {
            Coins += amount;
        }

        /// <summary>
        /// удаление монет у персонажа
        /// </summary>
        /// <param name="amount">количество монет</param>
        public void DeliteCoins(int amount)
        {
            Coins -= amount;
        }

        /// <summary>
        /// увеличение здоровья персонажа
        /// </summary>
        /// <param name="amount">количество здоровья</param>
        public void IncreaseHP(int amount)
        {
            HP += amount;
        }

        /// <summary>
        /// увеличение максимального здоровья персонажа
        /// </summary>
        /// <param name="amount">количество здоровья</param>
        public void IncreaseMaxHP(int amount)
        {
            MaxHP += amount;
        }

        /// <summary>
        /// получить максимальное здоровье персонажа
        /// </summary>
        /// <returns>максимальное здоровье персонажа</returns>
        public int GetMaxHP()
        {
            return MaxHP;
        }

        /// <summary>
        /// получить текущее здоровье персонажа
        /// </summary>
        /// <returns>текущее здоровье персонажа</returns>
        public int GetCurrentHP()
        {
            return HP;
        }
        public void Heal(int amount)
        {
            HP += amount;
            if (HP > MaxHP)
                HP = MaxHP;
        }
        /// <summary>
        /// вывод характеристик персонажа на консоль
        /// </summary>
        /// <param name="a">Номер позиции персонажа</param>
        public void GetCharacter(int a)
        {
            Console.SetCursorPosition((a - 1) * 60, 29);
            Console.WriteLine($"Имя героя = {NamePerson}");
            Console.SetCursorPosition((a - 1) * 60, 29);
            Console.WriteLine($"Здоровье = {HP}");
            Console.SetCursorPosition((a - 1) * 60, 29);
            Console.WriteLine($"MAX Здоровье = {MaxHP}");
            Console.SetCursorPosition((a - 1) * 60, 29);
            Console.WriteLine($"Деняк = {Coins}");
            Console.SetCursorPosition((a - 1) * 60, 29);
            Console.WriteLine($"Уровень мира = {levelWorld}");
        }
    }
}
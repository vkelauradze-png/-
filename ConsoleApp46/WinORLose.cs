using System;

namespace ConsoleApp46
{
    /// <summary>
    /// класс, представляющий выигрыш или поражение
    /// </summary>
    public class WinORLose
    {
        private static bool hasEnemiesCache = true;
        private static int lastCheckX = -1, lastCheckY = -1;

        static public bool WinI(char[,] mas)
        {
            if (hasEnemiesCache)
            {
                hasEnemiesCache = false;
                for (int i = 0; i < mas.GetLength(0); i++)
                {
                    for (int j = 0; j < mas.GetLength(1); j++)
                    {
                        if (mas[i, j] == (char)1)
                        {
                            hasEnemiesCache = true;
                            return false;
                        }
                        if (mas[i, j] == 'U')
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < mas.GetLength(0); i++)
                    for (int j = 0; j < mas.GetLength(1); j++)
                        if (mas[i, j] == (char)1)
                            return false;
            }

            int centerX = mas.GetLength(0) / 2;
            int centerY = mas.GetLength(1) / 2;
            mas[centerX, centerY] = 'U';
            hasEnemiesCache = false;
            return true;
        }

        static public void OnEnemyKilled()
        {
            hasEnemiesCache = true;
        }

        /// <summary>
        /// обработка поражения игрока
        /// </summary>
        static public void Losei()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("========================================");
            Console.WriteLine("              ПОРАЖЕНИЕ!                ");
            Console.WriteLine("========================================");
            Console.ResetColor();
            Console.WriteLine("\nИгра окончена.");
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}
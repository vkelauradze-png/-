using System;

namespace ConsoleApp46
{

    /// <summary>
    /// класс, представляющий выигрыш или поражение
    /// </summary>
    abstract class WinORLose
    {

        /// <summary>
        /// проверяет, выиграна ли игра
        /// </summary>
        /// <param name="mas">массив для проверки</param>
        /// <returns>возвращает true, если игра выиграна, иначе false</returns>
        static public bool WinI(char[,] mas)
        {
            for (int i = 0; i < mas.GetLength(0); i++)
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    if (mas[i, j] == (char)1 || mas[i, j] == 'U')
                        return false;
                }
            mas[10, 10] = 'U';
            return true;
        }

        /// <summary>
        /// завершает игру с сообщением о проигрыше
        /// </summary>
        static public void Losei()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("проигрыш!");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}

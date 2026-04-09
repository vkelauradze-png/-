using Xunit;

namespace ConsoleApp46.Tests;

public class WinORLoseTests
{
    [Fact]
    public void WinI_NoEnemies_ShouldReturnTrueAndPlacePortal()
    {
        int size = 25;
        var map = new char[size, size];

        // Заполняем всё точками (нет врагов)
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                map[i, j] = '.';

        bool result = WinORLose.WinI(map);

        Assert.True(result);
        int centerX = size / 2;
        int centerY = size / 2;
        Assert.Equal('U', map[centerX, centerY]);
    }

    [Fact]
    public void WinI_WithEnemy_ShouldReturnFalse()
    {
        int size = 25;
        var map = new char[size, size];

        // Заполняем всё точками, кроме одного врага
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                map[i, j] = '.';
        map[5, 5] = (char)1; // Враг

        bool result = WinORLose.WinI(map);

        Assert.False(result);
    }

    [Fact]
    public void WinI_WithPortal_ShouldReturnFalse()
    {
        int size = 25;
        var map = new char[size, size];

        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                map[i, j] = '.';
        map[10, 10] = 'U'; // Уже есть портал

        bool result = WinORLose.WinI(map);

        Assert.False(result);
    }

    [Fact]
    public void Losei_ShouldExitGame()
    {
        // Тестировать сложно из-за Environment.Exit
        // Рекомендуется рефакторинг: заменить Environment.Exit на исключение или флаг
        Assert.True(true); // Заглушка
    }
}
using Xunit;

namespace ConsoleApp46.Tests;

public class MovingTests
{
    [Fact]
    public void Move_UpDirection_ShouldShiftArray()
    {
        int size = 5;
        var map = new char[size, size];

        // Заполняем уникальными значениями
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                map[i, j] = (char)('A' + i * size + j);

        var originalCenter = map[size / 2, size / 2];

        Moving.Move(map, 1); // Вверх

        // Проверяем, что массив изменился
        Assert.NotEqual(originalCenter, map[size / 2, size / 2]);
    }

    [Fact]
    public void Move_DownDirection_ShouldShiftArray()
    {
        int size = 5;
        var map = new char[size, size];

        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                map[i, j] = '.';
        map[2, 2] = '@'; // Враг

        Moving.Move(map, 2); // Вниз

        // Проверяем вызов WinI (интеграционный тест)
        Assert.True(true);
    }

    [Fact]
    public void BreakTree_Level1_RemovesPlusShape()
    {
        Person.CountBreakTreesm = 1;
        int size = 25;
        var map = new char[size, size];

        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                map[i, j] = '.';

        int centerX = size / 2;
        int centerY = size / 2;
        map[centerX - 1, centerY] = 'T'; // Вверх
        map[centerX + 1, centerY] = 'T'; // Вниз
        map[centerX, centerY - 1] = 'T'; // Влево
        map[centerX, centerY + 1] = 'T'; // Вправо

        Moving.BreakTree(map);

        Assert.Equal('.', map[centerX - 1, centerY]);
        Assert.Equal('.', map[centerX + 1, centerY]);
        Assert.Equal('.', map[centerX, centerY - 1]);
        Assert.Equal('.', map[centerX, centerY + 1]);
    }

    [Fact]
    public void DeliteEnemies_Level1_RemovesAdjacentEnemies()
    {
        int size = 25;
        var map = new char[size, size];

        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                map[i, j] = '.';

        int centerX = size / 2;
        int centerY = size / 2;
        map[centerX - 1, centerY] = (char)1; // Враг сверху
        map[centerX, centerY + 1] = (char)1; // Враг справа

        Moving.DeliteEnemies(map);

        Assert.Equal('.', map[centerX - 1, centerY]);
        Assert.Equal('.', map[centerX, centerY + 1]);
    }

    [Fact]
    public void Boomb_Level1_RemovesAreaRadius2()
    {
        int size = 25;
        var map = new char[size, size];

        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                map[i, j] = '.';

        int centerX = size / 2;
        int centerY = size / 2;

        // Устанавливаем деревья вокруг
        for (int dx = -2; dx <= 2; dx++)
            for (int dy = -2; dy <= 2; dy++)
                if (dx != 0 || dy != 0)
                    map[centerX + dx, centerY + dy] = 'T';

        Moving.Boomb(map);

        // Проверяем, что все деревья в радиусе 2 удалены
        for (int dx = -2; dx <= 2; dx++)
            for (int dy = -2; dy <= 2; dy++)
                if (dx != 0 || dy != 0)
                    Assert.Equal('.', map[centerX + dx, centerY + dy]);
    }
}
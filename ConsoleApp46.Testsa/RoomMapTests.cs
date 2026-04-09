using Xunit;

namespace ConsoleApp46.Tests;

public class RoomMapTests
{
    [Fact]
    public void Array_ShouldCreateValidMap()
    {
        int size = 25;
        var map = new char[size, size];

        RoomMap.Array(map, '0');

        // Проверяем границы
        for (int i = 0; i < size; i++)
        {
            Assert.Equal('#', map[0, i]);      // Верхняя стена
            Assert.Equal('#', map[size - 1, i]); // Нижняя стена
            Assert.Equal('#', map[i, 0]);      // Левая стена
            Assert.Equal('#', map[i, size - 1]); // Правая стена
        }

        // Проверяем выход и позицию игрока
        Assert.Equal('E', map[1, 23]);
        Assert.Equal('0', map[23, 1]);
    }

    [Fact]
    public void Array_WithDifferentSymbol_ShouldPlaceSymbolCorrectly()
    {
        int size = 25;
        var map = new char[size, size];

        RoomMap.Array(map, '8');

        Assert.Equal('8', map[23, 1]);
    }
}
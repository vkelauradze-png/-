using Xunit;

namespace ConsoleApp46.Tests;

public class IventTests
{
    [Fact]
    public void CheckIvent_WithTree_ShouldReturnFalse()
    {
        int size = 25;
        var map = new char[size, size];
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                map[i, j] = '.';
        map[12, 12] = 'T'; // Дерево в центре

        var person = new Person(100, "Тестер");
        var ivent = new Ivent(person);

        bool result = ivent.CheckIvent(ref person.levelWorld, map, null, 12, 12, '0', 1, person);

        Assert.False(result);
    }
}
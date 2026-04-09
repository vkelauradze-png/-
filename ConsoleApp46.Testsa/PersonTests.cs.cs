using Xunit;

namespace ConsoleApp46.Tests;

public class PersonTests
{
    public PersonTests()
    {
        Person.CountBreakTreesm = 1;
        Person.CountBoombm = 1;
        Person.CountDeliteEnemiesm = 1;
    }

    [Fact]
    public void Constructor_WithName_ShouldSetCorrectName()
    {
        var person = new Person(100, "Тестер");
        Assert.Equal("Тестер", person.NamePerson);
    }

    [Fact]
    public void Constructor_DefaultHP_ShouldBe100()
    {
        var person = new Person(Name: "Тестер");
        Assert.Equal(100, person.HP);
        Assert.Equal(100, person.MaxHP);
    }

    [Fact]
    public void ReceiveDamage_ShouldReduceHP()
    {
        var person = new Person(100, "Тестер");
        person.ReceiveDamage(30);
        Assert.Equal(70, person.HP);
    }

    [Fact]
    public void ReceiveDamage_HPShouldNotGoNegative()
    {
        var person = new Person(100, "Тестер");
        person.ReceiveDamage(150);
        Assert.Equal(-50, person.HP);
    }

    [Fact]
    public void Heal_ShouldIncreaseHP()
    {
        var person = new Person(100, "Тестер");
        person.ReceiveDamage(30);
        person.Heal(20);
        Assert.Equal(90, person.HP);
    }

    [Fact]
    public void Heal_ShouldNotExceedMaxHP()
    {
        var person = new Person(100, "Тестер");
        person.ReceiveDamage(30);
        person.Heal(50);
        Assert.Equal(100, person.HP);
    }

    [Fact]
    public void AddCoins_ShouldIncreaseCoins()
    {
        var person = new Person(100, "Тестер");
        person.AddCoins(50);
        Assert.Equal(50, person.Coins);
    }

    [Fact]
    public void DeliteCoins_ShouldDecreaseCoins()
    {
        var person = new Person(100, "Тестер");
        person.AddCoins(100);
        person.DeliteCoins(30);
        Assert.Equal(70, person.Coins);
    }

    [Fact]
    public void IncreaseHP_ShouldAddHealth()
    {
        var person = new Person(100, "Тестер");
        person.ReceiveDamage(40);
        person.IncreaseHP(20);
        Assert.Equal(80, person.HP);
    }

    [Fact]
    public void IncreaseMaxHP_ShouldIncreaseMaximum()
    {
        var person = new Person(100, "Тестер");
        person.IncreaseMaxHP(50);
        Assert.Equal(150, person.MaxHP);
    }

    [Fact]
    public void GetMaxHP_ShouldReturnMaxHP()
    {
        var person = new Person(100, "Тестер");
        Assert.Equal(100, person.GetMaxHP());
    }

    [Fact]
    public void GetCurrentHP_ShouldReturnCurrentHP()
    {
        var person = new Person(100, "Тестер");
        person.ReceiveDamage(30);
        Assert.Equal(70, person.GetCurrentHP());
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    [InlineData(3, 4)]
    public void Boombm_ShouldIncrementLevel(int increments, int expected)
    {
        var person = new Person(100, "Тестер");
        for (int i = 0; i < increments; i++)
            person.Boombm();
        Assert.Equal(expected, Person.CountBoombm);
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    [InlineData(3, 4)]
    public void BreakTreesm_ShouldIncrementLevel(int increments, int expected)
    {
        var person = new Person(100, "Тестер");
        for (int i = 0; i < increments; i++)
            person.BreakTreesm();
        Assert.Equal(expected, Person.CountBreakTreesm);
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    [InlineData(3, 4)]
    public void DeliteEnemiesm_ShouldIncrementLevel(int increments, int expected)
    {
        var person = new Person(100, "Тестер");
        for (int i = 0; i < increments; i++)
            person.DeliteEnemiesm();
        Assert.Equal(expected, Person.CountDeliteEnemiesm);
    }
}
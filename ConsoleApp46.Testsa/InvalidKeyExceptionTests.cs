namespace ConsoleApp46.Tests;

public class InvalidKeyExceptionTests
{
    [Fact]
    public void Constructor_ShouldSetMessage()
    {
        string expectedMessage = "Тестовое сообщение";
        var exception = new InvalidKeyException(expectedMessage);

        Assert.Equal(expectedMessage, exception.Message);
    }
}
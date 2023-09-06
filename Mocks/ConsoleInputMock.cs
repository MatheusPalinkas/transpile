namespace Transpilador.Mocks;

public class ConsoleInputMock : IDisposable
{
    private TextReader originalInput;
    private TextReader customInput;

    public ConsoleInputMock(params string[] outputsMock)
    {
        originalInput = Console.In;
        customInput = new InputReaderMock(outputsMock);
        Console.SetIn(customInput);
    }

    public void Dispose()
    {
        Console.SetIn(originalInput);
        customInput.Dispose();
    }
}
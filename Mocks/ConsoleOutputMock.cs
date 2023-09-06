namespace Transpilador.Mocks;

public class ConsoleOutputMock : IDisposable
{
    private readonly StringWriter consoleOutWriter;
    private readonly TextWriter originalConsoleOut;

    public ConsoleOutputMock()
    {
        consoleOutWriter = new StringWriter();
        originalConsoleOut = Console.Out;
        Console.SetOut(consoleOutWriter);
    }

    public string GetOutput()
    {
        return consoleOutWriter.ToString();
    }

    public void Dispose()
    {
        Console.SetOut(originalConsoleOut);
        consoleOutWriter.Dispose();
    }
}

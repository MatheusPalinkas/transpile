namespace Transpilador.Mocks;

public class InputReaderMock : TextReader
{
    private string[] _outputsMock;

    private int index = 0;
    private int qtdParams { get => _outputsMock.Length; }

    public InputReaderMock(params string[] outputsMock)
    {
        _outputsMock = outputsMock;
    }
    public override string? ReadLine()
    {
        if (index + 1 > qtdParams)
            index = 0;

        return _outputsMock[index++];
    }
}

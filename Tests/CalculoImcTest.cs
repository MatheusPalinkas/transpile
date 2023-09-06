using System.Reflection;
using Transpilador.Mocks;
using Xunit;
using Xunit.Sdk;

namespace Transpilador.Tests;

public class CalculoImcTest : TestBase
{
    public CalculoImcTest(MethodInfo program) : base(program)
    {
    }

    public override void Run()
    {
        //Arrange
        const double expectImc = 25.221;
        const string expectNome = "João Paulo";
        setInputValuesMock("20", "70", "1,70", "João Paulo");

        //Act
        executeProgram();

        //Assert

        // Deve retornar o nome 
        if(!ContainsOutput(expectNome))
            Console.WriteLine("Não retornou o nome: " + expectNome);

        if (!ContainsOutput(expectImc))
            Console.WriteLine("Não calculou o imc corretamente: " + expectImc);


        Console.WriteLine("Passou nos testes");
    }
}

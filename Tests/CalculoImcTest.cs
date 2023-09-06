using System.Reflection;
using Transpilador.Mocks;
using Transpilador.Tests._Core;
using Xunit;
using Xunit.Sdk;

namespace Transpilador.Tests;

public class CalculoImcTest : TestBase, ITest
{
    public CalculoImcTest(MethodInfo program) : base(program)
    {
    }

    public override void ExecuteTests()
    {
        //Arrange
        //const double expectImc = 25.221;
        const double expectImc = 24.221;
        const string expectNome = "João Paulo";
        setInputValuesMock("20", "70", "1,70", "João Paulo");

        //Act
        executeProgram();

        //Assert
        ExpectContains(expectNome);
        ExpectContains(expectImc);
    }
}

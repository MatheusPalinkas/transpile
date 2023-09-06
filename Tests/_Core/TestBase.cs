using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Transpilador.Mocks;

namespace Transpilador.Tests._Core
{
    public abstract class TestBase : ITest
    {
        protected MethodInfo _program;
        private string[] inputValuesMock;
        private string consoleOutput;

        public TestBase(MethodInfo program)
        {
            _program = program;
        }

        public void Run()
        {
            try
            {
                this.ExecuteTests();
                Console.WriteLine("Passou nos testes");
            }
            catch (FailTestExcepion ex)
            {
                Console.WriteLine("Não passou nos testes.");

                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Expect);
                Console.WriteLine(ex.CurrentOutput);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Não foi possivel executar os testes.");
            }
        }

        public abstract void ExecuteTests();

        protected void setInputValuesMock(params string[] values)
        {
            inputValuesMock = values;
        }

        protected void executeProgram()
        {
            using (ConsoleOutputMock outputMock = new ConsoleOutputMock())
            using (ConsoleInputMock inputMock = new ConsoleInputMock(inputValuesMock))
            {
                _program.Invoke(null, null);

                consoleOutput = outputMock.GetOutput();
            }
        }

        protected void ExpectContains(string text)
        {
            if (!ContainsOutput(text))
                throw new FailTestExcepion("Not contains", text, consoleOutput);
        }

        protected void ExpectContains(double value)
        {
            if (!ContainsOutput(value))
                throw new FailTestExcepion("Not contains", value.ToString(), consoleOutput);
        }

        private bool ContainsOutput(string text)
        {
            return consoleOutput.Contains(text);
        }

        private bool ContainsOutput(double value)
        {
            var valueText = value.ToString()?.Replace(".", ",");
            return ContainsOutput(valueText);
        }
    }
}

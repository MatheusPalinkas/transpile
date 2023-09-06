using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Transpilador.Mocks;

namespace Transpilador.Tests
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

        public abstract void Run();

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

                this.consoleOutput = outputMock.GetOutput();
            }
        }

        protected bool ContainsOutput(string text)
        {
            return consoleOutput.Contains(text);
        }

        protected bool ContainsOutput(double value)
        {
            var valueText = value.ToString()?.Replace(".", ",");
            return ContainsOutput(valueText);
        }
    }
}

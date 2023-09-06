using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpilador.Tests._Core
{
    public class FailTestExcepion : Exception
    {
        private string expect;
        private string currentOutput;

        public FailTestExcepion(string mensagem) : base(mensagem)
        {
            expect = "";
            currentOutput = "";
        }

        public FailTestExcepion(string mensagem, string expect, string currentOutput) : base(mensagem)
        {
            this.expect = expect;
            this.currentOutput = currentOutput;
        }

        public string Expect { get => $"Expect: \t{expect}";}
        public string CurrentOutput { get => $"Output:\n\n {currentOutput}"; }
    }
}

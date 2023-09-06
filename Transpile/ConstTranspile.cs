using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpilador.Transpile
{
    public static class ConstTranspile
    {
        public static string[] TYPES = new string[] { "inteiro", "real", "cadeia", "logico" };
        public const string OUTPUT_COMMAND = "escreva";
        public const string INPUT_COMMAND = "leia";


        public const string TRUE_COMMAND = "verdadeiro";
        public const string FALSE_COMMAND = "falso";
    }
}

using System.Text;
using System.Text.RegularExpressions;

namespace Transpilador.Transpile
{
    public static class TranspilePortugolToCSharp
    {
        private static Dictionary<string, string> variables = new Dictionary<string, string>();
        public static string Transpile(string code)
        {
            var finalCode = new StringBuilder();

            finalCode.AppendLine("using System;\n");
            finalCode.AppendLine("class Program");
            finalCode.AppendLine("{");
            finalCode.AppendLine("static void Main()");
            finalCode.AppendLine("{");

            string[] linesCode = code.Split('\n');

            foreach (var lineCode in linesCode)
            {
                finalCode.AppendLine(parserLineOfCode(lineCode));
            }

            finalCode.AppendLine("}");
            finalCode.AppendLine("}");

            return finalCode.ToString();
        }

        private static string parserLineOfCode(string lineCode)
        {

            if (ConstTranspile.TYPES.Any(type => lineCode.Contains(type)))
                return parserVariables(lineCode);


            if (lineCode.Contains(ConstTranspile.OUTPUT_COMMAND))
                return parserOutput(lineCode);

            if (lineCode.Contains(ConstTranspile.INPUT_COMMAND))
                return parserInput(lineCode);

            if (!string.IsNullOrWhiteSpace(lineCode))
                return $"{lineCode?.Trim()};";

            return lineCode;
        }


        private static string parserVariables(string lineCode)
        {
            string[] parts = lineCode.Trim()
                                         .Replace(",", " ")
                                         .Split(" ")
                                         .Where(part => !string.IsNullOrWhiteSpace(part))
                                         .ToArray();

            string type = parts[0];
            string[] namesVariables = parts.Where(part => !ConstTranspile.TYPES.Any(type => part.Contains(type))).ToArray();

            Dictionary<string, string> parserTypes = new Dictionary<string, string>()
                {
                    { "inteiro", "int" },
                    { "real", "double" },
                    { "cadeia", "string" },
                    { "logico", "bool" }
                };

            foreach (var nameVariable in namesVariables)
            {
                variables.Add(nameVariable, type);
            }
            return $"{parserTypes[type]} {string.Join(", ", namesVariables)};";
        }

        private static string parserOutput(string lineCode)
        {
            string message = lineCode.Substring(lineCode.IndexOf("(") + 1, lineCode.IndexOf(")") - lineCode.IndexOf("(") - 1);

            string pattern = @",(?=(?:[^""]*""[^""]*"")*[^""]*$)";
            message = Regex.Replace(message, pattern, " + ");

            return $"Console.WriteLine({message});";
        }

        private static string parserInput(string lineCode)
        {
            string variable = lineCode.Substring(lineCode.IndexOf("(") + 1, lineCode.IndexOf(")") - lineCode.IndexOf("(") - 1);
            string type = variables[variable] ?? "";


            Dictionary<string, string> typeToInput = new Dictionary<string, string>()
                {
                    { "inteiro", "Convert.ToInt32(Console.ReadLine());" },
                    { "real", "Convert.ToDouble(Console.ReadLine());" },
                    { "cadeia", "Console.ReadLine();" },
                    { "logico", "Convert.ToBoolean(Console.ReadLine());" }
                };

            return $"{variable} = {typeToInput[type]}";
        }
    }
}

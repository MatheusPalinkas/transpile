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
            var (type, namesVariables) = separateDeclarationVariable(lineCode);
            registerVariables(type, namesVariables);

            Dictionary<string, string> parserTypes = new Dictionary<string, string>()
                {
                    { "inteiro", "int" },
                    { "real", "double" },
                    { "cadeia", "string" },
                    { "logico", "bool" }
                };


            return $"{parserTypes[type]} {string.Join(", ", namesVariables)};";
        }

        private static (string, string[]) separateDeclarationVariable(string lineCode)
        {
            string type = lineCode.Trim().Substring(0, lineCode.Trim().IndexOf(" "));
            string[] namesVariables = lineCode.Trim()
                                              .Substring(lineCode.Trim().IndexOf(" "))
                                              .Trim()
                                              .Split(",")
                                              .Select(variable => {
                                                if(variable.Contains(ConstTranspile.TRUE_COMMAND) || variable.Contains(ConstTranspile.FALSE_COMMAND))
                                                {
                                                      variable = variable
                                                                    .Replace(ConstTranspile.TRUE_COMMAND, "true")
                                                                    .Replace(ConstTranspile.FALSE_COMMAND, "false");
                                                }    

                                                return variable.Trim();
                                              })
                                              .ToArray();

            return (type, namesVariables);
        }

        private static void registerVariables(string type, string[] namesVariables)
        {
            foreach (var nameVariable in namesVariables)
            {
                string variable = !nameVariable.Contains("=")
                                    ? nameVariable
                                    : nameVariable?.Split("=")?.FirstOrDefault() ?? "";

                variables.Add(variable.Trim(), type);
            }
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

using Microsoft.CodeAnalysis.Emit;
using System.Reflection;

namespace Transpilador.CSharp
{
    public static class ExecuteCSharp
    {
        public static void Execute(string code)
        {
            var compilation = CompileCSharp.Compile(code);

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (result.Success)
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = Assembly.Load(ms.ToArray());
                    MethodInfo entryPoint = assembly.EntryPoint;
                    if (entryPoint != null)
                    {
                        object[] args = null;
                        if (entryPoint.GetParameters().Length > 0)
                        {
                            args = new object[] { new string[0] };
                        }
                        entryPoint.Invoke(null, args);
                    }
                    else
                    {
                        Console.WriteLine("Main method not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Compilation failed:");
                    foreach (var diagnostic in result.Diagnostics)
                    {
                        Console.WriteLine(diagnostic);
                    }
                }
            }
        }
    }
}

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Reflection;

namespace Transpilador.CSharp;

public static class CompileCSharp
{
    public static MethodInfo Compile(string code)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

        var compilation = CSharpCompilation
                            .Create("DynamicAssembly")
                            .WithOptions(new CSharpCompilationOptions(OutputKind.ConsoleApplication))
                            .AddReferences(AppDomain.CurrentDomain.GetAssemblies().Select(a => MetadataReference.CreateFromFile(a.Location)))
                            .AddSyntaxTrees(syntaxTree);

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
                    return entryPoint;
                }
                else
                {
                    throw new Exception("Main method not found.");
                }

            }
            else
            {
                var diagnostics = string.Join("\n", result.Diagnostics.Select(diagnostic => diagnostic.ToString()));
                throw new Exception($"Compilation failed:\n\n{diagnostics}");
            }

        }
    }
}

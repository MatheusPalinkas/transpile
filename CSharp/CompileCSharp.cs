using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Transpilador.CSharp
{
    public static class CompileCSharp
    {
        public static CSharpCompilation Compile(string code)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

            var compilation = CSharpCompilation.Create("DynamicAssembly")
                .WithOptions(new CSharpCompilationOptions(OutputKind.ConsoleApplication))
                .AddReferences(AppDomain.CurrentDomain.GetAssemblies().Select(a => MetadataReference.CreateFromFile(a.Location)))
                .AddSyntaxTrees(syntaxTree);

            return compilation;
        }
    }
}

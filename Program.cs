using Transpilador.CSharp;
using Transpilador.Transpile;

var codePortugol = @"
    inteiro idade
    real altura, peso, imc
    cadeia nome, prontuario
    logico temPermissao

    escreva(""Digite a idade"")
    leia(idade)

    escreva(""Digite o peso"")
    leia(peso)

    escreva(""Digite a altura"")
    leia(altura)

    escreva(""Digite o nome"")
    leia(nome)

    escreva(""Digite o prontuario"")
    leia(prontuario)

    imc = peso / (altura * altura)

    escreva(""Nome: "", nome)
    escreva(""IMC: "", imc)
";
var codeCSharp = TranspilePortugolToCSharp.Transpile(codePortugol);

Console.WriteLine(codeCSharp);
Console.WriteLine("\n\n\n\n============================\n\n\n\n");
Console.WriteLine("Precione uma tecla para continuar...");
Console.ReadKey();

ExecuteCSharp.Execute(codeCSharp);

Console.WriteLine("\n\n\n\n============================\n\n\n\n");
Console.WriteLine("Precione uma tecla para continuar...");
Console.ReadKey();
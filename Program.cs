using Transpilador.CSharp;
using Transpilador.Tests;
using Transpilador.Transpile;

var codePortugol = @"
    inteiro idade
    real altura, peso, imc
    cadeia nome = ""Palinkas"", prontuario = ""CB12345678""
    logico temPermissao = verdadeiro

    escreva(""Digite a idade"")
    leia(idade)

    escreva(""Digite o peso"")
    leia(peso)

    escreva(""Digite a altura"")
    leia(altura)

    escreva(""Digite o nome"")
    leia(nome)

    escreva(""peso: "", peso)
    escreva(""altura: "", altura)

    imc = peso / (altura * altura)
    
    escreva(""imc: "", imc)

    escreva(""Nome: "", nome)
    escreva(""prontuario: "", prontuario)
    escreva(""IMC: "", imc)
    escreva(""temPermissao: "", temPermissao)
";
var codeCSharp = TranspilePortugolToCSharp.Transpile(codePortugol);

var compilation = CompileCSharp.Compile(codeCSharp);

var testImc = new CalculoImcTest(compilation);
testImc.Run();

Console.ReadKey();

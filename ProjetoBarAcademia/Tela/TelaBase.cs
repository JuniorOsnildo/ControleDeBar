// Tela/TelaBase.cs
using System;

namespace ProjetoBarAcademia.Tela
{
    public abstract class TelaBase
    {
        protected string Titulo { get; set; }

        public TelaBase(string titulo)
        {
            Titulo = titulo;
        }

        public abstract void ApresentarMenu();

        protected void ApresentarTitulo()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine($"\t{Titulo}");
            Console.WriteLine("========================================");
            Console.WriteLine();
        }

        protected void ApresentarMensagem(string mensagem, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Console.WriteLine(mensagem);
            Console.ResetColor();
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        protected string ObterString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        protected int ObterInt(string prompt)
        {
            int valor;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out valor))
                {
                    return valor;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Entrada inválida. Por favor, digite um número inteiro.");
                Console.ResetColor();
            }
        }

        protected decimal ObterDecimal(string prompt)
        {
            decimal valor;
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out valor))
                {
                    return valor;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Entrada inválida. Por favor, digite um número decimal (ex: 10,50).");
                Console.ResetColor();
            }
        }
    }
}
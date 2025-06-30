
using System;

namespace ProjetoBarAcademia.Tela
{
    public class TelaPrincipal
    {
        private readonly TelaMesa _telaMesa;
        private readonly TelaGarcom _telaGarcom;
        private readonly TelaProduto _telaProduto;
        private readonly TelaConta _telaConta;

        public TelaPrincipal(TelaMesa telaMesa, TelaGarcom telaGarcom, TelaProduto telaProduto, TelaConta telaConta)
        {
            _telaMesa = telaMesa;
            _telaGarcom = telaGarcom;
            _telaProduto = telaProduto;
            _telaConta = telaConta;
        }

        public void MostrarMenuPrincipal()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("\tSistema de Gerenciamento de Bar e Restaurante");
                Console.WriteLine("========================================");
                Console.WriteLine();
                Console.WriteLine("1 - Gerenciar Contas e Pedidos");
                Console.WriteLine("2 - Gerenciar Mesas");
                Console.WriteLine("3 - Gerenciar Garçons");
                Console.WriteLine("4 - Gerenciar Produtos");
                Console.WriteLine("0 - Sair");
                Console.Write("\nEscolha um módulo: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": _telaConta.ApresentarMenu(); break;
                    case "2": _telaMesa.ApresentarMenu(); break;
                    case "3": _telaGarcom.ApresentarMenu(); break;
                    case "4": _telaProduto.ApresentarMenu(); break;
                    case "0":
                        Console.WriteLine("Saindo do sistema...");
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opção inválida! Pressione qualquer tecla para tentar novamente.");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
// Tela/TelaConta.cs
using ProjetoBarAcademia.Entidade;
using ProjetoBarAcademia.Enum;
using ProjetoBarAcademia.Repositorio;
using System;
using System.Linq;

namespace ProjetoBarAcademia.Tela
{
    public class TelaConta : TelaBase
    {
        private readonly RepositorioConta _repositorioConta;
        private readonly RepositorioMesa _repositorioMesa;
        private readonly RepositorioGarcom _repositorioGarcom;
        private readonly RepositorioProduto _repositorioProduto;

        public TelaConta(RepositorioConta repositorioConta, RepositorioMesa repositorioMesa, RepositorioGarcom repositorioGarcom, RepositorioProduto repositorioProduto)
            : base("Gerenciamento de Contas e Pedidos")
        {
            _repositorioConta = repositorioConta;
            _repositorioMesa = repositorioMesa;
            _repositorioGarcom = repositorioGarcom;
            _repositorioProduto = repositorioProduto;
        }

        public override void ApresentarMenu()
        {
            while (true)
            {
                ApresentarTitulo();
                Console.WriteLine("1 - Abrir Nova Conta");
                Console.WriteLine("2 - Adicionar Itens a uma Conta");
                Console.WriteLine("3 - Remover Itens de uma Conta");
                Console.WriteLine("4 - Fechar Conta");
                Console.WriteLine("5 - Visualizar Contas Abertas");
                Console.WriteLine("6 - Visualizar Contas Fechadas");
                Console.WriteLine("7 - Visualizar Faturamento do Dia");
                Console.WriteLine("0 - Voltar");
                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": AbrirConta(); break;
                    case "2": AdicionarItens(); break;
                    case "3": RemoverItens(); break;
                    case "4": FecharConta(); break;
                    case "5": VisualizarContasAbertas(); break;
                    case "6": VisualizarContasFechadas(); break;
                    case "7": VisualizarFaturamentoDiario(); break;
                    case "0": return;
                    default: ApresentarMensagem("Opção inválida!", ConsoleColor.Red); break;
                }
            }
        }

        private void AbrirConta()
        {
            ApresentarTitulo();
            Console.WriteLine(">> Abrindo Nova Conta");

            Console.WriteLine("\nMesas Livres:");
            var mesasLivres = _repositorioMesa.SelecionarTodos().Where(m => m.Status == StatusMesa.Livre).ToList();
            if (mesasLivres.Count == 0)
            {
                ApresentarMensagem("Nenhuma mesa livre no momento.", ConsoleColor.Yellow);
                return;
            }
            mesasLivres.ForEach(m => Console.WriteLine(m.ToString()));
            int idMesa = ObterInt("\nID da Mesa: ");
            Mesa mesaSelecionada = mesasLivres.FirstOrDefault(m => m.Id == idMesa);
            if (mesaSelecionada == null)
            {
                ApresentarMensagem("Mesa inválida ou já ocupada.", ConsoleColor.Red);
                return;
            }

            Console.WriteLine("\nGarçons Disponíveis:");
            var garcons = _repositorioGarcom.SelecionarTodos();
            if (garcons.Count == 0)
            {
                ApresentarMensagem("Nenhum garçom cadastrado.", ConsoleColor.Red);
                return;
            }
            garcons.ForEach(g => Console.WriteLine(g.ToString()));
            int idGarcom = ObterInt("\nID do Garçom: ");
            Garcom garcomSelecionado = _repositorioGarcom.SelecionarPorId(idGarcom);
            if (garcomSelecionado == null)
            {
                ApresentarMensagem("Garçom não encontrado.", ConsoleColor.Red);
                return;
            }

            string nomeCliente = ObterString("Nome do Cliente: ");
            if (string.IsNullOrWhiteSpace(nomeCliente))
            {
                ApresentarMensagem("O nome do cliente é obrigatório.", ConsoleColor.Red);
                return;
            }

            Conta novaConta = new Conta(nomeCliente, mesaSelecionada, garcomSelecionado);
            _repositorioConta.Inserir(novaConta);

            ApresentarMensagem($"Conta para o cliente '{nomeCliente}' aberta na mesa {mesaSelecionada.Numero}!", ConsoleColor.Green);
        }

        private void AdicionarItens()
        {
            ApresentarTitulo();
            VisualizarContasAbertas(false);
            Console.WriteLine("\n>> Adicionando Itens");

            if (_repositorioConta.ListarContasAbertas().Count == 0) return;

            int idConta = ObterInt("ID da conta para adicionar itens: ");
            Conta conta = _repositorioConta.SelecionarPorId(idConta);
            if (conta == null || conta.Status == StatusConta.Fechada)
            {
                ApresentarMensagem("Conta não encontrada ou já está fechada.", ConsoleColor.Red);
                return;
            }

            Console.WriteLine("\nProdutos Disponíveis:");
            _repositorioProduto.SelecionarTodos().ForEach(p => Console.WriteLine(p.ToString()));

            int idProduto = ObterInt("\nID do Produto a adicionar: ");
            Produto produto = _repositorioProduto.SelecionarPorId(idProduto);
            if (produto == null)
            {
                ApresentarMensagem("Produto não encontrado.", ConsoleColor.Red);
                return;
            }

            int quantidade = ObterInt("Quantidade: ");
            if (quantidade <= 0)
            {
                ApresentarMensagem("A quantidade deve ser positiva.", ConsoleColor.Red);
                return;
            }

            conta.AdicionarItem(produto, quantidade);
            _repositorioConta.Editar(conta.Id, conta);
            ApresentarMensagem($"{quantidade}x {produto.Nome} adicionado(s) à conta.", ConsoleColor.Green);
        }

        private void RemoverItens()
        {
            ApresentarTitulo();
            VisualizarContasAbertas(false);
            Console.WriteLine("\n>> Removendo Itens");

            if (_repositorioConta.ListarContasAbertas().Count == 0) return;

            int idConta = ObterInt("ID da conta para remover itens: ");
            Conta conta = _repositorioConta.SelecionarPorId(idConta);
            if (conta == null || conta.Status == StatusConta.Fechada)
            {
                ApresentarMensagem("Conta não encontrada ou já está fechada.", ConsoleColor.Red);
                return;
            }

            if (conta.Itens.Count == 0)
            {
                ApresentarMensagem("Esta conta não possui itens para remover.", ConsoleColor.Yellow);
                return;
            }

            Console.WriteLine("\nItens na Conta:");
            conta.Itens.ForEach(i => Console.WriteLine(i.ToString()));

            int idProduto = ObterInt("\nID do Produto a remover: ");
            Produto produto = _repositorioProduto.SelecionarPorId(idProduto);
            if (produto == null)
            {
                ApresentarMensagem("Produto não encontrado.", ConsoleColor.Red);
                return;
            }

            int quantidade = ObterInt("Quantidade a remover: ");
            if (quantidade <= 0)
            {
                ApresentarMensagem("A quantidade deve ser positiva.", ConsoleColor.Red);
                return;
            }

            if (conta.RemoverItem(produto, quantidade))
            {
                _repositorioConta.Editar(conta.Id, conta);
                ApresentarMensagem("Item(s) removido(s) com sucesso.", ConsoleColor.Green);
            }
            else
            {
                ApresentarMensagem("Não foi possível remover. A quantidade pode ser maior que a existente.", ConsoleColor.Red);
            }
        }

        private void FecharConta()
        {
            ApresentarTitulo();
            VisualizarContasAbertas(false);
            Console.WriteLine("\n>> Fechando Conta");

            if (_repositorioConta.ListarContasAbertas().Count == 0) return;

            int idConta = ObterInt("ID da conta para fechar: ");
            Conta conta = _repositorioConta.SelecionarPorId(idConta);
            if (conta == null || conta.Status == StatusConta.Fechada)
            {
                ApresentarMensagem("Conta não encontrada ou já está fechada.", ConsoleColor.Red);
                return;
            }

            ExibirDetalhesConta(conta);

            Console.Write("\nConfirmar fechamento da conta? (S/N): ");
            string confirmacao = Console.ReadLine();

            if (confirmacao.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                conta.FecharConta();
                _repositorioConta.Editar(conta.Id, conta);
                ApresentarMensagem("Conta fechada com sucesso!", ConsoleColor.Green);
            }
            else
            {
                ApresentarMensagem("Operação cancelada.", ConsoleColor.Yellow);
            }
        }

        private void VisualizarContasAbertas(bool pausarNoFinal = true)
        {
            ApresentarTitulo();
            Console.WriteLine(">> Contas em Aberto");
            var contas = _repositorioConta.ListarContasAbertas();
            if (contas.Count == 0)
            {
                Console.WriteLine("Nenhuma conta aberta no momento.");
            }
            else
            {
                foreach (var conta in contas)
                {
                    ExibirDetalhesConta(conta);
                }
            }

            if (pausarNoFinal)
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        private void VisualizarContasFechadas()
        {
            ApresentarTitulo();
            Console.WriteLine(">> Contas Fechadas Hoje");
            var contas = _repositorioConta.ListarContasFechadas()
                .Where(c => c.DataFechamento?.Date == DateTime.Today)
                .ToList();

            if (contas.Count == 0)
            {
                Console.WriteLine("Nenhuma conta foi fechada hoje.");
            }
            else
            {
                foreach (var conta in contas)
                {
                    ExibirDetalhesConta(conta);
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private void VisualizarFaturamentoDiario()
        {
            ApresentarTitulo();
            decimal faturamento = _repositorioConta.ObterFaturamentoDoDia(DateTime.Today);

            Console.WriteLine($">> Faturamento Total do Dia: {DateTime.Today:dd/MM/yyyy}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"R$ {faturamento:F2}");
            Console.ResetColor();

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private void ExibirDetalhesConta(Conta conta)
        {
            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine($"Conta ID: {conta.Id} | Cliente: {conta.NomeCliente}");
            Console.WriteLine($"Mesa: {conta.Mesa.Numero} | Garçom: {conta.Garcom.Nome}");
            Console.WriteLine($"Aberta em: {conta.DataAbertura:G} | Status: {conta.Status}");

            Console.WriteLine("Itens:");
            if (conta.Itens.Count == 0)
            {
                Console.WriteLine("  (Nenhum item adicionado)");
            }
            else
            {
                foreach (var item in conta.Itens)
                {
                    Console.WriteLine($"  - {item}");
                }
            }

            Console.WriteLine($"\nSubtotal: R$ {conta.ValorTotal:F2}");
            Console.WriteLine($"Taxa de Serviço (10%): R$ {conta.Gorjeta:F2}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Total a Pagar: R$ {conta.TotalAPagar:F2}");
            Console.ResetColor();
            Console.WriteLine("----------------------------------------");
        }
    }
}
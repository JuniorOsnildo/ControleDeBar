// Tela/TelaProduto.cs
using ProjetoBarAcademia.Entidade;
using ProjetoBarAcademia.Repositorio;
using System;
using System.Collections.Generic;

namespace ProjetoBarAcademia.Tela
{
    public class TelaProduto : TelaBase
    {
        private readonly RepositorioProduto _repositorioProduto;
        private readonly RepositorioConta _repositorioConta;

        public TelaProduto(RepositorioProduto repositorioProduto, RepositorioConta repositorioConta) : base("Gerenciamento de Produtos")
        {
            _repositorioProduto = repositorioProduto;
            _repositorioConta = repositorioConta;
        }

        public override void ApresentarMenu()
        {
            while (true)
            {
                ApresentarTitulo();
                Console.WriteLine("1 - Cadastrar Novo Produto");
                Console.WriteLine("2 - Editar Produto");
                Console.WriteLine("3 - Excluir Produto");
                Console.WriteLine("4 - Visualizar Produtos");
                Console.WriteLine("0 - Voltar");
                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": Inserir(); break;
                    case "2": Editar(); break;
                    case "3": Excluir(); break;
                    case "4": Visualizar(); break;
                    case "0": return;
                    default: ApresentarMensagem("Opção inválida!", ConsoleColor.Red); break;
                }
            }
        }

        private void Inserir()
        {
            ApresentarTitulo();
            Console.WriteLine(">> Cadastrando Novo Produto");
            string nome = ObterString("Nome do Produto: ");
            if (nome.Length < 2 || nome.Length > 100)
            {
                ApresentarMensagem("Erro: O nome deve ter entre 2 e 100 caracteres.", ConsoleColor.Red);
                return;
            }
            if (_repositorioProduto.VerificarNomeExistente(nome))
            {
                ApresentarMensagem("Erro: Já existe um produto com este nome.", ConsoleColor.Red);
                return;
            }

            decimal preco = ObterDecimal("Preço (ex: 15,50): ");
            if (preco <= 0)
            {
                ApresentarMensagem("Erro: O preço deve ser um número positivo.", ConsoleColor.Red);
                return;
            }

            Produto novoProduto = new Produto(nome, Math.Round(preco, 2));
            _repositorioProduto.Inserir(novoProduto);
            ApresentarMensagem("Produto cadastrado com sucesso!", ConsoleColor.Green);
        }

        private void Editar()
        {
            ApresentarTitulo();
            Visualizar(false);
            Console.WriteLine("\n>> Editando Produto");
            int id = ObterInt("ID do produto para editar: ");
            Produto produto = _repositorioProduto.SelecionarPorId(id);

            if (produto == null)
            {
                ApresentarMensagem("Produto não encontrado!", ConsoleColor.Red);
                return;
            }

            string novoNome = ObterString($"Novo nome (atual: {produto.Nome}): ");
            if (novoNome.Length < 2 || novoNome.Length > 100)
            {
                ApresentarMensagem("Erro: O nome deve ter entre 2 e 100 caracteres.", ConsoleColor.Red);
                return;
            }
            if (_repositorioProduto.VerificarNomeExistente(novoNome, id))
            {
                ApresentarMensagem("Erro: Já existe outro produto com este nome.", ConsoleColor.Red);
                return;
            }

            decimal novoPreco = ObterDecimal($"Novo preço (atual: {produto.Preco:F2}): ");
            if (novoPreco <= 0)
            {
                ApresentarMensagem("Erro: O preço deve ser um número positivo.", ConsoleColor.Red);
                return;
            }

            produto.Nome = novoNome;
            produto.Preco = Math.Round(novoPreco, 2);
            _repositorioProduto.Editar(id, produto);
            ApresentarMensagem("Produto editado com sucesso!", ConsoleColor.Green);
        }

        private void Excluir()
        {
            ApresentarTitulo();
            Visualizar(false);
            Console.WriteLine("\n>> Excluindo Produto");
            int id = ObterInt("ID do produto para excluir: ");

            if (_repositorioConta.VerificarProdutoComPedidosVinculados(id))
            {
                ApresentarMensagem("Erro: Este produto não pode ser excluído pois está vinculado a pedidos.", ConsoleColor.Red);
                return;
            }

            Produto produto = _repositorioProduto.SelecionarPorId(id);
            if (produto == null)
            {
                ApresentarMensagem("Produto não encontrado!", ConsoleColor.Red);
                return;
            }

            _repositorioProduto.Excluir(id);
            ApresentarMensagem("Produto excluído com sucesso!", ConsoleColor.Green);
        }

        private void Visualizar(bool pausarNoFinal = true)
        {
            ApresentarTitulo();
            Console.WriteLine(">> Lista de Produtos Cadastrados");
            List<Produto> produtos = _repositorioProduto.SelecionarTodos();
            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado.");
            }
            else
            {
                foreach (var produto in produtos)
                {
                    Console.WriteLine(produto.ToString());
                }
            }
            if (pausarNoFinal)
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}
// Tela/TelaGarcom.cs
using ProjetoBarAcademia.Entidade;
using ProjetoBarAcademia.Repositorio;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProjetoBarAcademia.Tela
{
    public class TelaGarcom : TelaBase
    {
        private readonly RepositorioGarcom _repositorioGarcom;
        private readonly RepositorioConta _repositorioConta;

        public TelaGarcom(RepositorioGarcom repositorioGarcom, RepositorioConta repositorioConta) : base("Gerenciamento de Garçons")
        {
            _repositorioGarcom = repositorioGarcom;
            _repositorioConta = repositorioConta;
        }

        public override void ApresentarMenu()
        {
            while (true)
            {
                ApresentarTitulo();
                Console.WriteLine("1 - Cadastrar Novo Garçom");
                Console.WriteLine("2 - Editar Garçom");
                Console.WriteLine("3 - Excluir Garçom");
                Console.WriteLine("4 - Visualizar Garçons");
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
            Console.WriteLine(">> Cadastrando Novo Garçom");
            string nome = ObterString("Nome: ");
            if (nome.Length < 3 || nome.Length > 100)
            {
                ApresentarMensagem("Erro: O nome deve ter entre 3 e 100 caracteres.", ConsoleColor.Red);
                return;
            }

            string cpf = ObterString("CPF (formato XXX.XXX.XXX-XX): ");
            if (!ValidarFormatoCpf(cpf))
            {
                ApresentarMensagem("Erro: Formato de CPF inválido.", ConsoleColor.Red);
                return;
            }
            if (_repositorioGarcom.VerificarCpfExistente(cpf))
            {
                ApresentarMensagem("Erro: Este CPF já está cadastrado.", ConsoleColor.Red);
                return;
            }

            Garcom novoGarcom = new Garcom(nome, cpf);
            _repositorioGarcom.Inserir(novoGarcom);
            ApresentarMensagem("Garçom cadastrado com sucesso!", ConsoleColor.Green);
        }

        private void Editar()
        {
            ApresentarTitulo();
            Visualizar(false);
            Console.WriteLine("\n>> Editando Garçom");
            int id = ObterInt("ID do garçom para editar: ");
            Garcom garcom = _repositorioGarcom.SelecionarPorId(id);

            if (garcom == null)
            {
                ApresentarMensagem("Garçom não encontrado!", ConsoleColor.Red);
                return;
            }

            string novoNome = ObterString($"Novo nome (atual: {garcom.Nome}): ");
            if (novoNome.Length < 3 || novoNome.Length > 100)
            {
                ApresentarMensagem("Erro: O nome deve ter entre 3 e 100 caracteres.", ConsoleColor.Red);
                return;
            }

            string novoCpf = ObterString($"Novo CPF (atual: {garcom.Cpf}): ");
            if (!ValidarFormatoCpf(novoCpf))
            {
                ApresentarMensagem("Erro: Formato de CPF inválido.", ConsoleColor.Red);
                return;
            }
            if (_repositorioGarcom.VerificarCpfExistente(novoCpf, id))
            {
                ApresentarMensagem("Erro: Este CPF já está cadastrado para outro garçom.", ConsoleColor.Red);
                return;
            }

            garcom.Nome = novoNome;
            garcom.Cpf = novoCpf;
            _repositorioGarcom.Editar(id, garcom);
            ApresentarMensagem("Garçom editado com sucesso!", ConsoleColor.Green);
        }

        private void Excluir()
        {
            ApresentarTitulo();
            Visualizar(false);
            Console.WriteLine("\n>> Excluindo Garçom");
            int id = ObterInt("ID do garçom para excluir: ");

            if (_repositorioConta.VerificarGarcomComPedidosVinculados(id))
            {
                ApresentarMensagem("Erro: Este garçom não pode ser excluído pois possui pedidos vinculados.", ConsoleColor.Red);
                return;
            }

            Garcom garcom = _repositorioGarcom.SelecionarPorId(id);
            if (garcom == null)
            {
                ApresentarMensagem("Garçom não encontrado!", ConsoleColor.Red);
                return;
            }

            _repositorioGarcom.Excluir(id);
            ApresentarMensagem("Garçom excluído com sucesso!", ConsoleColor.Green);
        }

        private void Visualizar(bool pausarNoFinal = true)
        {
            ApresentarTitulo();
            Console.WriteLine(">> Lista de Garçons Cadastrados");
            List<Garcom> garcons = _repositorioGarcom.SelecionarTodos();
            if (garcons.Count == 0)
            {
                Console.WriteLine("Nenhum garçom cadastrado.");
            }
            else
            {
                foreach (var garcom in garcons)
                {
                    Console.WriteLine(garcom.ToString());
                }
            }
            if (pausarNoFinal)
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        private bool ValidarFormatoCpf(string cpf)
        {
            return Regex.IsMatch(cpf, @"^\d{3}\.\d{3}\.\d{3}-\d{2}$");
        }
    }
}
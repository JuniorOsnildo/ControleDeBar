// Tela/TelaMesa.cs
using ProjetoBarAcademia.Entidade;
using ProjetoBarAcademia.Repositorio;
using System;
using System.Collections.Generic;

namespace ProjetoBarAcademia.Tela
{
    public class TelaMesa : TelaBase
    {
        private readonly RepositorioMesa _repositorioMesa;
        private readonly RepositorioConta _repositorioConta;

        public TelaMesa(RepositorioMesa repositorioMesa, RepositorioConta repositorioConta) : base("Gerenciamento de Mesas")
        {
            _repositorioMesa = repositorioMesa;
            _repositorioConta = repositorioConta;
        }

        public override void ApresentarMenu()
        {
            while (true)
            {
                ApresentarTitulo();
                Console.WriteLine("1 - Cadastrar Nova Mesa");
                Console.WriteLine("2 - Editar Mesa");
                Console.WriteLine("3 - Excluir Mesa");
                Console.WriteLine("4 - Visualizar Mesas");
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
            Console.WriteLine(">> Cadastrando Nova Mesa");
            int numero = ObterInt("Número da Mesa: ");
            if (_repositorioMesa.VerificarNumeroExistente(numero))
            {
                ApresentarMensagem($"Erro: Já existe uma mesa com o número {numero}.", ConsoleColor.Red);
                return;
            }
            if (numero <= 0)
            {
                ApresentarMensagem("Erro: O número da mesa deve ser positivo.", ConsoleColor.Red);
                return;
            }

            int lugares = ObterInt("Quantidade de Lugares: ");
            if (lugares <= 0)
            {
                ApresentarMensagem("Erro: A quantidade de lugares deve ser positiva.", ConsoleColor.Red);
                return;
            }

            Mesa novaMesa = new Mesa(numero, lugares);
            _repositorioMesa.Inserir(novaMesa);
            ApresentarMensagem("Mesa cadastrada com sucesso!", ConsoleColor.Green);
        }

        private void Editar()
        {
            ApresentarTitulo();
            Visualizar(false);
            Console.WriteLine("\n>> Editando Mesa");
            int id = ObterInt("ID da mesa para editar: ");
            Mesa mesa = _repositorioMesa.SelecionarPorId(id);

            if (mesa == null)
            {
                ApresentarMensagem("Mesa não encontrada!", ConsoleColor.Red);
                return;
            }

            int novoNumero = ObterInt($"Novo número (atual: {mesa.Numero}): ");
            if (_repositorioMesa.VerificarNumeroExistente(novoNumero, id))
            {
                ApresentarMensagem($"Erro: Já existe outra mesa com o número {novoNumero}.", ConsoleColor.Red);
                return;
            }
            if (novoNumero <= 0)
            {
                ApresentarMensagem("Erro: O número da mesa deve ser positivo.", ConsoleColor.Red);
                return;
            }

            int novosLugares = ObterInt($"Nova quantidade de lugares (atual: {mesa.QuantidadeLugares}): ");
            if (novosLugares <= 0)
            {
                ApresentarMensagem("Erro: A quantidade de lugares deve ser positiva.", ConsoleColor.Red);
                return;
            }

            mesa.Numero = novoNumero;
            mesa.QuantidadeLugares = novosLugares;
            _repositorioMesa.Editar(id, mesa);
            ApresentarMensagem("Mesa editada com sucesso!", ConsoleColor.Green);
        }

        private void Excluir()
        {
            ApresentarTitulo();
            Visualizar(false);
            Console.WriteLine("\n>> Excluindo Mesa");
            int id = ObterInt("ID da mesa para excluir: ");

            if (_repositorioConta.VerificarMesaComPedidosVinculados(id))
            {
                ApresentarMensagem("Erro: Esta mesa não pode ser excluída pois possui pedidos vinculados.", ConsoleColor.Red);
                return;
            }

            Mesa mesa = _repositorioMesa.SelecionarPorId(id);
            if (mesa == null)
            {
                ApresentarMensagem("Mesa não encontrada!", ConsoleColor.Red);
                return;
            }

            _repositorioMesa.Excluir(id);
            ApresentarMensagem("Mesa excluída com sucesso!", ConsoleColor.Green);
        }

        private void Visualizar(bool pausarNoFinal = true)
        {
            ApresentarTitulo();
            Console.WriteLine(">> Lista de Mesas Cadastradas");
            List<Mesa> mesas = _repositorioMesa.SelecionarTodos();
            if (mesas.Count == 0)
            {
                Console.WriteLine("Nenhuma mesa cadastrada.");
            }
            else
            {
                foreach (var mesa in mesas)
                {
                    Console.WriteLine(mesa.ToString());
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
// Repositorio/RepositorioConta.cs
using ProjetoBarAcademia.Entidade;
using ProjetoBarAcademia.Enum;
using ProjetoBarAcademia.Repositorio.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoBarAcademia.Repositorio
{
    public class RepositorioConta : RepositorioBase<Conta>
    {
        private readonly RepositorioMesa _repositorioMesa;

        public RepositorioConta(RepositorioMesa repositorioMesa)
        {
            _repositorioMesa = repositorioMesa;
        }

        public override void Inserir(Conta conta)
        {
            base.Inserir(conta);
            conta.Mesa.Status = StatusMesa.Ocupada;
            _repositorioMesa.Editar(conta.Mesa.Id, conta.Mesa);
        }

        public List<Conta> ListarContasAbertas()
        {
            return registros.Where(c => c.Status == StatusConta.Aberta).ToList();
        }

        public List<Conta> ListarContasFechadas()
        {
            return registros.Where(c => c.Status == StatusConta.Fechada).ToList();
        }

        public decimal ObterFaturamentoDoDia(DateTime dia)
        {
            return registros
                .Where(c => c.Status == StatusConta.Fechada && c.DataFechamento.HasValue && c.DataFechamento.Value.Date == dia.Date)
                .Sum(c => c.ValorTotal);
        }
        
        public bool VerificarMesaComContaAtiva(int idMesa)
        {
            return registros.Any(c => c.Mesa.Id == idMesa && c.Status == StatusConta.Aberta);
        }

        public bool VerificarGarcomComPedidosVinculados(int idGarcom)
        {
            return registros.Any(c => c.Garcom.Id == idGarcom);
        }

        public bool VerificarProdutoComPedidosVinculados(int idProduto)
        {
            return registros.Any(c => c.Itens.Any(i => i.Produto.Id == idProduto));
        }
        
        public bool VerificarMesaComPedidosVinculados(int idMesa)
        {
            return registros.Any(c => c.Mesa.Id == idMesa);
        }
    }
}
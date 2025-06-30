// Repositorio/RepositorioProduto.cs
using ProjetoBarAcademia.Entidade;
using ProjetoBarAcademia.Repositorio.Base;
using System.Linq;
using System;

namespace ProjetoBarAcademia.Repositorio
{
    public class RepositorioProduto : RepositorioBase<Produto>
    {
        public bool VerificarNomeExistente(string nome, int idExcecao = 0)
        {
            return registros.Any(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase) && p.Id != idExcecao);
        }
    }
}
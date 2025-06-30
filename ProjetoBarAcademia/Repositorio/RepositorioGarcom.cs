// Repositorio/RepositorioGarcom.cs
using ProjetoBarAcademia.Entidade;
using ProjetoBarAcademia.Repositorio.Base;
using System.Linq;

namespace ProjetoBarAcademia.Repositorio
{
    public class RepositorioGarcom : RepositorioBase<Garcom>
    {
        public bool VerificarCpfExistente(string cpf, int idExcecao = 0)
        {
            return registros.Any(g => g.Cpf == cpf && g.Id != idExcecao);
        }
    }
}
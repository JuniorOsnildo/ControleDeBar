// Repositorio/RepositorioMesa.cs
using ProjetoBarAcademia.Entidade;
using ProjetoBarAcademia.Repositorio.Base;
using System.Linq;

namespace ProjetoBarAcademia.Repositorio
{
    public class RepositorioMesa : RepositorioBase<Mesa>
    {
        public bool VerificarNumeroExistente(int numero, int idExcecao = 0)
        {
            return registros.Any(m => m.Numero == numero && m.Id != idExcecao);
        }
    }
}
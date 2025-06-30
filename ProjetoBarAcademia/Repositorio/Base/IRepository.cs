using System.Collections.Generic;

namespace ProjetoBarAcademia.Repositorio.Base
{
    public interface IRepositorio<T> where T : class
    {
        void Inserir(T entidade);
        void Editar(int id, T entidade);
        void Excluir(int id);
        T SelecionarPorId(int id);
        List<T> SelecionarTodos();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoBarAcademia.Repositorio.Base
{
    public abstract class RepositorioBase<T> : IRepositorio<T> where T : class
    {
        protected static readonly List<T> registros = new List<T>();
        private static int contadorId = 1;

        public virtual void Inserir(T entidade)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null && idProperty.PropertyType == typeof(int))
            {
                idProperty.SetValue(entidade, contadorId++);
            }
            registros.Add(entidade);
        }

        public virtual void Editar(int id, T entidadeAtualizada)
        {
            var registroExistente = SelecionarPorId(id);
            if (registroExistente != null)
            {
                var index = registros.IndexOf(registroExistente);
                registros[index] = entidadeAtualizada;

                var idProperty = typeof(T).GetProperty("Id");
                if (idProperty != null)
                {
                    idProperty.SetValue(entidadeAtualizada, id);
                }
            }
        }

        public virtual void Excluir(int id)
        {
            var registroParaExcluir = SelecionarPorId(id);
            if (registroParaExcluir != null)
            {
                registros.Remove(registroParaExcluir);
            }
        }

        public T SelecionarPorId(int id)
        {
            return registros.FirstOrDefault(r =>
            {
                var idProperty = r.GetType().GetProperty("Id");
                if (idProperty != null)
                {
                    var valorId = (int)idProperty.GetValue(r);
                    return valorId == id;
                }
                return false;
            });
        }

        public List<T> SelecionarTodos()
        {
            return new List<T>(registros);
        }
    }
}
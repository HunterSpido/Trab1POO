using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPOO.Repository
{
    public interface IRepository<T> where T : class
    {
        public void Adicionar(T obj);
        public void Remover(T obj);
        public IList<T> Listar();
        public void Alterar(T obj);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPOO.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public void Adicionar(T obj);
        public void Remover(T obj);
        public List<T> Listar();
        public void Alterar(T obj);
        public void Salvar();
        public void Carregar();

    }
}

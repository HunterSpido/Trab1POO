using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPOO.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        bool Adicionar(T obj);
        bool Remover(T obj);
        List<T> Listar();
        bool Alterar(T obj);
        void Salvar();
        void Carregar();

    }
}

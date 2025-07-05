using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoPOO.Models;

namespace ProjetoPOO.Repository.Interfaces
{
    public interface IRepositoryFornecedor
    {
        bool Adicionar(Fornecedor obj);
        bool Remover(Fornecedor obj);
        List<Fornecedor> Listar();
        bool Alterar(Fornecedor obj);
        void Salvar();
        void Carregar();

    }
}

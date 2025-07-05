using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoPOO.Models;

namespace ProjetoPOO.Repository.Interfaces
{
    public interface IRepositoryProduto
    {
        bool Adicionar(Produto obj);
        bool Alterar(Produto obj);
        bool Remover(Produto obj);
        List<Produto> Listar();
        void Carregar();
        void Salvar();
        List<Produto> ObterPorNomeOuDescricao(string termo);
        List<Produto> ObterPorFornecedor(int fornecedorId);
    }
}

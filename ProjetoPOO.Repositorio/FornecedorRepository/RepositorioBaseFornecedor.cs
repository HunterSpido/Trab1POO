using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Repository.FornecedorRepository
{
    public class RepositorioBaseFornecedor
    {
        private IRepositoryFornecedor repositorioFornecedor;
        public RepositorioBaseFornecedor(IRepositoryFornecedor repositoryFornecedor)
        {
            repositorioFornecedor= repositoryFornecedor;
        }
        public void adicionarFornecedor(Fornecedor fornecedor)
        {
            repositorioFornecedor.Adicionar(fornecedor);
        }
        public void removerFornecedor(Fornecedor fornecedor)
        {
            repositorioFornecedor.Remover(fornecedor);
        }
        public void listarFornecedores()
        {
            repositorioFornecedor.Listar();
        }
        public void salvarFornecedores()
        {
            repositorioFornecedor.Salvar();
        }
        public void carregarFornecedores()
        {
            repositorioFornecedor.Carregar();
        }
    }
}

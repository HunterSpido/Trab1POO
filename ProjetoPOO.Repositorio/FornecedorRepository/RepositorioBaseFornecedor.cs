using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace ProjetoPOO.Repository.FornecedorRepository
{
    public class RepositorioBaseFornecedor
    {
        private readonly IRepository<Fornecedor> _repositorioFornecedor;

        public RepositorioBaseFornecedor(IRepository<Fornecedor> repositorioFornecedor)
        {
            _repositorioFornecedor = repositorioFornecedor ?? throw new ArgumentNullException(nameof(repositorioFornecedor));
        }

        public bool AdicionarFornecedor(Fornecedor fornecedor)
        {
            try
            {
                _repositorioFornecedor.Adicionar(fornecedor);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoverFornecedor(Fornecedor fornecedor)
        {
            try
            {
                _repositorioFornecedor.Remover(fornecedor);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AlterarFornecedor(Fornecedor fornecedor)
        {
            try
            {
                _repositorioFornecedor.Alterar(fornecedor);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Fornecedor> ListarFornecedores()
        {
            return _repositorioFornecedor.Listar();
        }

        public void SalvarFornecedores()
        {
            _repositorioFornecedor.Salvar();
        }

        public void CarregarFornecedores()
        {
            _repositorioFornecedor.Carregar();
        }
    }
}

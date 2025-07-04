using System;
using System.Collections.Generic;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Repository.ClienteRepository
{
    public class RepositorioBaseCliente
    {
        private readonly IRepositoryClientes repositoryClientes;

        public RepositorioBaseCliente(IRepositoryClientes repositoryClientes)
        {
            this.repositoryClientes = repositoryClientes ?? throw new ArgumentNullException(nameof(repositoryClientes));
        }

        public bool AdicionarCliente(Cliente cliente)
        {
            try
            {
                if (cliente == null) throw new ArgumentNullException(nameof(cliente));
                return repositoryClientes.Adicionar(cliente);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar cliente: {ex.Message}");
                return false;
            }
        }

        public bool ValidarLogin(string nome, string senha)
        {
            try
            {
                return repositoryClientes.ValidarLogin(nome, senha);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao validar login: {ex.Message}");
                return false;
            }
        }

        public Cliente ConsultarPorNomeESenha(string nome, string senha)
        {
            try
            {
                return repositoryClientes.ConsultarPorNomeESenha(nome, senha);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao consultar cliente: {ex.Message}");
                return null;
            }
        }

        public List<Cliente> Listar()
        {
            try
            {
                return repositoryClientes.Listar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar clientes: {ex.Message}");
                return new List<Cliente>();
            }
        }

        public void Carregar()
        {
            try
            {
                repositoryClientes.Carregar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar dados: {ex.Message}");
            }
        }

        public void AlterarCliente(Cliente cliente)
        {
            try
            {
                if (cliente == null) throw new ArgumentNullException(nameof(cliente));
                repositoryClientes.AlterarCliente(cliente);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao alterar cliente: {ex.Message}");
            }
        }

        public void RemoverCliente(Cliente cliente)
        {
            try
            {
                if (cliente == null) throw new ArgumentNullException(nameof(cliente));
                repositoryClientes.Remover(cliente);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao remover cliente: {ex.Message}");
            }
        }

        public void Salvar()
        {
            try
            {
                repositoryClientes.Salvar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar dados: {ex.Message}");
            }
        }

        public void ListarPedidos()
        {
            try
            {
                repositoryClientes.ListarPedidos();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar pedidos: {ex.Message}");
            }
        }
    }
}

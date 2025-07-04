using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Repository.ClienteRepository
{
    public class ClienteRepositoryList : IRepositoryClientes
    {
        private readonly List<Cliente> clientes = new();

        public bool Adicionar(Cliente cliente)
        {
            clientes.Add(cliente);
            return true;
        }

        public bool ValidarLogin(string nome, string senha)
        {
            return clientes.Exists(c => c.Nome == nome && c.Senha == senha);
        }

        public Cliente ConsultarPorNomeESenha(string nome, string senha)
        {
            return clientes.Find(c => c.Nome == nome && c.Senha == senha);
        }

        public List<Cliente> Listar()
        {
            return new List<Cliente>(clientes);
        }

        public void Carregar()
        {
            if (File.Exists("clientes_lista.json"))
            {
                var json = File.ReadAllText("clientes_lista.json");
                var lista = JsonSerializer.Deserialize<List<Cliente>>(json);
                if (lista != null)
                {
                    clientes.Clear();
                    clientes.AddRange(lista);
                }
            }
        }

        public void AlterarCliente(Cliente cliente)
        {
            var idx = clientes.FindIndex(c => c.IdCliente == cliente.IdCliente);
            if (idx >= 0)
                clientes[idx] = cliente;
        }

        public void Remover(Cliente cliente)
        {
            clientes.RemoveAll(c => c.IdCliente == cliente.IdCliente);
        }

        public void Salvar()
        {
            var json = JsonSerializer.Serialize(clientes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("clientes_lista.json", json);
        }

        public void ListarPedidos()
        {
            foreach (var cliente in clientes)
            {
                Console.WriteLine($"Cliente: {cliente.Nome} - Id: {cliente.IdCliente}");
                // Implemente a exibição dos pedidos do cliente se houver
            }
        }
    }
}
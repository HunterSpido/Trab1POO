using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Repository.ClienteRepository
{
    public class ClienteRepositoryVetor : IRepositoryClientes
    {
        private Cliente[] clientes = new Cliente[100];
        private int totalClientes = 0;

        public bool Adicionar(Cliente cliente)
        {
            if (totalClientes >= clientes.Length)
                return false;
            clientes[totalClientes++] = cliente;
            return true;
        }

        public bool ValidarLogin(string nome, string senha)
        {
            for (int i = 0; i < totalClientes; i++)
                if (clientes[i].Nome == nome && clientes[i].Senha == senha)
                    return true;
            return false;
        }

        public Cliente ConsultarPorNomeESenha(string nome, string senha)
        {
            for (int i = 0; i < totalClientes; i++)
                if (clientes[i].Nome == nome && clientes[i].Senha == senha)
                    return clientes[i];
            return null;
        }

        public List<Cliente> Listar()
        {
            var lista = new List<Cliente>();
            for (int i = 0; i < totalClientes; i++)
                lista.Add(clientes[i]);
            return lista;
        }

        public void Carregar()
        {
            if (File.Exists("clientes_vetor.json"))
            {
                var json = File.ReadAllText("clientes_vetor.json");
                var lista = JsonSerializer.Deserialize<List<Cliente>>(json);
                if (lista != null)
                {
                    totalClientes = Math.Min(lista.Count, clientes.Length);
                    for (int i = 0; i < totalClientes; i++)
                        clientes[i] = lista[i];
                }
            }
        }

        public void AlterarCliente(Cliente cliente)
        {
            for (int i = 0; i < totalClientes; i++)
                if (clientes[i].IdCliente == cliente.IdCliente)
                    clientes[i] = cliente;
        }

        public void Remover(Cliente cliente)
        {
            for (int i = 0; i < totalClientes; i++)
            {
                if (clientes[i].IdCliente == cliente.IdCliente)
                {
                    for (int j = i; j < totalClientes - 1; j++)
                        clientes[j] = clientes[j + 1];
                    clientes[--totalClientes] = null!;
                    break;
                }
            }
        }

        public void Salvar()
        {
            var lista = Listar();
            var json = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("clientes_vetor.json", json);
        }

        public void ListarPedidos()
        {
            for (int i = 0; i < totalClientes; i++)
            {
                Console.WriteLine($"Cliente: {clientes[i].Nome} - Id: {clientes[i].IdCliente}");
                // Implemente a exibição dos pedidos do cliente se houver
            }
        }
    }
}
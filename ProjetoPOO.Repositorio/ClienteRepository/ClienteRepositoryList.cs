using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Repository.ClienteRepository
{
    public class ClienteRepositoryList : IClienteRepository
    {
        private List<Cliente> _clientes = new List<Cliente>();
        private const string FileName = "clientes_lista.json";

        public ClienteRepositoryList() => Carregar();

        public void Carregar()
        {
            if (!File.Exists(FileName)) return;
            var json = File.ReadAllText(FileName);
            var lista = JsonSerializer.Deserialize<List<Cliente>>(json);
            if (lista != null) _clientes = lista;
        }

        public void Salvar()
        {
            var json = JsonSerializer.Serialize(_clientes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FileName, json);
        }

        public bool Adicionar(Cliente cliente)
        {
            if (cliente == null) return false;
            cliente.Id = _clientes.Select(c => c.Id).DefaultIfEmpty(0).Max() + 1;
            _clientes.Add(cliente);
            Salvar();
            return true;
        }

        public bool Alterar(Cliente cliente)
        {
            if (cliente == null) return false;
            int idx = _clientes.FindIndex(c => c.Id == cliente.Id);
            if (idx < 0) return false;
            _clientes[idx] = cliente;
            Salvar();
            return true;
        }

        public bool Remover(Cliente cliente)
        {
            if (cliente == null) return false;
            bool removed = _clientes.RemoveAll(c => c.Id == cliente.Id) > 0;
            if (removed) Salvar();
            return removed;
        }

        public List<Cliente> Listar()
        {
            return new List<Cliente>(_clientes);
        }

        public Cliente? ObterPorId(int id)
        {
            return _clientes.FirstOrDefault(c => c.Id == id);
        }

        public bool ValidarLogin(string nome, string senha)
        {
            return ObterPorNomeESenha(nome, senha) != null;
        }

        public Cliente? ObterPorNomeESenha(string nome, string senha)
        {
            return _clientes
                .FirstOrDefault(c =>
                    c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase)
                    && c.Senha == senha);
        }

        public List<Cliente> ObterPorNome(string termo)
        {
            termo = termo?.ToLower() ?? "";
            return _clientes
                .Where(c => c.Nome?.ToLower().Contains(termo) == true)
                .ToList();
        }
    }
}

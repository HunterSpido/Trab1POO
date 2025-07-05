using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Repository.ClienteRepository
{
    public class ClienteRepositoryVetor : IClienteRepository
    {
        private Cliente[] _clientes = new Cliente[100];
        private int _count = 0;
        private const string FileName = "clientes_vetor.json";

        public ClienteRepositoryVetor() => Carregar();

        public void Carregar()
        {
            if (!File.Exists(FileName)) return;
            var json = File.ReadAllText(FileName);
            var lista = JsonSerializer.Deserialize<List<Cliente>>(json);
            if (lista == null) return;

            _count = Math.Min(lista.Count, _clientes.Length);
            for (int i = 0; i < _count; i++)
                _clientes[i] = lista[i];
        }

        public void Salvar()
        {
            var lista = new List<Cliente>();
            for (int i = 0; i < _count; i++)
                lista.Add(_clientes[i]);

            var json = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FileName, json);
        }

        public bool Adicionar(Cliente cliente)
        {
            if (cliente == null || _count >= _clientes.Length) return false;
            cliente.Id = _clientes
                .Take(_count)
                .Select(c => c.Id)
                .DefaultIfEmpty(0)
                .Max() + 1;
            _clientes[_count++] = cliente;
            Salvar();
            return true;
        }

        public bool Alterar(Cliente cliente)
        {
            if (cliente == null) return false;
            for (int i = 0; i < _count; i++)
            {
                if (_clientes[i].Id == cliente.Id)
                {
                    _clientes[i] = cliente;
                    Salvar();
                    return true;
                }
            }
            return false;
        }

        public bool Remover(Cliente cliente)
        {
            if (cliente == null) return false;
            for (int i = 0; i < _count; i++)
            {
                if (_clientes[i].Id == cliente.Id)
                {
                    // desloca todos à esquerda
                    for (int j = i; j < _count - 1; j++)
                        _clientes[j] = _clientes[j + 1];
                    _clientes[--_count] = null!;
                    Salvar();
                    return true;
                }
            }
            return false;
        }

        public List<Cliente> Listar()
        {
            var lista = new List<Cliente>();
            for (int i = 0; i < _count; i++)
                lista.Add(_clientes[i]);
            return lista;
        }

        public Cliente? ObterPorId(int id)
        {
            for (int i = 0; i < _count; i++)
                if (_clientes[i].Id == id)
                    return _clientes[i];
            return null;
        }

        public bool ValidarLogin(string nome, string senha)
        {
            return ObterPorNomeESenha(nome, senha) != null;
        }

        public Cliente? ObterPorNomeESenha(string nome, string senha)
        {
            for (int i = 0; i < _count; i++)
                if (_clientes[i].Nome.Equals(nome, StringComparison.OrdinalIgnoreCase)
                    && _clientes[i].Senha == senha)
                    return _clientes[i];
            return null;
        }

        public List<Cliente> ObterPorNome(string termo)
        {
            termo = termo?.ToLower() ?? "";
            var resultados = new List<Cliente>();
            for (int i = 0; i < _count; i++)
                if (_clientes[i].Nome?.ToLower().Contains(termo) == true)
                    resultados.Add(_clientes[i]);
            return resultados;
        }
    }
}

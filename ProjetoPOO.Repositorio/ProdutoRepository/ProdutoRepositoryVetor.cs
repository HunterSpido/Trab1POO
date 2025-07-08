// ProjetoPOO/Repository/ProdutoRepository/ProdutoRepositoryVetor.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Repository.ProdutoRepository
{
    public class ProdutoRepositoryVetor : IRepositoryProduto
    {
        private Produto[] _produtos = new Produto[100];
        private int _count = 0;
        private const string FileName = "produtos_vetor.json";

        public ProdutoRepositoryVetor()
        {
            Carregar();
        }

        public void Carregar()
        {
            if (!File.Exists(FileName)) return;
            var json = File.ReadAllText(FileName);
            var lista = JsonSerializer.Deserialize<List<Produto>>(json);
            if (lista == null) return;
            _count = Math.Min(lista.Count, _produtos.Length);
            for (int i = 0; i < _count; i++)
                _produtos[i] = lista[i];
        }

        public void Salvar()
        {
            var lista = new List<Produto>();
            for (int i = 0; i < _count; i++)
                lista.Add(_produtos[i]);
            var json = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FileName, json);
        }

        public bool Adicionar(Produto obj)
        {
            if (obj == null || _count >= _produtos.Length) return false;
            obj.Id = _produtos
                .Take(_count)
                .Select(p => p.Id)
                .DefaultIfEmpty(-1)
                .Max() + 1;
            _produtos[_count++] = obj;
            Salvar();
            return true;
        }

        public bool Alterar(Produto obj)
        {
            if (obj == null) return false;
            for (int i = 0; i < _count; i++)
            {
                if (_produtos[i].Id == obj.Id)
                {
                    _produtos[i] = obj;
                    Salvar();
                    return true;
                }
            }
            return false;
        }

        public bool Remover(Produto obj)
        {
            if (obj == null) return false;
            for (int i = 0; i < _count; i++)
            {
                if (_produtos[i].Id == obj.Id)
                {
                    for (int j = i; j < _count - 1; j++)
                        _produtos[j] = _produtos[j + 1];
                    _produtos[--_count] = null!;
                    Salvar();
                    return true;
                }
            }
            return false;
        }

        public List<Produto> Listar()
        {
            var lista = new List<Produto>();
            for (int i = 0; i < _count; i++)
                lista.Add(_produtos[i]);
            return lista;
        }

        public List<Produto> ObterPorNomeOuDescricao(string termo)
        {
            if (string.IsNullOrWhiteSpace(termo)) return Listar();
            termo = termo.ToLower();
            var resultados = new List<Produto>();
            for (int i = 0; i < _count; i++)
            {
                var p = _produtos[i];
                if (p.Nome.ToLower().Contains(termo) ||
                    p.Descricao.ToLower().Contains(termo))
                {
                    resultados.Add(p);
                }
            }
            return resultados;
        }

        public List<Produto> ObterPorFornecedor(int fornecedorId)
        {
            var resultados = new List<Produto>();
            for (int i = 0; i < _count; i++)
            {
                var p = _produtos[i];
                if (p.Fornecedor != null && p.Fornecedor.Id == fornecedorId)
                    resultados.Add(p);
            }
            return resultados;
        }
    }
}

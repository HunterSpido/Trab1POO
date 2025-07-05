// ProjetoPOO/Repository/ProdutoRepository/ProdutoRepositoryList.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Repository.ProdutoRepository
{
    public class ProdutoRepositoryList : IRepositoryProduto
    {
        private List<Produto> _produtos = new List<Produto>();
        private const string FileName = "produtos_lista.json";

        public ProdutoRepositoryList()
        {
            Carregar();
        }

        public void Carregar()
        {
            if (!File.Exists(FileName)) return;
            var json = File.ReadAllText(FileName);
            var lista = JsonSerializer.Deserialize<List<Produto>>(json);
            if (lista != null) _produtos = lista;
        }

        public void Salvar()
        {
            var json = JsonSerializer.Serialize(_produtos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FileName, json);
        }

        public bool Adicionar(Produto obj)
        {
            if (obj == null) return false;
            obj.Id = _produtos.Count == 0 ? 0 : _produtos.Max(p => p.Id) + 1;
            _produtos.Add(obj);
            Salvar();
            return true;
        }

        public bool Alterar(Produto obj)
        {
            if (obj == null) return false;
            int idx = _produtos.FindIndex(p => p.Id == obj.Id);
            if (idx < 0) return false;
            _produtos[idx] = obj;
            Salvar();
            return true;
        }

        public bool Remover(Produto obj)
        {
            if (obj == null) return false;
            bool removed = _produtos.RemoveAll(p => p.Id == obj.Id) > 0;
            if (removed) Salvar();
            return removed;
        }

        public List<Produto> Listar()
            => new List<Produto>(_produtos);

        public List<Produto> ObterPorNomeOuDescricao(string termo)
        {
            if (string.IsNullOrWhiteSpace(termo)) return Listar();
            termo = termo.ToLower();
            return _produtos
                .Where(p =>
                    p.Nome.ToLower().Contains(termo) ||
                    p.Descricao.ToLower().Contains(termo))
                .ToList();
        }

        public List<Produto> ObterPorFornecedor(int fornecedorId)
            => _produtos
                .Where(p => p.Fornecedor != null && p.Fornecedor.Id == fornecedorId)
                .ToList();
    }
}

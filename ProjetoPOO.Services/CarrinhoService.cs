using ProjetoPOO.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoPOO.Services
{
    public class CarrinhoService
    {
        private readonly List<PedidoItem> _itens = new();

        public IReadOnlyList<PedidoItem> Itens => _itens;

        public void AdicionarProduto(Produto produto, int quantidade)
        {
            var existente = _itens.FirstOrDefault(x => x.Produto == produto);
            if (existente != null)
                existente.Quantidade += quantidade;
            else
                _itens.Add(new PedidoItem
                {
                    Produto = produto,
                    Quantidade = quantidade,
                    PrecoUnitario = (decimal)produto.Preco
                });
        }

        public void RemoverProduto(Produto produto)
        {
            _itens.RemoveAll(x => x.Produto == produto);
        }

        public void Limpar()
        {
            _itens.Clear();
        }

        public decimal CalcularTotal()
        {
            return _itens.Sum(x => x.PrecoTotal);
        }
    }
}

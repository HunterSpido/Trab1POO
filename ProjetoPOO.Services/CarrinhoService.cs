using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;
using ProjetoPOO.Services.Exceptions;
using System;
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
            if (produto == null)
                throw new ExcecaoEntidadeNaoEncontrada("Produto não encontrado.");

            if (quantidade <= 0)
                throw new ExcecaoEntradaInvalida("A quantidade deve ser maior que zero.");

            if (quantidade > produto.Estoque)
                throw new ExcecaoEstoqueInsuficiente(produto.Estoque);

            var existente = _itens.FirstOrDefault(i => i.ProdutoId == produto.Id);
            if (existente != null)
            {
                existente.Quantidade += quantidade;
                existente.PrecoTotal = existente.Quantidade * existente.PrecoUnitario;
            }
            else
            {
                _itens.Add(new PedidoItem
                {
                    Produto = produto,
                    ProdutoId = produto.Id,
                    Quantidade = quantidade,
                    PrecoUnitario = (decimal)produto.Preco,
                    PrecoTotal = (decimal)produto.Preco * quantidade
                });
            }
        }

        public decimal CalcularTotal()
        {
            return _itens.Sum(i => i.PrecoTotal);
        }

        public void Limpar()
        {
            _itens.Clear();
        }

        public void Visualizar()
        {
            if (!_itens.Any())
            {
                Console.WriteLine("Carrinho vazio!");
                return;
            }

            foreach (var item in _itens)
                Console.WriteLine($"Produto: {item.Produto.Nome}, Qtd: {item.Quantidade}, Total: {item.PrecoTotal:C}");

            Console.WriteLine($"Total do carrinho: {CalcularTotal():C}");
        }

        public void AdicionarProdutoViaConsole(ProdutoService produtoService)
        {
            Console.Write("Digite o código do produto: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
                throw new ExcecaoEntradaInvalida("ID do produto inválido.");

            var produto = produtoService.ObterPorId(id);

            if (produto.Estoque <= 0)
                throw new ExcecaoEstoqueInsuficiente("Produto sem estoque disponível.");

            Console.Write("Quantidade: ");
            if (!int.TryParse(Console.ReadLine(), out int qtd))
                throw new ExcecaoEntradaInvalida("Quantidade inválida.");

            AdicionarProduto(produto, qtd);
            Console.WriteLine("Produto adicionado ao carrinho!");
        }

        public void FinalizarPedidoViaConsole(
            Cliente cliente,
            ProdutoService produtoService,
            TransportadoraService transportadoraService,
            PedidoService pedidoService)
        {
            if (!_itens.Any())
                throw new ExcecaoEntradaInvalida("Carrinho vazio! Não é possível finalizar o pedido.");

            Console.Write("Determine a distância (km): ");
            if (!double.TryParse(Console.ReadLine(), out double distancia) || distancia <= 0)
                throw new ExcecaoEntradaInvalida("Distância inválida.");

            var transportadoras = transportadoraService.GetTodos();
            if (transportadoras.Count == 0)
                throw new ExcecaoEntidadeNaoEncontrada("Nenhuma transportadora disponível.");

            Console.WriteLine("Escolha uma transportadora:");
            for (int i = 0; i < transportadoras.Count; i++)
                Console.WriteLine($"{i + 1} - {transportadoras[i].Nome} (R$ {transportadoras[i].PrecoPorKm} por km)");

            Console.Write("Opção: ");
            if (!int.TryParse(Console.ReadLine(), out int idxT) || idxT < 1 || idxT > transportadoras.Count)
                throw new ExcecaoEntradaInvalida("Opção inválida de transportadora.");

            var transportadora = transportadoras[idxT - 1];

            decimal valorFrete = (decimal)distancia * (decimal)transportadora.PrecoPorKm;
            decimal totalItens = CalcularTotal();
            decimal valorTotal = totalItens + valorFrete;

            Console.WriteLine($"Total dos itens: {totalItens:C}");
            Console.WriteLine($"Frete: {valorFrete:C}");
            Console.WriteLine($"Total do pedido: {valorTotal:C}");
            Console.Write("Confirmar pedido? (s/n): ");
            var confirmacao = Console.ReadLine();
            if (confirmacao?.ToLower() != "s")
            {
                Console.WriteLine("Pedido cancelado.");
                return;
            }

            foreach (var item in _itens)
            {
                var produto = produtoService.ObterPorId(item.ProdutoId);
                if (produto == null)
                    throw new ExcecaoEntidadeNaoEncontrada($"Produto {item.ProdutoId} não encontrado durante finalização.");

                if (item.Quantidade > produto.Estoque)
                    throw new ExcecaoEstoqueInsuficiente($"Produto {produto.Nome} não tem estoque suficiente.");

                produto.Estoque -= item.Quantidade;
                produtoService.Atualizar(produto);
            }

            var pedido = new Pedido
            {
                Cliente = cliente,
                Data = DateTime.Now,
                Itens = _itens.ToList(),
                ValorFrete = valorFrete,
                ValorTotal = valorTotal,
                Transportadora = transportadora,
                Status = "Aguardando"
            };

            pedidoService.RealizarPedido(pedido);
            Limpar();
            Console.WriteLine("Pedido realizado com sucesso!");
        }
    }
}

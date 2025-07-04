//using System;
//using ProjetoPOO.Models;
//using ProjetoPOO.Repository.Interfaces;
//using System.Collections.Generic;

//namespace ProjetoPOO.Services
//{
//    public class ProdutoService
//    {
//        private readonly FornecedorService fornecedorService;
//        private Produto[] vetorProdutos = new Produto[100];
//        private int qtdProdutos = 0;
//        private int idProduto = 0;

//        // Recebe o repositório de fornecedores para passar para o FornecedorService
//        public ProdutoService(IRepository<Fornecedor> repositorioFornecedor)
//        {
//            fornecedorService = new FornecedorService(repositorioFornecedor);
//        }

//        public void Adicionar()
//        {
//            if (qtdProdutos >= vetorProdutos.Length)
//            {
//                Console.WriteLine("Limite de produtos atingido!");
//                return;
//            }

//            Console.Write("Nome do produto: ");
//            string nome = Console.ReadLine() ?? "";

//            Console.Write("Descrição: ");
//            string descricao = Console.ReadLine() ?? "";

//            Console.Write("Preço: ");
//            string precoStr = Console.ReadLine() ?? "";
//            if (!double.TryParse(precoStr, out double preco))
//            {
//                Console.WriteLine("Preço inválido.");
//                return;
//            }

//            Console.Write("Quantidade em estoque: ");
//            string estoqueStr = Console.ReadLine() ?? "";
//            if (!int.TryParse(estoqueStr, out int estoque))
//            {
//                Console.WriteLine("Quantidade inválida.");
//                return;
//            }

//            var fornecedores = fornecedorService.ListarFornecedores();
//            if (fornecedores.Count == 0)
//            {
//                Console.WriteLine("Nenhum fornecedor cadastrado.");
//                return;
//            }

//            Console.WriteLine("\nFornecedores disponíveis:");
//            foreach (var forn in fornecedores)
//            {
//                Console.WriteLine($"{forn.IdFornecedor} - {forn.Nome}");
//            }

//            Console.Write("ID do Fornecedor: ");
//            string idFornStr = Console.ReadLine() ?? "";
//            if (!int.TryParse(idFornStr, out int idFornecedor))
//            {
//                Console.WriteLine("ID inválido.");
//                return;
//            }

//            var fornecedor = fornecedorService.ConsultarPorId(idFornecedor);
//            if (fornecedor == null)
//            {
//                Console.WriteLine("Fornecedor não encontrado!");
//                return;
//            }

//            var produto = new Produto
//            {
//                IdProduto = idProduto++,
//                Nome = nome,
//                Descricao = descricao,
//                Preco = preco,
//                Estoque = estoque,
//                Fornecedor = fornecedor
//            };

//            vetorProdutos[qtdProdutos++] = produto;
//            Console.WriteLine("Produto cadastrado com sucesso!");
//        }

//        public void Alterar()
//        {
//            Console.Write("ID do produto a alterar: ");
//            string idStr = Console.ReadLine() ?? "";
//            if (!int.TryParse(idStr, out int id))
//            {
//                Console.WriteLine("ID inválido.");
//                return;
//            }

//            for (int i = 0; i < qtdProdutos; i++)
//            {
//                if (vetorProdutos[i].IdProduto == id)
//                {
//                    Console.Write("Novo nome: ");
//                    vetorProdutos[i].Nome = Console.ReadLine() ?? "";

//                    Console.Write("Nova descrição: ");
//                    vetorProdutos[i].Descricao = Console.ReadLine() ?? "";

//                    Console.Write("Novo preço: ");
//                    string novoPrecoStr = Console.ReadLine() ?? "";
//                    vetorProdutos[i].Preco = double.TryParse(novoPrecoStr, out double novoPreco) ? novoPreco : vetorProdutos[i].Preco;

//                    Console.Write("Novo estoque: ");
//                    string novoEstoqueStr = Console.ReadLine() ?? "";
//                    vetorProdutos[i].Estoque = int.TryParse(novoEstoqueStr, out int novoEstoque) ? novoEstoque : vetorProdutos[i].Estoque;

//                    Console.WriteLine("Produto alterado!");
//                    return;
//                }
//            }
//            Console.WriteLine("Produto não encontrado.");
//        }

//        public void Excluir()
//        {
//            Console.Write("Digite o ID do Produto a excluir: ");
//            string idStr = Console.ReadLine() ?? "";
//            if (!int.TryParse(idStr, out int id))
//            {
//                Console.WriteLine("ID inválido.");
//                return;
//            }

//            for (int i = 0; i < qtdProdutos; i++)
//            {
//                if (vetorProdutos[i].IdProduto == id)
//                {
//                    for (int j = i; j < qtdProdutos - 1; j++)
//                    {
//                        vetorProdutos[j] = vetorProdutos[j + 1];
//                    }

//                    vetorProdutos[qtdProdutos - 1] = null!;
//                    qtdProdutos--;

//                    Console.WriteLine("Produto excluído com sucesso!");
//                    return;
//                }
//            }

//            Console.WriteLine("Produto não encontrado.");
//        }

//        private void Exibir(Produto p)
//        {
//            Console.WriteLine("\n=== DADOS DO PRODUTO ===");
//            Console.WriteLine($"ID: {p.IdProduto}");
//            Console.WriteLine($"Nome: {p.Nome}");
//            Console.WriteLine($"Descrição: {p.Descricao}");
//            Console.WriteLine($"Preço: R${p.Preco:F2}");
//            Console.WriteLine($"Estoque: {p.Estoque} unidades");
//            Console.WriteLine($"Fornecedor: {p.Fornecedor.Nome} (ID: {p.Fornecedor.IdFornecedor})");
//        }

//        public void Consultar()
//        {
//            Console.WriteLine("\n--- Tipo de Consulta ---");
//            Console.WriteLine("1 - Por ID");
//            Console.WriteLine("2 - Por Nome (busca parcial)");
//            Console.Write("Opção: ");

//            string opcao = Console.ReadLine() ?? "";

//            switch (opcao)
//            {
//                case "1":
//                    ConsultarId();
//                    break;
//                case "2":
//                    ConsultarPorNome();
//                    break;
//                default:
//                    Console.WriteLine("Opção inválida.");
//                    break;
//            }
//        }

//        public void ConsultarPorNome()
//        {
//            Console.Write("Digite a parte do nome para buscar: ");
//            string termo = Console.ReadLine()!.ToLower();

//            bool encontrou = false;

//            for (int i = 0; i < qtdProdutos; i++)
//            {
//                if (vetorProdutos[i] == null) continue;

//                string nomeLower = vetorProdutos[i].Nome.ToLower();

//                if (nomeLower.Contains(termo))
//                {
//                    Exibir(vetorProdutos[i]);
//                    Console.WriteLine("---------------------");
//                    encontrou = true;
//                }
//            }

//            if (!encontrou)
//            {
//                Console.WriteLine($"Nenhum produto encontrado com: {termo}");
//            }
//        }

//        public Fornecedor ConsultarPorId(int id)
//        {
//            // Implementar o método conforme o seu FornecedorService
//            return fornecedorService.ConsultarPorId(id);
//        }

//        public void ConsultarId()
//        {
//            Console.Write("Digite o id que deseja consultar: ");
//            string idStr = Console.ReadLine() ?? "";
//            if (!int.TryParse(idStr, out int id))
//            {
//                Console.WriteLine("ID inválido.");
//                return;
//            }

//            for (int i = 0; i < qtdProdutos; i++)
//            {
//                if (vetorProdutos[i].IdProduto == id)
//                {
//                    Exibir(vetorProdutos[i]);
//                    return;
//                }
//            }

//            Console.WriteLine("Produto não encontrado");
//        }
//    }
//}

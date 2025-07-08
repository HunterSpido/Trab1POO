using System;
using ProjetoPOO.Services;
using ProjetoPOO.Services.Exceptions;

namespace ProjetoPOO.Menu
{
    public class ProdutoMenu
    {
        private readonly ProdutoService _produtoService;

        public ProdutoMenu(ProdutoService produtoService)
        {
            _produtoService = produtoService ?? throw new ArgumentNullException(nameof(produtoService));
        }

        public void MenuProdutos()
        {
            while (true)
            {
                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1 - Adicionar produto");
                Console.WriteLine("2 - Alterar produto");
                Console.WriteLine("3 - Excluir produto");
                Console.WriteLine("4 - Consultar produto");
                Console.WriteLine("5 - Voltar");

                Console.Write("Opção: ");
                string entrada = Console.ReadLine() ?? "";

                if (!int.TryParse(entrada, out int opcao))
                {
                    Console.WriteLine("Entrada inválida. Digite um número de 1 a 5.");
                    continue;
                }

                try
                {
                    switch (opcao)
                    {
                        case 1:
                            _produtoService.Cadastrar();
                            break;
                        case 2:
                            _produtoService.Alterar();
                            break;
                        case 3:
                            _produtoService.Remover();
                            break;
                        case 4:
                            _produtoService.Consultar();
                            break;
                        case 5:
                            return;
                        default:
                            Console.WriteLine("Digite um número entre 1 e 5.");
                            break;
                    }
                }
                catch (ExcecaoEntradaInvalida ex)
                {
                    Console.WriteLine($"[Entrada inválida] {ex.Message}");
                }
                catch (ExcecaoEntidadeNaoEncontrada ex)
                {
                    Console.WriteLine($"[Entidade não encontrada] {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Erro inesperado] {ex.Message}");
                }
            }
        }
    }
}

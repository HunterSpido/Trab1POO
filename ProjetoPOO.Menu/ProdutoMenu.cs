using System;
using ProjetoPOO.Services;
namespace ProjetoPOO.Menu;

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
            Console.WriteLine("Selecione uma opção");
            Console.WriteLine("1- Adicone um produto");
            Console.WriteLine("2- Altere um produto");
            Console.WriteLine("3- Exclua um produto");
            Console.WriteLine("4- Consulte um produto");
            Console.WriteLine("5- Voltar");
            int v = int.Parse(Console.ReadLine()!);
            //ProdutoService produtoService = new ProdutoService();

            switch (v)
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
                    System.Console.WriteLine("Digite entre a opção 1-4");
                    break;
            }
        }

    }
}

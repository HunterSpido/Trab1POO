using System;
using ProjetoPOO.Services;
namespace ProjetoPOO.Menu;

public class ProdutoMenu
{
    public static void MenuProdutos()
    {
        while (true)
        {
            Console.WriteLine("Selecione uma opção");
            Console.WriteLine("1- Adicone um produto");
            Console.WriteLine("2- Altere um produto");
            Console.WriteLine("3- Exclua um produto");
            Console.WriteLine("4- Consulte um produto");
            Console.WriteLine("5- Voltar para o Menu");
            int v = int.Parse(Console.ReadLine()!);

            switch (v)
            {
                case 1:
                    ProdutoService.Adicionar();
                    break;
                case 2:
                    ProdutoService.Alterar();
                    break;
                case 3:
                    ProdutoService.Excluir();
                    break;
                case 4:
                    ProdutoService.Consultar();
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

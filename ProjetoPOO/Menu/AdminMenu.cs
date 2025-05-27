namespace ProjetoPOO.Menu;
using ProjetoPOO.Classes;
using ProjetoPOO.Services;
using System;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

public static class AdminMenu
{
    public static void MenuAdmin()
    {
        int tes = 0;
        while (tes != 4) // Mantém o loop principal
        {
            System.Console.WriteLine("Selecione a opcão:");
            System.Console.WriteLine("1- Menu de Fornecedor:");
            System.Console.WriteLine("2- Menu de Produtos:");
            System.Console.WriteLine("3- Menu de Transportadora:");
            System.Console.WriteLine("4- Sair");

            tes = int.Parse(Console.ReadLine()!);

            switch (tes)
            {
                case 1:
                    Console.WriteLine("MENU FORNECEDOR");
                    MenuFornecedor.TelaFornecedor();
                    break;
                case 2:
                    Console.WriteLine("MENU PRODUTO");
                    ProdutoMenu.MenuProdutos();
                    break;
                case 3:
                    Console.WriteLine("MENU TRANSPORTADORA");
                    TransportadoraMenu.MenuTransportadora(); 
                    break;
                case 4:
                    System.Console.WriteLine("Saindo...");
                    return; // Sai do MenuAdmin completamente
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}
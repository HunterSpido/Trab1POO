namespace ProjetoPOO.Menu;

using System;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

public static class AdminMenu
{
    
    public static void MenuAdmin()
    {
        int tes=-1;
        while (tes != 0)
        {
            System.Console.WriteLine("Selecione a opcão:");
            System.Console.WriteLine("1- Menu de Fornecedor:");
            System.Console.WriteLine("2- Menu de Produtos:");
            System.Console.WriteLine("3- Menu de Transportadora:");
            System.Console.WriteLine("4- Sair");

            tes = int.Parse(Console.ReadLine());

            switch (tes)
            {
                case 1:
                    Console.WriteLine("primeriabosta");
                    //chamar a tela de fornecedor
                    break;
                case 2:
                    Console.WriteLine("tela de prod");
                    break;
                case 3:
                    Console.WriteLine("tela de transp");
                    break;
                case 4:
                    tes = 0;
                    break;
            }


        }
    }
}
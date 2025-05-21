namespace ProjetoPOO.Menu;

using System;
using System.Security.Cryptography.X509Certificates;
using ProjetoPOO;

public static class Menu
{
    public static void telaLogin()
    {

        System.Console.WriteLine("Digite o user e a senha");
        System.Console.WriteLine("User: ");
        string? user = System.Console.ReadLine();
        System.Console.WriteLine("Digite a senha: ");
        string? password = System.Console.ReadLine();

        if (user == "admin" && password == "admin")
        {
            System.Console.WriteLine("É admin");
            AdminMenu.Men
            //chamar tela admin
        }
        else
        {
            System.Console.WriteLine("É tela normal");
            //chama menu normal
        }
    }
}
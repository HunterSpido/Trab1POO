using System;
using ProjetoPOO.Classes;
namespace ProjetoPOO.Services;

public static class ClienteService
{
    private static Cliente[] clientes = new Cliente[100];
    private static int totalClientes = 0;

    public static void CadastrarUsuario()
    {
        if (totalClientes >= clientes.Length)
        {
            Console.WriteLine("Limite de usuários atingido.");
            return;
        }

        Console.Write("Digite o nome: ");
        string nome = Console.ReadLine()!;

        Console.Write("Digite a senha: ");
        string senha = Console.ReadLine()!;

        Endereco endereco=EnderecoService.PedirEndereco();

        Console.Write("Digite o telefone: ");
        string telefone = Console.ReadLine()!;

        Console.Write("Digite o email: ");
        string email = Console.ReadLine()!;

        clientes[totalClientes] = new Cliente { Nome = nome, Senha = senha, Email=email,Telefone=telefone,Endereco=endereco };
        totalClientes++;

        Console.WriteLine("Cliente cadastrado com sucesso!");
    }

    public static bool ValidarNome(string nome, string senha)
    {
        for (int i = 0; i < totalClientes; i++)
        {
            if (clientes[i].Nome == nome && clientes[i].Senha == senha)
                return true;
        }
        return false;
    }
}

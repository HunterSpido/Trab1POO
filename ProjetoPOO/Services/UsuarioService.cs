using System;
using ProjetoPOO.Classes;
namespace ProjetoPOO.Services;

public static class UsuarioService
{
    private static Usuario[] usuarios = new Usuario[100];
    private static int totalUsuarios = 0;

    public static void CadastrarUsuario()
    {
        if (totalUsuarios >= usuarios.Length)
        {
            Console.WriteLine("Limite de usuários atingido.");
            return;
        }

        Console.Write("Digite o login: ");
        string login = Console.ReadLine()!;

        Console.Write("Digite a senha: ");
        string senha = Console.ReadLine()!;

        usuarios[totalUsuarios] = new Usuario { Login = login, Senha = senha };
        totalUsuarios++;

        Console.WriteLine("Usuário cadastrado com sucesso!");
    }

    public static bool ValidarLogin(string login, string senha)
    {
        for (int i = 0; i < totalUsuarios; i++)
        {
            if (usuarios[i].Login == login && usuarios[i].Senha == senha)
                return true;
        }
        return false;
    }
}

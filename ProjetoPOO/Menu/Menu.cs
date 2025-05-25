using ProjetoPOO.Services;
namespace ProjetoPOO.Menu;

public static class Menu
{
    public static void TelaLogin()
    {
        while (true)
        {
            Console.WriteLine("1 - Fazer login");
            Console.WriteLine("2 - Cadastrar novo usuário");
            Console.WriteLine("3 - Sair");
            Console.Write("Escolha: ");
            int opcao = int.Parse(Console.ReadLine()!);

            if (opcao == 1)
            {
                Console.Write("Login: ");
                string nome = Console.ReadLine()!;
                Console.Write("Senha: ");
                string senha = Console.ReadLine()!;

                if (nome == "admin" && senha == "admin")
                {
                    Console.WriteLine("Bem-vindo admin!");
                    Console.WriteLine(" == MENU DO ADMIN ==");
                    AdminMenu.MenuAdmin();
                    // Chama menu admin
                }
                else if (ClienteService.ValidarNome(nome, senha))
                {
                    Console.WriteLine("Login efetuado com sucesso!");
                    UsuarioNormalMenu.MenuUsuario();
                    // Chama menu usuário normal
                }
                else
                {
                    Console.WriteLine("Login ou senha incorretos.");
                }
            }
            else if (opcao == 2)
            {
                ClienteService.CadastrarUsuario();
            }
            else if (opcao == 3)
            {
                Console.WriteLine("Saindo...");
                break;
            }
            else
            {
                Console.WriteLine("Opção inválida.");
            }
        }
    }
}
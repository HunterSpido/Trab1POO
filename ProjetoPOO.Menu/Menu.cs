using ProjetoPOO.Services;
namespace ProjetoPOO.Menu;

public  class Menu
{
    public  void TelaLogin()
    {
        while (true)
        {
            Console.WriteLine("1 - Fazer login");
            Console.WriteLine("2 - Cadastrar novo usuário");
            Console.WriteLine("3 - Sair");
            Console.Write("Escolha: ");
            int opcao = int.Parse(Console.ReadLine()!);
            //ClienteService clienteService = new ClienteService();


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
                    AdminMenu adminMenu = new AdminMenu();
                    adminMenu.MenuAdmin();
                    // Chama menu admin
                }

                //else if (clienteService.ValidarNome(nome, senha))
                //{
                //    Console.WriteLine("Login efetuado com sucesso!");
                //    UsuarioNormalMenu usuarioNormalMenu = new UsuarioNormalMenu();
                //    usuarioNormalMenu.MenuUsuario();
                //    // Chama menu usuário normal
                //}
                else
                {
                    Console.WriteLine("Login ou senha incorretos.");
                }
            }
            else if (opcao == 2)
            {
                //clienteService.CadastrarUsuario();
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
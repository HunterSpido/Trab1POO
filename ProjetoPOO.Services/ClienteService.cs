
using ProjetoPOO.Models;
using ProjetoPOO.Repository;

namespace ProjetoPOO.Services
{
    public class ClienteService
    {
        private ClienteRepositoryVetor repo = new ClienteRepositoryVetor();

        public void CadastrarUsuario()
        {
            Console.Write("Digite o nome: ");
            string nome = Console.ReadLine()!;

            Console.Write("Digite a senha: ");
            string senha = Console.ReadLine()!;

            EnderecoService enderecoService = new EnderecoService();

            Endereco endereco = enderecoService.PedirEndereco();
            

            Console.Write("Digite o telefone: ");
            string telefone = Console.ReadLine()!;

            Console.Write("Digite o email: ");
            string email = Console.ReadLine()!;

            Cliente novoCliente = new Cliente
            {
                Nome = nome,
                Senha = senha,
                Email = email,
                Telefone = telefone,
                Endereco = endereco
            };

            bool sucesso = repo.Adicionar(novoCliente);

            if (sucesso)
                Console.WriteLine("Cliente cadastrado com sucesso!");
            else
                Console.WriteLine("Limite de usuários atingido.");
        }

        public bool ValidarNome(string nome, string senha)
        {
            var cliente = repo.ConsultarPorNomeESenha(nome, senha);
            return cliente != null;
        }
        public bool FazerLogin(string nome, string senha)
        {
            return repo.ValidarLogin(nome, senha);
        }
        
    }
}

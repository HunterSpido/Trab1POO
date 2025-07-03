using ProjetoPOO.Models;

namespace ProjetoPOO.Repository.ClienteRepository
{
    public class ClienteRepositoryVetor
    {
        private Cliente[] clientes;
        private int totalClientes;

        public ClienteRepositoryVetor()
        {
            clientes = new Cliente[100];
            totalClientes = 0;
        }

        // Inclusão
        public bool Adicionar(Cliente cliente)
        {
            if (totalClientes >= clientes.Length)
                return false;

            clientes[totalClientes] = cliente;
            totalClientes++;
            return true;
        }

        public bool ValidarLogin(string nome, string senha)
        {
            for (int i = 0; i < totalClientes; i++)
            {
                if (clientes[i].Nome == nome && clientes[i].Senha == senha)
                    return true;
            }
            return false;
        }
        public Cliente? ConsultarPorNomeESenha(string nome, string senha)
        {
            for (int i = 0; i < totalClientes; i++)
            {
                if (clientes[i].Nome == nome && clientes[i].Senha == senha)
                    return clientes[i];
            }
            return null;
        }



    }
}

using System;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.ClienteRepository;
using ProjetoPOO.Repository.Interfaces;
using ProjetoPOO.Services;

namespace ProjetoPOO.Test
{
    class Program
    {
        static void Main()
        {
            // 1) escolha lista ou vetor...
            IClienteRepository repo = new ClienteRepositoryVetor(); // ou List
            var enderecoSv = new EnderecoService();
            var svc = new ClienteService(repo, enderecoSv);

            // 2) Cadastrar
            svc.Cadastrar();           // digite dados de teste
            svc.ListarTodos();         // deve mostrar o que cadastrou

            // 3) Login
            Console.WriteLine(svc.EhLoginValido("nome", "senha"));

            // 4) Atualizar
            svc.Atualizar();
            svc.ListarTodos();

            // 5) Remover
            svc.Remover();
            svc.ListarTodos();

        }
    }
}

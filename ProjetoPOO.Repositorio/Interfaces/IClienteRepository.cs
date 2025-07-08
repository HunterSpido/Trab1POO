using System.Collections.Generic;
using ProjetoPOO.Models;

namespace ProjetoPOO.Repository.Interfaces
{
    public interface IClienteRepository
    {

        bool Adicionar(Cliente obj);
        bool Alterar(Cliente obj);
        bool Remover(Cliente obj);
        List<Cliente> Listar();


        void Carregar();
        void Salvar();


        Cliente? ObterPorId(int id);
        bool ValidarLogin(string nome, string senha);
        Cliente? ObterPorNomeESenha(string nome, string senha);
        List<Cliente> ObterPorNome(string termo);
    }
}

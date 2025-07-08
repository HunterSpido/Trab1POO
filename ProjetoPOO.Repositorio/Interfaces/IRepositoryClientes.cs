using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoPOO.Models;

namespace ProjetoPOO.Repository.Interfaces
{

    /*iMPLEMENTAR REPOSITORIO DE 
     * CLIENTE PARA LISTA E VETOR COM TODAS ESSAS FUNCOES*/
    public interface IRepositoryClientes
    {
        bool Adicionar(Cliente cliente);
        bool ValidarLogin(string nome, string senha);
        Cliente ConsultarPorNomeESenha(string nome, string senha);
        List<Cliente> Listar();
        void Carregar();
        void AlterarCliente(Cliente cliente);
        void Remover(Cliente cliente);
        void Salvar();
        void ListarPedidos();

        
    }
}

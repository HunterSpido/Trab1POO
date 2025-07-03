using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Repository.ClienteRepository
{
    /*TEM QUE IMPEMTAR AS FUNCOES AIDNA NAS CLASSES DE LISTA E VETOR*/
    public class RepositorioBaseCliente
    {
        private IRepositoryClientes repositoryClientes;
        public RepositorioBaseCliente(IRepositoryClientes repositoryClientes)
        {
            this.repositoryClientes = repositoryClientes;
        }
    }
}

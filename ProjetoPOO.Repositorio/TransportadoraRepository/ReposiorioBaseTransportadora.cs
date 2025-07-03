using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Repository.TransportadoraRepository
{
    /*IMPLEMENTAR FUNCOES DA INTERFACE TRANSPORTADORA AQUI*/
    public class ReposiorioBaseTransportadora
    {
        private IRepositoryTransportadora repositoryTransportadora;
        public ReposiorioBaseTransportadora(IRepositoryTransportadora repositoryTransportadora)
        {
            this.repositoryTransportadora = repositoryTransportadora;
        }

    }
}

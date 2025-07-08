using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoPOO.Models;

namespace ProjetoPOO.Repository.Interfaces
{
    public interface IRepositoryTransportadora
    {
        bool Adicionar(Transportadora obj);
        bool Remover(Transportadora obj);
        List<Transportadora> Listar();
        bool Alterar(Transportadora obj);
        void Salvar();
        void Carregar();
    }
}

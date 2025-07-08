using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoPOO.Models;

namespace ProjetoPOO.Repository.Interfaces
{
public interface IRepositoryPedido
{
    bool Adicionar(Pedido pedido);
    bool Alterar(Pedido pedido);
    bool Remover(Pedido pedido);
    List<Pedido> Listar();
    Pedido? ObterPorId(int id);
    void Salvar();
    void Carregar();
    List<Pedido> ObterPorCliente(int clienteId);
    List<Pedido> ObterPorData(DateTime inicio, DateTime fim);
}
}

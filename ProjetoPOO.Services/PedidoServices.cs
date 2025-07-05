using System;
using System.Collections.Generic;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Services
{
    public class PedidoService
    {
        private readonly IRepositoryPedido _repo;

        public PedidoService(IRepositoryPedido repo)
        {
            _repo = repo;
            _repo.Carregar();
        }

        public bool RealizarPedido(Pedido pedido)
        {
            pedido.Data = DateTime.Now;
            pedido.Status = "Novo";
            return _repo.Adicionar(pedido);
        }

        public List<Pedido> ListarPedidos()
        {
            return _repo.Listar();
        }

        public Pedido? ObterPorId(int id)
        {
            return _repo.ObterPorId(id);
        }

        public List<Pedido> ObterPorCliente(int clienteId)
        {
            return _repo.ObterPorCliente(clienteId);
        }

        public List<Pedido> ObterPorData(DateTime inicio, DateTime fim)
        {
            return _repo.ObterPorData(inicio, fim);
        }

        public bool AlterarStatus(int pedidoId, string novoStatus, DateTime? dataEnvio = null, DateTime? dataCancelamento = null)
        {
            var pedido = _repo.ObterPorId(pedidoId);
            if (pedido == null) return false;
            pedido.Status = novoStatus;
            if (dataEnvio != null) pedido.DataEnvio = dataEnvio;
            if (dataCancelamento != null) pedido.DataCancelamento = dataCancelamento;
            return _repo.Alterar(pedido);
        }

        
    }
}

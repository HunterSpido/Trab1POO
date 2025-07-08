using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Repository.PedidoRepository
{
    public class PedidoRepositoryList : IRepositoryPedido
    {
        private List<Pedido> _pedidos = new();
        private int _proximoId = 0;
        private const string FileName = "pedidos_lista.json";

        public bool Adicionar(Pedido pedido)
        {
            pedido.Id = _proximoId++;
            _pedidos.Add(pedido);
            Salvar();
            return true;
        }

        public bool Alterar(Pedido pedido)
        {
            int idx = _pedidos.FindIndex(p => p.Id == pedido.Id);
            if (idx < 0) return false;
            _pedidos[idx] = pedido;
            Salvar();
            return true;
        }

        public bool Remover(Pedido pedido)
        {
            int removidos = _pedidos.RemoveAll(p => p.Id == pedido.Id);
            if (removidos > 0) Salvar();
            return removidos > 0;
        }

        public List<Pedido> Listar()
        {
            return new List<Pedido>(_pedidos);
        }

        public Pedido? ObterPorId(int id)
        {
            return _pedidos.Find(p => p.Id == id);
        }

        public void Salvar()
        {
            var json = JsonSerializer.Serialize(_pedidos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FileName, json);
        }

        public void Carregar()
        {
            if (File.Exists(FileName))
            {
                var json = File.ReadAllText(FileName);
                var lista = JsonSerializer.Deserialize<List<Pedido>>(json);
                if (lista != null)
                {
                    _pedidos = lista;
                    _proximoId = _pedidos.Count > 0 ? _pedidos[^1].Id + 1 : 0;
                }
            }
        }

        public List<Pedido> ObterPorCliente(int clienteId)
        {
            return _pedidos.FindAll(p => p.Cliente.Id == clienteId);
        }

        public List<Pedido> ObterPorData(DateTime inicio, DateTime fim)
        {
            return _pedidos.FindAll(p => p.Data >= inicio && p.Data <= fim);
        }
    }
}

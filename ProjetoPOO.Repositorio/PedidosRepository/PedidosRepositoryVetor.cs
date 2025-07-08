using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Repository.PedidoRepository
{
    public class PedidoRepositoryVetor : IRepositoryPedido
    {
        private Pedido[] _pedidos = new Pedido[100];
        private int _qtdPedidos = 0;
        private int _proximoId = 0;
        private const string FileName = "pedidos_vetor.json";

        public bool Adicionar(Pedido pedido)
        {
            if (_qtdPedidos >= _pedidos.Length) return false;
            pedido.Id = _proximoId++;
            _pedidos[_qtdPedidos++] = pedido;
            Salvar();
            return true;
        }

        public bool Alterar(Pedido pedido)
        {
            for (int i = 0; i < _qtdPedidos; i++)
            {
                if (_pedidos[i].Id == pedido.Id)
                {
                    _pedidos[i] = pedido;
                    Salvar();
                    return true;
                }
            }
            return false;
        }

        public bool Remover(Pedido pedido)
        {
            for (int i = 0; i < _qtdPedidos; i++)
            {
                if (_pedidos[i].Id == pedido.Id)
                {
                    for (int j = i; j < _qtdPedidos - 1; j++)
                        _pedidos[j] = _pedidos[j + 1];
                    _pedidos[_qtdPedidos - 1] = null!;
                    _qtdPedidos--;
                    Salvar();
                    return true;
                }
            }
            return false;
        }

        public List<Pedido> Listar()
        {
            var lista = new List<Pedido>();
            for (int i = 0; i < _qtdPedidos; i++)
                lista.Add(_pedidos[i]);
            return lista;
        }

        public Pedido? ObterPorId(int id)
        {
            for (int i = 0; i < _qtdPedidos; i++)
                if (_pedidos[i].Id == id)
                    return _pedidos[i];
            return null;
        }

        public void Salvar()
        {
            var lista = Listar();
            var json = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true });
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
                    _qtdPedidos = Math.Min(lista.Count, _pedidos.Length);
                    for (int i = 0; i < _qtdPedidos; i++)
                        _pedidos[i] = lista[i];
                    _proximoId = _qtdPedidos > 0 ? _pedidos[_qtdPedidos - 1].Id + 1 : 0;
                }
            }
        }

        public List<Pedido> ObterPorCliente(int clienteId)
        {
            var lista = new List<Pedido>();
            for (int i = 0; i < _qtdPedidos; i++)
                if (_pedidos[i].Cliente.Id == clienteId)
                    lista.Add(_pedidos[i]);
            return lista;
        }

        public List<Pedido> ObterPorData(DateTime inicio, DateTime fim)
        {
            var lista = new List<Pedido>();
            for (int i = 0; i < _qtdPedidos; i++)
                if (_pedidos[i].Data >= inicio && _pedidos[i].Data <= fim)
                    lista.Add(_pedidos[i]);
            return lista;
        }
    }
}

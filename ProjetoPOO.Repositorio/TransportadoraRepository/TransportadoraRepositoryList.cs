using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Repository.TransportadoraRepository
{
    public class TransportadoraRepositoryList : IRepositoryTransportadora
    {
        private readonly List<Transportadora> transportadoras = new();
        private int proximoId = 0; // <-- CONTADOR DE ID

        public bool Adicionar(Transportadora transportadora)
        {
            // Atribui ID único ao adicionar
            transportadora.IdTransportadora = proximoId++;
            transportadoras.Add(transportadora);
            return true;
        }

        public bool Alterar(Transportadora transportadora)
        {
            var idx = transportadoras.FindIndex(t => t.IdTransportadora == transportadora.IdTransportadora);
            if (idx >= 0)
            {
                transportadoras[idx] = transportadora;
                return true;
            }
            return false;
        }

        public bool Remover(Transportadora transportadora)
        {
            int removidos = transportadoras.RemoveAll(t => t.IdTransportadora == transportadora.IdTransportadora);
            return removidos > 0;
        }

        public List<Transportadora> Listar()
        {
            return new List<Transportadora>(transportadoras);
        }

        public void Salvar()
        {
            var json = JsonSerializer.Serialize(transportadoras, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("transportadoras_lista.json", json);
        }

        public void Carregar()
        {
            if (File.Exists("transportadoras_lista.json"))
            {
                var json = File.ReadAllText("transportadoras_lista.json");
                var lista = JsonSerializer.Deserialize<List<Transportadora>>(json);
                if (lista != null)
                {
                    transportadoras.Clear();
                    transportadoras.AddRange(lista);

                    // Atualiza o contador de IDs para evitar duplicidade!
                    if (transportadoras.Count > 0)
                        proximoId = transportadoras[transportadoras.Count - 1].IdTransportadora + 1;
                    else
                        proximoId = 0;
                }
            }
        }

        public Transportadora? ConsultarPorId(int id)
        {
            return transportadoras.Find(t => t.IdTransportadora == id);
        }

        public List<Transportadora> ConsultarPorNome(string nome)
        {
            return transportadoras.FindAll(t => !string.IsNullOrEmpty(t.Nome) && t.Nome.ToLower().Contains(nome.ToLower()));
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Repository.TransportadoraRepository
{
    public class TransportadoraRepositoryVetor : IRepositoryTransportadora
    {
        private Transportadora[] transportadoras = new Transportadora[100];
        private int totalTransportadoras = 0;
        private int idTransportadoras = 0; // Para auto incrementar o ID

        public bool Adicionar(Transportadora transportadora)
        {
            if (totalTransportadoras >= transportadoras.Length)
                return false; // Limite atingido

            transportadora.IdTransportadora = idTransportadoras++; // Atribui ID único
            transportadoras[totalTransportadoras++] = transportadora;
            return true;
        }

        public bool Alterar(Transportadora transportadora)
        {
            for (int i = 0; i < totalTransportadoras; i++)
            {
                if (transportadoras[i].IdTransportadora == transportadora.IdTransportadora)
                {
                    transportadoras[i] = transportadora;
                    return true;
                }
            }
            return false; // Não encontrado
        }

        public bool Remover(Transportadora transportadora)
        {
            for (int i = 0; i < totalTransportadoras; i++)
            {
                if (transportadoras[i].IdTransportadora == transportadora.IdTransportadora)
                {
                    for (int j = i; j < totalTransportadoras - 1; j++)
                        transportadoras[j] = transportadoras[j + 1];
                    transportadoras[--totalTransportadoras] = null!;
                    return true;
                }
            }
            return false; // Não encontrado
        }

        public List<Transportadora> Listar()
        {
            var lista = new List<Transportadora>();
            for (int i = 0; i < totalTransportadoras; i++)
                lista.Add(transportadoras[i]);
            return lista;
        }

        public void Salvar()
        {
            var lista = Listar();
            var json = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("transportadoras_vetor.json", json);
        }

        public void Carregar()
        {
            if (File.Exists("transportadoras_vetor.json"))
            {
                var json = File.ReadAllText("transportadoras_vetor.json");
                var lista = JsonSerializer.Deserialize<List<Transportadora>>(json);
                if (lista != null)
                {
                    totalTransportadoras = Math.Min(lista.Count, transportadoras.Length);
                    for (int i = 0; i < totalTransportadoras; i++)
                        transportadoras[i] = lista[i];
                    idTransportadoras = totalTransportadoras > 0
                        ? transportadoras[totalTransportadoras - 1].IdTransportadora + 1
                        : 0;
                }
            }
        }

        public Transportadora? ConsultarPorId(int id)
        {
            for (int i = 0; i < totalTransportadoras; i++)
                if (transportadoras[i].IdTransportadora == id)
                    return transportadoras[i];
            return null;
        }

        public List<Transportadora> ConsultarPorNome(string nome)
        {
            var lista = new List<Transportadora>();
            for (int i = 0; i < totalTransportadoras; i++)
            {
                if (!string.IsNullOrEmpty(transportadoras[i].Nome) &&
                    transportadoras[i].Nome.ToLower().Contains(nome.ToLower()))
                    lista.Add(transportadoras[i]);
            }
            return lista;
        }
    }
}

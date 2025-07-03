using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Repository.ClienteRepository
{
    public class ClienteRepositoryList : IRepository<Cliente>
    {
        private List<Cliente> clientes=new List<Cliente>();
        public void Adicionar(Cliente obj)
        {
            clientes.Add(obj);
        }

        public void Alterar(Cliente obj)
        {
            throw new NotImplementedException();
        }

        public void Carregar()
        {
            if (File.Exists("clientes.json"))
            {
                var json = File.ReadAllText("clientes.json");
                var lista = System.Text.Json.JsonSerializer.Deserialize<List<Cliente>>(json);
                if (lista != null)
                    clientes = lista;
            }
        }

        public List<Cliente> Listar()
        {
            return clientes;
        }



        public void Remover(Cliente obj)
        {
            clientes.Remove(obj);
        }

        public void Salvar()
        {
            var json = System.Text.Json.JsonSerializer.Serialize(clientes, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("clientes.json", json);
        }

       
    }



}

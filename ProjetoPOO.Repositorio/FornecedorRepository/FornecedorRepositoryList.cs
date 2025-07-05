using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Repository.FornecedorRepository
{
    public class FornecedorRepositoryList : IRepositoryFornecedor
    {
        private readonly List<Fornecedor> fornecedores;
        private int idFornecedores = 0;

        public FornecedorRepositoryList()
        {
            fornecedores = new();
        }

        public bool Adicionar(Fornecedor obj)
        {
            obj.IdFornecedor = idFornecedores++;
            fornecedores.Add(obj);
            return true;
        }

        public bool Alterar(Fornecedor obj)
        {
            for (int i = 0; i < fornecedores.Count; i++)
            {
                if (fornecedores[i].IdFornecedor == obj.IdFornecedor)
                {
                    fornecedores[i] = obj;
                    return true;
                }
            }
            return false; // Não encontrou para alterar
        }

        public bool Remover(Fornecedor obj)
        {
            int removedCount = fornecedores.RemoveAll(f => f.IdFornecedor == obj.IdFornecedor);
            return removedCount > 0;
        }

        public List<Fornecedor> Listar()
        {
            return fornecedores;
        }

        public void Salvar()
        {
            var json = JsonSerializer.Serialize(fornecedores, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("fornecedores_lista.json", json);
        }

        public void Carregar()
        {
            if (File.Exists("fornecedores_lista.json"))
            {
                var json = File.ReadAllText("fornecedores_lista.json");
                var lista = JsonSerializer.Deserialize<List<Fornecedor>>(json);
                if (lista != null)
                {
                    fornecedores.Clear();
                    fornecedores.AddRange(lista);
                    if (fornecedores.Count > 0)
                        idFornecedores = fornecedores[fornecedores.Count - 1].IdFornecedor + 1;
                    else
                        idFornecedores = 0;
                }
            }
        }
    }
}

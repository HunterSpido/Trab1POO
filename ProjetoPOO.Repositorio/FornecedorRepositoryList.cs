using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ProjetoPOO.Models;

namespace ProjetoPOO.Repository
{
    public class FornecedorRepositoryList : IRepository<Fornecedor>
    {
        private readonly List<Fornecedor> fornecedores;
        private int idFornecedores = 0;

        public FornecedorRepositoryList()
        {
            fornecedores = new();
        }

        public void Adicionar(Fornecedor obj)
        {
            obj.IdFornecedor = idFornecedores++;
            fornecedores.Add(obj);
        }

        public void Alterar(Fornecedor obj)
        {
            for (int i = 0; i < fornecedores.Count; i++)
            {
                if (fornecedores[i].IdFornecedor == obj.IdFornecedor)
                {
                    fornecedores[i] = obj;
                    return;
                }
            }
            throw new KeyNotFoundException("Fornecedor não encontrado para alteração.");
        }

        public void Remover(Fornecedor obj)
        {
            fornecedores.RemoveAll(f => f.IdFornecedor == obj.IdFornecedor);
        }

        public IList<Fornecedor> Listar()
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
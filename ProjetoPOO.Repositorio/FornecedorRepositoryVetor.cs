using System.IO;
using System.Text.Json;
using ProjetoPOO.Models;
using System.Collections.Generic;

namespace ProjetoPOO.Repository
{
    public class FornecedorRepositoryVetor : IRepository<Fornecedor>
    {
        private Fornecedor[] fornecedores;
        private int qtdFornecedores;
        private int idFornecedores;

        public FornecedorRepositoryVetor()
        {
            fornecedores = new Fornecedor[100];
            qtdFornecedores = 0;
            idFornecedores = 0;
        }

        public void Adicionar(Fornecedor obj)
        {
            if (qtdFornecedores >= fornecedores.Length)
                throw new InvalidOperationException("Limite de fornecedores atingido.");

            obj.IdFornecedor = idFornecedores++;
            fornecedores[qtdFornecedores] = obj;
            qtdFornecedores++;
        }

        public void Alterar(Fornecedor obj)
        {
            for (int i = 0; i < qtdFornecedores; i++)
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
            for (int i = 0; i < qtdFornecedores; i++)
            {
                if (fornecedores[i].IdFornecedor == obj.IdFornecedor)
                {
                    for (int j = i; j < qtdFornecedores - 1; j++)
                        fornecedores[j] = fornecedores[j + 1];
                    fornecedores[qtdFornecedores - 1] = null!;
                    qtdFornecedores--;
                    return;
                }
            }
            throw new KeyNotFoundException("Fornecedor não encontrado para remoção.");
        }

        public IList<Fornecedor> Listar()
        {
            var lista = new List<Fornecedor>();
            for (int i = 0; i < qtdFornecedores; i++)
                lista.Add(fornecedores[i]);
            return lista;
        }

        public void Salvar()
        {
            var lista = Listar();
            var json = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("fornecedores_vetor.json", json);
        }
        public int GetQuantidade()
        {
            return qtdFornecedores;
        }

        public void Carregar()
        {
            if (File.Exists("fornecedores_vetor.json"))
            {
                var json = File.ReadAllText("fornecedores_vetor.json");
                var lista = JsonSerializer.Deserialize<List<Fornecedor>>(json);
                if (lista != null)
                {
                    qtdFornecedores = Math.Min(lista.Count, fornecedores.Length);
                    for (int i = 0; i < qtdFornecedores; i++)
                        fornecedores[i] = lista[i];
                    idFornecedores = qtdFornecedores > 0 ? fornecedores[qtdFornecedores - 1].IdFornecedor + 1 : 0;
                }
            }
        }
    }
}
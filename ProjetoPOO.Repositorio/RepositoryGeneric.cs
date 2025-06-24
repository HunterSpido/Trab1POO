using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ProjetoPOO.Models;

namespace ProjetoPOO.Repository
{
    public class RepositoryGeneric<T> : IRepository<T> where T : class,IIdentificavel
    {
        private List<T> lista;
        public RepositoryGeneric()
        {
            lista = new List<T>();
        }
        

        public void Adicionar(T obj)
        {
            lista.Add(obj);
        }

        public void Remover(T obj)
        {
            lista.Remove(obj);
        }

        public IList<T> Listar()
        {
            return lista;
        }

        public void Alterar(T obj)
        {
            // Busca a propriedade "Id" do objeto
            var prop = typeof(T).GetProperty("Id");
            if (prop == null)
                throw new InvalidOperationException("O tipo não possui propriedade 'Id'.");

            var id = prop.GetValue(obj);

            // Procura o índice do objeto com o mesmo Id na lista
            int index = lista.FindIndex(x => prop.GetValue(x)?.Equals(id) == true);

            if (index >= 0)
            {
                lista[index] = obj;
            }
            else
            {
                throw new InvalidOperationException("Objeto para alteração não encontrado.");
            }
        }
        public T ConsultarPorId(int id)
        {
            return lista.FirstOrDefault(x => ((IIdentificavel)x).Id == id);
        }
        public List<T> ConsultarPorNome(string nome)
        {
            return lista.Where(x => x.Nome != null && x.Nome.Contains(nome)).ToList();
        }


    }

    
}

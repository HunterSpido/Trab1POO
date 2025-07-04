using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Repository
{
    public class RepositorioBase<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public RepositorioBase(IRepository<T> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Adicionar(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            _repository.Adicionar(obj);
        }

        public void Remover(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            _repository.Remover(obj);
        }

        public List<T> Listar()
        {
            return _repository.Listar();
        }

        public void Alterar(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            _repository.Alterar(obj);
        }

        public void Salvar()
        {
            _repository.Salvar();
        }

        public void Carregar()
        {
            _repository.Carregar();
        }
    }
}
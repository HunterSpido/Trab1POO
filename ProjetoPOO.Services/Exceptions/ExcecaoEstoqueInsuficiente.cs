using System;

namespace ProjetoPOO.Services.Exceptions
{

    public class ExcecaoEstoqueInsuficiente : Exception
    {
        public ExcecaoEstoqueInsuficiente(int disponivel)
            : base($"Estoque insuficiente. Disponível: {disponivel} unidades.")
        {
        }
    }
}

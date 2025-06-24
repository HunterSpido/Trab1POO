using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPOO.Models
{
    public interface IIdentificavel
    {
        int Id { get; }
        string? Nome { get; }
    }
}

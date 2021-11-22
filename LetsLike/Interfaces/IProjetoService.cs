using LetsLike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsLike.Interfaces
{
    public interface IProjetoService
    {
        Projeto SaveOrUpdate(Projeto projeto);

        Projeto LikeProketo(UsuarioLikeProjeto like);
    }
}

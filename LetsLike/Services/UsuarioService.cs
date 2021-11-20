using LetsLike.Data;
using LetsLike.Interfaces;
using LetsLike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsLike.Services
{
    public class UsuarioService : IUsuarioService
    {
        public LetsLikeContext _contexto;
        public UsuarioService(LetsLikeContext contexto)
        {
            _contexto = contexto;
        }

        public IList<Usuario> FindAllUsuarios()
        {
            return _contexto.Usuarios.ToList();
        }

        public Usuario SaveOrUpdate(Usuario usuario)
        {
            // TODO se eu estou salvando e atualizando no mesmo método 
            // a primeira coisa que preciso verificar é se o usuário do parâemtro existe

            var existe = _contexto.Usuarios.Where(x => x.Id.Equals(usuario.Id)).FirstOrDefault();

            if(existe == null)
            {
                _contexto.Usuarios.Add(usuario);
            }
            else
            {
                existe.Nome = usuario.Nome;
                existe.Username = usuario.Username;
                existe.Senha = usuario.Senha;
                existe.Email = usuario.Email;
            }
            _contexto.SaveChanges();

            return usuario;
        }
    }
}

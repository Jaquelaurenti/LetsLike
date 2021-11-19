using LetsLike.Models;

namespace LetsLike.Interfaces
{
    public interface IUsuarioService
    {
        Usuario SaveOrUpdate(Usuario usuario);
    }
}

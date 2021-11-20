using LetsLike.Models;
using System.Collections.Generic;

namespace LetsLike.Interfaces
{
    public interface IUsuarioService
    {
        Usuario SaveOrUpdate(Usuario usuario);

        // TODO a titulo de boas práticas não é bacana colocar o nome da Entidade no método
        // o mais correto neste caso seria chamá-lo de FindAll
        IList<Usuario> FindAllUsuarios();
    }
}

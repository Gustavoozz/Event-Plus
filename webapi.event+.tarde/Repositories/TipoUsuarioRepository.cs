using webapi.event_.tarde.Contexts;
using webapi.event_.tarde.Domains;
using webapi.event_.tarde.Interfaces;

namespace webapi.event_.tarde.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private readonly EventContext _eventContext;

        public TipoUsuarioRepository()
        {
            _eventContext = new EventContext();
        }


        public void Atualizar(Guid id, TipoUsuario tipoUsuario)
        {
            TipoUsuario tipoUsuarioBuscado = _eventContext.TipoUsuario.Find(id)!;

            if (tipoUsuarioBuscado != null)
            {
                tipoUsuarioBuscado.Titulo = tipoUsuario.Titulo;
            }

            _eventContext.TipoUsuario.Update(tipoUsuarioBuscado!);

            _eventContext.SaveChanges();
        }

        public TipoUsuario BuscarPorId(Guid id)
        {
            return _eventContext.TipoUsuario.FirstOrDefault(a => a.IdTipoUsuario == id)!;
        }

        public void Cadastrar(TipoUsuario tipoUsuario)
        {
            try
            {
                _eventContext.TipoUsuario.Add(tipoUsuario);

                _eventContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Deletar(Guid id)
        {
            TipoUsuario tipoUsuarioBuscado = _eventContext.TipoUsuario.Find(id)!;

            if (tipoUsuarioBuscado != null)
            {
                _eventContext.TipoUsuario.Remove(tipoUsuarioBuscado);
            }

            _eventContext.SaveChanges();
        }

        public List<TipoUsuario> Listar()
        {
            return _eventContext.TipoUsuario.ToList();
        }
    }
}

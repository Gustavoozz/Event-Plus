using webapi.event_.tarde.Contexts;
using webapi.event_.tarde.Domains;
using webapi.event_.tarde.Interfaces;

namespace webapi.event_.tarde.Repositories
{
    public class TipoEventoRepository : ITipoEventoRepository
    {
        private readonly EventContext _eventContext;

        public TipoEventoRepository()
        {
            _eventContext = new EventContext();
        }
        public void Atualizar(Guid id, TipoEvento tipoEvento)
        {
            TipoEvento tipoEventoBuscado = _eventContext.TipoEvento.Find(id)!;

            if (tipoEventoBuscado != null)
            {
                tipoEventoBuscado.Titulo = tipoEvento.Titulo;
            }

            _eventContext.TipoEvento.Update(tipoEventoBuscado!);

            _eventContext.SaveChanges();
        }

        public TipoEvento BuscarPorId(Guid id)
        {
            return _eventContext.TipoEvento.FirstOrDefault(a => a.IdTipoEvento == id)!;
        }

        public void Cadastrar(TipoEvento tipoEvento)
        {
            try
            {
                _eventContext.TipoEvento.Add(tipoEvento);

                _eventContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Deletar(Guid id)
        {
            TipoEvento tipoEventoBuscado = _eventContext.TipoEvento.Find(id)!;

            if (tipoEventoBuscado != null)
            {
                _eventContext.TipoEvento.Remove(tipoEventoBuscado);
            }

            _eventContext.SaveChanges();
        }

        public List<TipoEvento> Listar()
        {
            return _eventContext.TipoEvento.ToList();
        }
    }
}

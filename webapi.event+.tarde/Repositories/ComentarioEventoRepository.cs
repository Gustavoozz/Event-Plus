using webapi.event_.tarde.Contexts;
using webapi.event_.tarde.Domains;
using webapi.event_.tarde.Interfaces;

namespace webapi.event_.tarde.Repositories
{
    public class ComentarioEventoRepository : IComentarioEventoRepository
    {
        private readonly EventContext _eventContext;

        public ComentarioEventoRepository()
        {
            _eventContext = new EventContext();
        }

        public ComentarioEvento BuscarPorId(Guid id)
        {
            return _eventContext.ComentarioEvento.FirstOrDefault(a => a.IdComentarioEvento == id)!;
        }

        public void Cadastrar(ComentarioEvento comentarioEvento)
        {
            _eventContext.ComentarioEvento.Add(comentarioEvento);

            _eventContext.SaveChanges();
        }

        public void Deletar(Guid id)
        {
            ComentarioEvento comentarioEvento = _eventContext.ComentarioEvento.Find(id)!;

            if (comentarioEvento != null)
            {
                _eventContext.ComentarioEvento.Remove(comentarioEvento);
            }

            _eventContext.SaveChanges();
        }

        public List<ComentarioEvento> Listar()
        {
            return _eventContext.ComentarioEvento.ToList();
        }
    }
}

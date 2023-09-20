using webapi.event_.tarde.Contexts;
using webapi.event_.tarde.Domains;
using webapi.event_.tarde.Interfaces;

namespace webapi.event_.tarde.Repositories
{
    public class PresencaEventoRepository : IPresencaEventoRepository
    {
        private readonly EventContext _eventContext;

        public PresencaEventoRepository()
        {
            _eventContext = new EventContext();
        }
        public void Atualizar(Guid id, PresencaEvento presencaEvento)
        {
            throw new NotImplementedException();
        }

        public PresencaEvento BuscarPorId(Guid id)
        {
            return _eventContext.PresencaEvento.FirstOrDefault(a => a.IdPresencaEvento == id)!;
        }

        public void Deletar(Guid id)
        {
            PresencaEvento presencasEvento = _eventContext.PresencaEvento.Find(id)!;

            if (presencasEvento != null)
            {
                _eventContext.PresencaEvento.Remove(presencasEvento);
            }

            _eventContext.SaveChanges();
        }

        public void Inscrever(PresencaEvento inscricao)
        {
            _eventContext.PresencaEvento.Add(inscricao);

            _eventContext.SaveChanges();
        }

        public List<PresencaEvento> Listar()
        {
            return _eventContext.PresencaEvento.ToList();
        }

        public List<PresencaEvento> ListarMinhas(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

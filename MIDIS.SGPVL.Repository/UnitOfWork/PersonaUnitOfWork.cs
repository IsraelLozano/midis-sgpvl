using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Repository.Persona;
using PROMART.SISTRAN.Repository.Base;

namespace MIDIS.SGPVL.Repository.UnitOfWork
{
    public class PersonaUnitOfWork : IUnitOfWork<BDPVLContext>
    {
        private bool disposedValue;

        private readonly BDPVLContext _context;
        public IPersonaRepository _personaRepository { get; }
        public IPersonaNaturalRepository _personaNaturalRepository { get; }


        public PersonaUnitOfWork(BDPVLContext context, 
            IPersonaRepository personaRepository, 
            IPersonaNaturalRepository personaNaturalRepository)
        {
            _context = context;
            _personaRepository = personaRepository;
            _personaNaturalRepository = personaNaturalRepository;
        }

        public void Save()
        {
            this._context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await this._context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    this._context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~PersonaUnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

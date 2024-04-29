using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Repository.ComitePvl;
using PROMART.SISTRAN.Repository.Base;

namespace MIDIS.SGPVL.Repository.UnitOfWork
{
    public class ComiteUnitOfWork : IUnitOfWork<BDPVLContext>
    {
        private bool disposedValue;
        private readonly BDPVLContext _context;
        public IComitePVLRepository  _comitePVLRepository { get; }
        public IJunDirectivaRepository  _junDirectivaRepository{ get; }
        public IMiembroJuntaRepository _miembroJuntaRepository{ get; }
        public ISocioReposiroty _socioReposiroty{ get; }
        public IUsuarioRepository _usuarioRepository { get; }

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
        // ~ComiteUnitOfWork()
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

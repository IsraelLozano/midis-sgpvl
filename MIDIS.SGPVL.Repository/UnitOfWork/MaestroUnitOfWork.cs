using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Repository.Maestro;
using PROMART.SISTRAN.Repository.Base;

namespace MIDIS.SGPVL.Repository.UnitOfWork
{
    public class MaestroUnitOfWork : IUnitOfWork<BDPVLContext>
    {
        private bool disposedValue;
        private readonly BDPVLContext _context;
        public IEnumeradoRepository _enumeradoRepository { get; }
        public IEnumeradoItemRepository _enumeradoItemRepository { get; }
        public ICentroPobladoRepository _centroPobladoRepository { get; }
        public IDepartamentoRepository _departamentoRepository { get; }
        public IProvinciaRepository _provinciaRepository { get; }
        public IUbigeoRepository _ubigeoRepository { get; }
        public IDistritoRepository  _distritoRepository { get; }

        public MaestroUnitOfWork(BDPVLContext context,
            IEnumeradoRepository enumeradoRepository,
            IEnumeradoItemRepository enumeradoItemRepository,
            ICentroPobladoRepository centroPobladoRepository,
            IDepartamentoRepository departamentoRepository,
            IProvinciaRepository provinciaRepository,
            IUbigeoRepository ubigeoRepository,
            IDistritoRepository distritoRepository)
        {
            _context = context;
            _enumeradoRepository = enumeradoRepository;
            _enumeradoItemRepository = enumeradoItemRepository;
            _centroPobladoRepository = centroPobladoRepository;
            _departamentoRepository = departamentoRepository;
            _provinciaRepository = provinciaRepository;
            _ubigeoRepository = ubigeoRepository;
            _distritoRepository = distritoRepository;
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
        // ~MaestroUnitOfWork()
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

﻿using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Entity.Models.ComitePvl;
using PROMART.SISTRAN.Repository.Base;

namespace MIDIS.SGPVL.Repository.ComitePvl
{
    public class MiembroJuntaRepository : GenericRepository<VLMiembroJuntum>, IMiembroJuntaRepository
    {
        public MiembroJuntaRepository(BDPVLContext context) : base(context)
        {
        }
    }
}

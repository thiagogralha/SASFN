using SASF.Domain.Entities;
using SASF.Domain.Interfaces.Repositories;
using SASF.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace SASF.Domain.Services
{
    public class FichaService : ServiceBase<Ficha>, IFichaService
    {
        private readonly IFichaRepository _fichaRepository;

        public FichaService(IFichaRepository fichaRepository)
            : base(fichaRepository)
        {
            _fichaRepository = fichaRepository;
        }
        
        public List<int> BuscarIdFichas(int planoId)
        {
            return _fichaRepository.BuscarIdFichas(planoId);
        }

        public List<Ficha> GetAllFichas()
        {
            return _fichaRepository.GetAllFichas();
        }

    }
}

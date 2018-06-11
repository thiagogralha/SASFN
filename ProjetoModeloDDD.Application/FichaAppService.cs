using SASF.Application.Interface;
using SASF.Domain.Entities;
using SASF.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace SASF.Application
{
    public class FichaAppService : AppServiceBase<Ficha>, IFichaAppService
    {
        private readonly IFichaService _fichaService;

        public FichaAppService(IFichaService fichaService)
            : base(fichaService)
        {
            _fichaService = fichaService;
        }

        public List<int> BuscarIdFichas(int planoId)
        {
            return _fichaService.BuscarIdFichas(planoId);
        }

        public List<Ficha> GetAllFichas()
        {
            return _fichaService.GetAllFichas();
        }

    }
}

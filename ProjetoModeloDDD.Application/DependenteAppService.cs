using SASF.Application.Interface;
using SASF.Domain.Entities;
using SASF.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace SASF.Application
{
    public class DependenteAppService : AppServiceBase<Dependente>, IDependenteAppService
    {
        private readonly IDependenteService _dependenteService;

        public DependenteAppService(IDependenteService dependenteService)
            : base(dependenteService)
        {
            _dependenteService = dependenteService;
        }

        public Dependente BuscarporId(int id)
        {
            return _dependenteService.BuscarporId(id);
        }

        public List<Dependente> BuscarDependentesPorFicha(int fichaId)
        {
            return _dependenteService.BuscarDependentesPorFicha(fichaId);
        }
    }
}

using SASF.Domain.Entities;
using SASF.Domain.Interfaces.Repositories;
using SASF.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace SASF.Domain.Services
{
    public class DependenteService : ServiceBase<Dependente>, IDependenteService
    {
        private readonly IDependenteRepository _dependenteRepository;

        public DependenteService(IDependenteRepository dependenteRepository)
            : base(dependenteRepository)
        {
            _dependenteRepository = dependenteRepository;
        }

        public Dependente BuscarporId(int id)
        {
            return _dependenteRepository.BuscarporId(id);
        }

        public List<Dependente> BuscarDependentesPorFicha(int fichaId)
        {
            return _dependenteRepository.BuscarDependentesPorFicha(fichaId);
        }
    }
}

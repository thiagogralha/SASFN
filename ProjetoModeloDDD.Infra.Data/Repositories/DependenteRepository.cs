using SASF.Domain.Entities;
using SASF.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SASF.Infra.Data.Repositories
{
    public class DependenteRepository : RepositoryBase<Dependente>, IDependenteRepository
    {
        public Dependente BuscarporId(int id)
        {
            return Db.Dependentes.Where(d => d.DependenteId == id).FirstOrDefault();
        }

        public List<Dependente> BuscarDependentesPorFicha(int fichaId)
        {
            return Db.Dependentes.Where(d => d.FichaId == fichaId).ToList();
        }
    }
}

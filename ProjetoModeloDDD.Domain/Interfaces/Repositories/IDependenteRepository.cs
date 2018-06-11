using SASF.Domain.Entities;
using System.Collections.Generic;

namespace SASF.Domain.Interfaces.Repositories
{
    public interface IDependenteRepository : IRepositoryBase<Dependente>
    {
        Dependente BuscarporId(int id);

        List<Dependente> BuscarDependentesPorFicha(int fichaId);
    }
}

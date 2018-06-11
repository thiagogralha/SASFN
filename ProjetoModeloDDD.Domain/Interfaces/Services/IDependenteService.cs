using SASF.Domain.Entities;
using System.Collections.Generic;

namespace SASF.Domain.Interfaces.Services
{
    public interface IDependenteService : IServiceBase<Dependente>
    {
        Dependente BuscarporId(int id);

        List<Dependente> BuscarDependentesPorFicha(int fichaId);
    }
}

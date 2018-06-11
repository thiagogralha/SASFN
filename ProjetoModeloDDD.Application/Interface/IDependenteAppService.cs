using SASF.Domain.Entities;
using System.Collections.Generic;

namespace SASF.Application.Interface
{
    public interface IDependenteAppService : IAppServiceBase<Dependente>
    {
        Dependente BuscarporId(int id);

        List<Dependente> BuscarDependentesPorFicha(int fichaId);
    }
}

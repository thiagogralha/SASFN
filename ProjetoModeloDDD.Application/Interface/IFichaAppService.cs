using SASF.Domain.Entities;
using System.Collections.Generic;

namespace SASF.Application.Interface
{
    public interface IFichaAppService : IAppServiceBase<Ficha>
    {
        List<int> BuscarIdFichas(int planoId);

        List<Ficha> GetAllFichas();
    }
}

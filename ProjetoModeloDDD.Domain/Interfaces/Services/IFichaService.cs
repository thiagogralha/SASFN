using SASF.Domain.Entities;
using System.Collections.Generic;

namespace SASF.Domain.Interfaces.Services
{
    public interface IFichaService : IServiceBase<Ficha>
    {
        List<int> BuscarIdFichas(int planoId);

        List<Ficha> GetAllFichas();
    }
}

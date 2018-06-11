using SASF.Domain.Entities;
using System.Collections.Generic;

namespace SASF.Domain.Interfaces.Repositories
{
    public interface IFichaRepository : IRepositoryBase<Ficha>
    {
        List<int> BuscarIdFichas(int planoId);

        List<Ficha> GetAllFichas();
    }
}

using SASF.Domain.Entities;
using SASF.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SASF.Infra.Data.Repositories
{
    public class FichaRepository : RepositoryBase<Ficha>, IFichaRepository
    {
        public List<int> BuscarIdFichas(int planoId)
        {
            return Db.Ficha.Where(d => d.PlanoId == planoId).Select(x => x.FichaId).ToList();
        }

        public List<Ficha> GetAllFichas()
        {
            return Db.Ficha.Include("Dependentes").Include("Plano").ToList();
        }
    }
}

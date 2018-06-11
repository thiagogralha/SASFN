using SASF.Domain.Entities;
using SASF.Domain.Interfaces.Repositories;
using SASF.Domain.Interfaces.Services;

namespace SASF.Domain.Services
{
    public class PlanoService : ServiceBase<Plano>, IPlanoService
    {
        private readonly IPlanoRepository _planoRepository;

        public PlanoService(IPlanoRepository planoRepository)
            : base(planoRepository)
        {
            _planoRepository = planoRepository;
        }
        
    }
}

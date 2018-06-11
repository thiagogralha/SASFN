using SASF.Application.Interface;
using SASF.Domain.Entities;
using SASF.Domain.Interfaces.Services;

namespace SASF.Application
{
    public class PlanoAppService : AppServiceBase<Plano>, IPlanoAppService
    {
        private readonly IPlanoService _planoService;

        public PlanoAppService(IPlanoService planoService)
            : base(planoService)
        {
            _planoService = planoService;
        }
        
    }
}

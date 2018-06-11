using SASF.Application.Interface;
using SASF.Domain.Entities;
using SASF.Domain.Interfaces.Services;

namespace SASF.Application
{
    public class FuncionarioAppService : AppServiceBase<Funcionario>, IFuncionarioAppService
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionarioAppService(IFuncionarioService funcionarioService)
            : base(funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        public Funcionario BuscarDetalhesFuncionario(string login, string senha)
        {
            return _funcionarioService.BuscarDetalhesFuncionario(login, senha);
        }
    }
}

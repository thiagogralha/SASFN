using SASF.Domain.Entities;
using SASF.Domain.Interfaces.Repositories;
using SASF.Domain.Interfaces.Services;

namespace SASF.Domain.Services
{
    public class FuncionarioService : ServiceBase<Funcionario>, IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository)
            : base(funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public Funcionario BuscarDetalhesFuncionario(string login, string senha)
        {
            return _funcionarioRepository.BuscarDetalhesFuncionario(login, senha);
        }
    }
}

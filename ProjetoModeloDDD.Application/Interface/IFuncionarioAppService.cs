using SASF.Domain.Entities;

namespace SASF.Application.Interface
{
    public interface IFuncionarioAppService : IAppServiceBase<Funcionario>
    {
        Funcionario BuscarDetalhesFuncionario(string login, string senha);
    }
}

using SASF.Domain.Entities;

namespace SASF.Domain.Interfaces.Services
{
    public interface IFuncionarioService : IServiceBase<Funcionario>
    {
        Funcionario BuscarDetalhesFuncionario(string login, string senha);
    }
}

using SASF.Domain.Entities;

namespace SASF.Domain.Interfaces.Repositories
{
    public interface IFuncionarioRepository : IRepositoryBase<Funcionario>
    {
        Funcionario BuscarDetalhesFuncionario(string login, string senha);
    }
}

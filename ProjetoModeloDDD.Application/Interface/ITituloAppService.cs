using SASF.Domain.Entities;
using System.Collections.Generic;

namespace SASF.Application.Interface
{
    public interface ITituloAppService : IAppServiceBase<Titulo>
    {
        Titulo BuscarTitulo(int id);
        
        bool AtualizarTodosTituloPendente(float valor, int fichaId, string planoNome);
        List<Titulo> BuscarTitulosPorFicha(int fichaId);
    }
}

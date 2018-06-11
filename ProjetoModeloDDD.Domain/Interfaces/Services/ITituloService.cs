using SASF.Domain.Entities;
using System.Collections.Generic;

namespace SASF.Domain.Interfaces.Services
{
    public interface ITituloService : IServiceBase<Titulo>
    {
        Titulo BuscarTitulo(int id);
        
        bool AtualizarTodosTituloPendente(float valor, int fichaId, string planoNome);
        List<Titulo> BuscarTitulosPorFicha(int fichaId);
    }
}

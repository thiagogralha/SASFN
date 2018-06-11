using SASF.Domain.Entities;
using SASF.Domain.Interfaces.Repositories;
using SASF.Infra.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SASF.Infra.Data.Repositories
{
    public class TituloRepository : RepositoryBase<Titulo>, ITituloRepository
    {
        public Titulo BuscarTitulo(int id)
        {
            return Db.Titulos.Include("Ficha").Where(d => d.TituloId == id).FirstOrDefault();
        }

        public bool AtualizarTodosTituloPendente(float valor, int fichaId, string planoNome)
        {
            try
            {
                using (var ctx = new ProjetoModeloContext())
                {
                    var query = "UPDATE Titulo SET Valor = '" + valor.ToString().Replace(",", ".") + "', PlanoNome = '" + planoNome + "'  WHERE STATUS = 'P' AND FichaId ='" + fichaId + "' AND DataBaixa IS NULL ";

                    ctx.Database.ExecuteSqlCommand(query);

                    return true;
                }
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Titulo> BuscarTitulosPorFicha(int fichaId)
        {
            return Db.Titulos.Where(d => d.FichaId == fichaId).ToList();
        }
    }
}


using SASF.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SASF.Infra.Data.EntityConfig
{
    public class PlanoConfiguration : EntityTypeConfiguration<Plano>
    {
        public PlanoConfiguration()
        {
            HasKey(p => p.PlanoId);

            Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(100);

            Property(c => c.Ativo)
           .HasMaxLength(1);

            Property(c => c.Valor)
           .IsRequired();

        }
    }
}

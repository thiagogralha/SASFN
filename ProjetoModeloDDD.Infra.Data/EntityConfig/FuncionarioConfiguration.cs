
using SASF.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SASF.Infra.Data.EntityConfig
{
    public class FuncionarioConfiguration : EntityTypeConfiguration<Funcionario>
    {
        public FuncionarioConfiguration()
        {
            HasKey(c => c.FuncionarioId);            

            Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);

            Property(c => c.Email)
               .IsRequired()
               .HasMaxLength(150);

            Property(c => c.Login)
               .IsRequired()
               .HasMaxLength(20);

            Property(c => c.Senha)
              .IsRequired()
              .HasMaxLength(8);

            Property(c => c.Token)
             .HasMaxLength(5);

            Property(c => c.IsAdmin)
          .HasMaxLength(1);
        }
    }
}

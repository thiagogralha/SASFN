using SASF.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SASF.Infra.Data.EntityConfig
{
    public class FichaConfiguration : EntityTypeConfiguration<Ficha>
    {
        public FichaConfiguration()
        {
            HasKey(p => p.FichaId);

            Property(c => c.NomeCompleto)
               .IsRequired()
               .HasMaxLength(200);

            Property(c => c.CPF)
               .IsRequired()
               .HasMaxLength(15);

            Property(c => c.Endereco)
               .IsRequired()
               .HasMaxLength(150);

            Property(c => c.Cep)
               .IsRequired()
               .HasMaxLength(20);

            Property(c => c.Bairro)
              .IsRequired()
              .HasMaxLength(50);

            Property(c => c.Complemento)
              .HasMaxLength(100);

            Property(c => c.Estado)
              .IsRequired()
              .HasMaxLength(50);

            Property(c => c.Cidade)
              .IsRequired()
              .HasMaxLength(50);

            Property(c => c.Telefone)
              .IsRequired()
              .HasMaxLength(20);

            Property(c => c.TelefoneOpcional)
              .HasMaxLength(20);

            Property(c => c.Email)
              .HasMaxLength(50);

            Property(c => c.NumeroIdentidade)
             .HasMaxLength(20);            

            Property(c => c.DataNascimento)
              .IsRequired();

            HasRequired(p => p.Plano)
                .WithMany()
                .HasForeignKey(p => p.PlanoId);
        }
    }
}

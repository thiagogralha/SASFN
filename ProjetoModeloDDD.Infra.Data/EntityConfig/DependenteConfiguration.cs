using SASF.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SASF.Infra.Data.EntityConfig
{
    public class DependenteConfiguration : EntityTypeConfiguration<Dependente>
    {
        public DependenteConfiguration()
        {
            HasKey(p => p.DependenteId);

            Property(c => c.NomeCompletoDep)
               .IsRequired()
               .HasMaxLength(200);

            Property(c => c.CPFDep)
               .IsRequired()
               .HasMaxLength(15);

            Property(c => c.EnderecoDep)
               .IsRequired()
               .HasMaxLength(150);

            Property(c => c.CepDep)
               .IsRequired()
               .HasMaxLength(20);

            Property(c => c.BairroDep)
              .IsRequired()
              .HasMaxLength(50);

            Property(c => c.ComplementoDep)
              .HasMaxLength(100);

            Property(c => c.EstadoDep)
              .IsRequired()
              .HasMaxLength(50);

            Property(c => c.CidadeDep)
              .IsRequired()
              .HasMaxLength(50);

            Property(c => c.TelefoneDep)
              .IsRequired()
              .HasMaxLength(20);

            Property(c => c.TelefoneOpcionalDep)
              .HasMaxLength(20);

            Property(c => c.EmailDep)
              .HasMaxLength(50);

            Property(c => c.DataNascimentoDep)
              .IsRequired();


            HasRequired(p => p.Ficha)
                .WithMany(p => p.Dependentes)
                .HasForeignKey(p => p.FichaId);           

        }
    }
}

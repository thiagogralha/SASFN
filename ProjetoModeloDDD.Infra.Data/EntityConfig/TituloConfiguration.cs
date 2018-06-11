using SASF.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SASF.Infra.Data.EntityConfig
{
    public class TituloConfiguration : EntityTypeConfiguration<Titulo>
    {
        public TituloConfiguration()
        {
            HasKey(p => p.TituloId);

            Property(c => c.Status)
           .IsRequired()
           .HasMaxLength(1);
            
            //HasRequired(p => p.Ficha)
            //   .WithMany(p => p.Titulos)               
            //   .HasForeignKey(p => p.FichaId).WillCascadeOnDelete();
        }
    }
}

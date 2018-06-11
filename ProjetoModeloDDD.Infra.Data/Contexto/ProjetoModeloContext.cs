
using SASF.Domain.Entities;
using SASF.Infra.Data.EntityConfig;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace SASF.Infra.Data.Contexto
{
    public class ProjetoModeloContext : DbContext
    {
        public ProjetoModeloContext()
            :base("sasfni")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Plano> Planos { get; set; }
        public DbSet<Dependente> Dependentes { get; set; }
        public DbSet<Ficha> Ficha { get; set; }
        public DbSet<Titulo> Titulos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));
            
            modelBuilder.Configurations.Add(new FichaConfiguration());
            modelBuilder.Configurations.Add(new PlanoConfiguration());
            modelBuilder.Configurations.Add(new FuncionarioConfiguration());
            modelBuilder.Configurations.Add(new DependenteConfiguration());
            modelBuilder.Configurations.Add(new TituloConfiguration());
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChanges();
        }
    }
}

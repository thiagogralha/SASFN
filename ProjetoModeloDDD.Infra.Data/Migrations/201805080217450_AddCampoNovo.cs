namespace SASF.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCampoNovo : DbMigration
    {
        public override void Up()
        {

            AddColumn("dbo.Funcionario", "IsAdmin", c => c.String());         
            
        }
        
        public override void Down()
        {           
        }
    }
}

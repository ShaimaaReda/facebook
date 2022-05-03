namespace face.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.SignUpCls");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SignUpCls",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        password = c.String(nullable: false),
                        loginErrormassage = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
    }
}

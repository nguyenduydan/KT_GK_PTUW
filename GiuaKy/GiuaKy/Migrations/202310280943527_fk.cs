namespace GiuaKy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fk : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SINHVIENS", "malop", c => c.String(maxLength: 128));
            CreateIndex("dbo.SINHVIENS", "malop");
            AddForeignKey("dbo.SINHVIENS", "malop", "dbo.LOPS", "malop");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SINHVIENS", "malop", "dbo.LOPS");
            DropIndex("dbo.SINHVIENS", new[] { "malop" });
            AlterColumn("dbo.SINHVIENS", "malop", c => c.String());
        }
    }
}

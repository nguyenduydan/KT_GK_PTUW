namespace GiuaKy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LOPS",
                c => new
                    {
                        malop = c.String(nullable: false, maxLength: 128),
                        tenlop = c.String(),
                    })
                .PrimaryKey(t => t.malop);
            
            CreateTable(
                "dbo.SINHVIENS",
                c => new
                    {
                        masv = c.String(nullable: false, maxLength: 128),
                        hosv = c.String(nullable: false),
                        tensv = c.String(nullable: false),
                        ngaysinh = c.DateTime(nullable: false),
                        gioitinh = c.Byte(nullable: false),
                        anhsv = c.String(),
                        diachi = c.String(),
                        malop = c.String(),
                    })
                .PrimaryKey(t => t.masv);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SINHVIENS");
            DropTable("dbo.LOPS");
        }
    }
}

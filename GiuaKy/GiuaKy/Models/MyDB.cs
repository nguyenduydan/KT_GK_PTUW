using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace GiuaKy.Models
{
    public partial class MyDB : DbContext
    {
        public MyDB()
            : base("name=MyDB")
        {
        }
        public DbSet<SINHVIENS> sinhvien {  get; set; }
        public DbSet<LOPS> lop {  get; set; }
    }
}

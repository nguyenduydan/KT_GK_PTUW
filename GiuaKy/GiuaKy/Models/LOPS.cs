using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GiuaKy.Models
{
    public class LOPS
    {
        [Key]
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Mã lớp")]
        public string malop { get; set; }
        [Display(Name = "Tên lớp")]
        public string tenlop { get; set; }

        public ICollection<SINHVIENS> sinhvien { get; set; }
    }
}
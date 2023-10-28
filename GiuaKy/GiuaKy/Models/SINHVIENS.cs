using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GiuaKy.Models
{
    public class SINHVIENS
    {
        [Key]
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Mã Sinh Viên")]
        public string masv { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Họ Sinh Viên")]
        public string hosv { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Tên Sinh Viên")]
        public string tensv { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Ngày Sinh")]
        public DateTime ngaysinh { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Giới Tính")]
        public byte gioitinh { get; set; }

        [Display(Name = "Ảnh Sinh Viên")]
        public string anhsv { get; set; }
        [Display(Name = "Địa Chỉ")]
        public string diachi { get; set; }
        
        //foreign key
        [Display(Name = "Lớp")]
        public string malop { get; set; }
        public virtual LOPS  LOPS { get; set; }
        

    }
}
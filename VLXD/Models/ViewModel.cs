using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VLXD.Models
{
    public class ViewModel
    {
        public KHACHHANG khachhang { get; set; }
        public HOADON hoadon { get; set; }
        public LOAIVATLIEU loaivatlieu { get; set; }
        public NHANVIEN nhanvien { get; set; }
        public CHITIETHOADON chitiethoadon { get; set; }
        public VATLIEU vatlieu { get; set; }
        public double Thanhtien { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VLXD.Models
{
    public class CartItem
    {
        public int MaVL { get; set; }
        public string TenVL { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public double ThanhTien {
            get
            {
                return SoLuong * DonGia;
            }
        }
    }
}
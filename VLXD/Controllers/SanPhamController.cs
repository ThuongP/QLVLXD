using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VLXD.Models;
using System.Data.Entity;
using PagedList;
namespace VLXD.Controllers
{
    public class SanPhamController : Controller
    {
        QLVLXDEntities db = new QLVLXDEntities();
        // GET: SanPham
        public ActionResult Index(string currentFilter, int?page, int MaLoaiSP = 0, string SearchString="")
        {
            if(SearchString!="")
            {
                page = 1;
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                var sanPhams = db.VATLIEUx.Include(s => s.LOAIVATLIEU)
                    .Where(x => x.TenVL.ToUpper().Contains(SearchString.ToUpper())).OrderBy(m=>m.TenVL);

                return View(sanPhams.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            if (MaLoaiSP == 0)// lấy all
            {
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                var sanPhams = db.VATLIEUx.Include(s => s.LOAIVATLIEU).OrderBy(x=>x.TenVL);
                return View(sanPhams.ToPagedList(pageNumber, pageSize));
                //var vatlieu = db.VATLIEUx.ToList();
                //return View(vatlieu);
            }
            else// tìm theo loại sản pham
            {
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                var sanPhams = db.VATLIEUx.Include(s => s.LOAIVATLIEU)
                    .Where(x => x.MaLoaiVL == MaLoaiSP).OrderBy(m=>m.TenVL);
                return View(sanPhams.ToPagedList(pageNumber, pageSize));
            }
        }
    }
}
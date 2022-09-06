using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VLXD.Models;
using System.Net;
using System.Net.Mail;

namespace VLXD.Controllers
{
    public class GiohangController : Controller
    {
        private QLVLXDEntities db = new QLVLXDEntities();
        // GET: Giohang
        public ActionResult Index()
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            return View(giohang);
        }

        public RedirectToRouteResult AddToCart(int MaVL)
        {
            if(Session["giohang"] == null)
            {
                Session["giohang"] = new List<CartItem>();
            }
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            if(giohang.FirstOrDefault(m=>m.MaVL == MaVL) == null)
            {
                VATLIEU vl = db.VATLIEUx.Find(MaVL);
                CartItem item = new CartItem();
                item.MaVL = MaVL;
                item.TenVL = vl.TenVL;
                item.DonGia = Convert.ToDouble(vl.GiaVL);
                item.SoLuong = 1;
                giohang.Add(item);
            }
            else
            {
                CartItem item = giohang.FirstOrDefault(m => m.MaVL == MaVL);
                item.SoLuong++;
            }
            Session["giohang"] = giohang;
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult Update(int MaVL, int txtSoluong)
        {
            
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            
            CartItem item = giohang.FirstOrDefault(m => m.MaVL == MaVL);
            if(item != null)
            {
                item.SoLuong = txtSoluong;
                Session["giohang"] = giohang;
            }
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult Delete(int MaVL)
        {

            List<CartItem> giohang = Session["giohang"] as List<CartItem>;

            CartItem item = giohang.FirstOrDefault(m => m.MaVL == MaVL);
            if (item != null)
            {
                giohang.Remove(item);
                Session["giohang"] = giohang;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Order(string Email, string Phone)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            string sMsg = "<html><body><table><caption> Thông tin đặt hàng</caption>";
            sMsg += "<tr><th>STT</th><th>Tên hàng</th><th>Số lượng</th><th>Đơn giá</th><th>Thành tiền</th></tr>";
            int i = 0;
            double tongtien = 0;
            foreach(var item in giohang)
            {
                i++;
                sMsg += "<tr>";
                sMsg += "<td>" + i.ToString() + "</td>";
                sMsg += "<td>" + item.TenVL + "</td>";
                sMsg += "<td>" + item.SoLuong + "</td>";
                sMsg += "<td>" + item.DonGia.ToString("#,###") + "</td>";
                sMsg += "<td>" + item.ThanhTien.ToString("#,###") + "</td>";
                tongtien += item.ThanhTien;
            }
            sMsg += "<tr><th colspan='5'> Tổng cộng" + tongtien.ToString("#,###") + "</th></tr>";
            sMsg += "</table></body></html>";

            try
            {
                MailMessage mail = new MailMessage("vatlieuxaydungonline2@gmail.com", Email, "Thông tin đặt hàng", sMsg);
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("vatlieuxaydungonline2", "vatlieuxaydung2");
                mail.IsBodyHtml = true;
                client.Send(mail);
            }
            catch(Exception ex)
            {
                //ViewBag.Error = ex.Message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
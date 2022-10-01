using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using CoffeeShopProject.Models;
using System.Threading.Tasks;

namespace CoffeeShopProject.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            
            SliderDBEntities1 db = new SliderDBEntities1();
            var data = (from d in db.TblSliders select d).ToList();


            return View(data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Menu()
        {
            MenuDBEntities1 db = new MenuDBEntities1();
            var data = (from d in db.MenuTable select d).ToList();
            return View(data);


        }
        [Authorize]
        public ActionResult AddToCart(string id,string OrderNo)
       {

            UserCartDBEntities db = new UserCartDBEntities();

       
            MenuDBEntities1 db2 = new MenuDBEntities1();
            var data = (from d in db2.MenuTable select d).ToList();

            AdminCartDBEntities db3 = new AdminCartDBEntities();
            AdminCartTbl table = new AdminCartTbl();

            foreach(var item in data)
            {
                if(Int32.Parse(id)==item.Id)
                {
                    CartTable cart = new CartTable();
                    cart.Title = item.Title;
                    cart.Path = item.Path;
                    cart.Price = item.Price;
                    cart.Category = item.Category;
                    cart.MenuId = id;
                    cart.Quantity = OrderNo;
                    cart.UserId = User.Identity.GetUserName();
                    cart.Status = "Pending";


                    table.Title = item.Title;
                    table.Path = item.Path;
                    table.Price = item.Path;
                    table.Category = item.Category;
                    table.MenuId = id;
                    table.Quantity = OrderNo;
                    table.UserId = User.Identity.GetUserName();
                    table.Status = "Pending";

                    db.CartTable.Add(cart);
                    db3.AdminCartTbl.Add(table);
                    db.SaveChanges();
                    db3.SaveChanges();
                }
            }
            UserCartDBEntities db4 = new UserCartDBEntities();
            var data2 = (from d in db4.CartTable select d).ToList();

            List<CartTable> cartList = new List<CartTable>();
           
            foreach (var item in data2)
            {
                if (item.UserId == User.Identity.GetUserName())
                {
                    cartList.Add(item);
                    //String[] parts = item.Price.Split(new[] { '/' });
                    //String price = parts[0];
                    //quantity = Int16.Parse(item.Quantity);
                    //total_q += quantity;
                    //total += (Int16.Parse(price) *quantity);
                  
                }
            }

            //ViewBag.Total = total;
            //ViewBag.Quantity = total_q;

         
         


            //HttpCookie tg = new HttpCookie("total1", "" + total);
            //HttpCookie tg2 = new HttpCookie("quantity1", "" + total_q);
            //Response.Cookies.Add(tg);
            //Response.Cookies.Add(tg2);

            return View("~/Views/Home/ShowCart.cshtml",cartList);
        }

        public ActionResult ShowCart()
        {
            UserCartDBEntities db = new UserCartDBEntities();
            var data = (from d in db.CartTable select d).ToList();

            List<CartTable> cartList = new List<CartTable>();
            int total = 0;
            int quantity = 0;
            int total_q = 0;
            foreach (var item in data)
            {
                if(item.UserId == User.Identity.GetUserName())
                {
                    cartList.Add(item);
                    String[] parts = item.Price.Split(new[] { '/' });
                    String price = parts[0];
                    quantity = Int16.Parse(item.Quantity);
                    total_q += quantity;
                    total += (Int16.Parse(price) * quantity);
                }
            }

            TempData["total1"] = total;
            TempData["quantity1"] = total_q;

           

            //HttpCookie tg = new HttpCookie("total1", "" + total);
            //HttpCookie tg2 = new HttpCookie("quantity1", "" + total_q);
            //Response.Cookies.Add(tg);
            //Response.Cookies.Add(tg2);

            return View(cartList);
        }

        public ActionResult CancelCart(string id)
        {

            using(var context = new UserCartDBEntities())
            {
                var cart = context.CartTable.FirstOrDefault(x => x.MenuId == id);
                cart.Status = "Cancel";
                context.SaveChanges();
            }

            using (var context = new AdminCartDBEntities())
            {
                var cart = context.AdminCartTbl.FirstOrDefault(x => x.MenuId == id);
                cart.Status = "Cancel";
                context.SaveChanges();
            }

          


            UserCartDBEntities db = new UserCartDBEntities();
            var data = (from d in db.CartTable select d).ToList();

            List<CartTable> cartList = new List<CartTable>();
            int total = 0;
            int quantity = 0;
            foreach (var item in data)
            {
                if (item.UserId == User.Identity.GetUserName())
                {
                    cartList.Add(item);
                    //total += Int32.Parse(item.Price);
                    //quantity +=Int32.Parse(item.Quantity);
                }
            }

            //HttpCookie a2 = new HttpCookie("mes2", "Item order Canceled");
            ///*a2.Expires = DateTime.Now.AddDays(30);*/
            //Response.Cookies.Add(a2);
            return View("~/Views/Home/ShowCart.cshtml", cartList);


        }

        public ActionResult DeleteCart(string id)
        {

            using (var context = new UserCartDBEntities())
            {
                var cart = context.CartTable.FirstOrDefault(x => x.MenuId == id);
                context.CartTable.Remove(cart);
                context.SaveChanges();
            }


            UserCartDBEntities db = new UserCartDBEntities();
            var data = (from d in db.CartTable select d).ToList();

            List<CartTable> cartList = new List<CartTable>();
            int total = 0;
            int quantity = 0;
            foreach (var item in data)
            {
                if (item.UserId == User.Identity.GetUserName())
                {
                    cartList.Add(item);
                    //total += Int32.Parse(item.Price);
                    //quantity +=Int32.Parse(item.Quantity);
                }
            }

            return View("~/Views/Home/ShowCart.cshtml", cartList);


        }




       
        public ActionResult Gallery()
        {
            GalleryDBEntities db = new GalleryDBEntities();
            var data = (from d in db.GalleryTable select d).ToList();

            return View(data);
        }
        public ActionResult Review()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Book()
        {

            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Book(BookTableTbl table)
        {
            table.Status = "Pending";
            table.UserId = User.Identity.GetUserId();

           using(BookTableDBEntities db = new BookTableDBEntities())
            {
                db.BookTableTbl.Add(table);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.BookTable = "Table booking successfull!!";
            }

            using (BookTableDBEntities1 db = new BookTableDBEntities1())
            {
                AdminBookTable table1 = new AdminBookTable();
                table1.FullName = table.FullName;
                table1.PhoneNo = table.PhoneNo;
                table1.Email = table.Email;
                table1.Day = table.Day;
                table1.Hours = table.Hours;
                table1.PersonNo = table.PersonNo;
                table1.UserId = table.UserId;
                table1.Status = table.Status;
                db.AdminBookTable.Add(table1);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.BookTable = "Table booking successfull!!";
            }

            return View();
        }

        public ActionResult CancelTable(string id,string UserId)
        {

            using (var context = new BookTableDBEntities())
            {
                //int _id = Int32.Parse(id);
                var table = context.BookTableTbl.FirstOrDefault(x => x.UserId == UserId);
                table.Status = "Cancel";
                context.SaveChanges();
            }
            using (var context = new BookTableDBEntities1())
            {
                //int _id= Int32.Parse(id);
                var table = context.AdminBookTable.FirstOrDefault(x => x.UserId == UserId);
                table.Status = "Cancel";
                context.SaveChanges();
            }

            BookTableDBEntities db = new BookTableDBEntities();
            var data = (from d in db.BookTableTbl select d).ToList();

            List<BookTableTbl> tableList = new List<BookTableTbl>();

            foreach (var item in data)
            {
                if (item.UserId == User.Identity.GetUserId())
                {
                    tableList.Add(item);
                }
            }

            return View("~/Views/Home/ShowBookTable.cshtml",tableList);


        }

        public ActionResult DeleteTable(string id,string UserId)
        {

            using (var context = new BookTableDBEntities())
            {
                var table = context.BookTableTbl.FirstOrDefault(x => x.UserId == UserId);
                context.BookTableTbl.Remove(table);
                context.SaveChanges();
            }
         
            BookTableDBEntities db = new BookTableDBEntities();
            var data = (from d in db.BookTableTbl select d).ToList();

            List<BookTableTbl> tableList = new List<BookTableTbl>();

            foreach (var item in data)
            {
                if (item.UserId == User.Identity.GetUserId())
                {
                    tableList.Add(item);
                }
            }

            return View("~/Views/Home/ShowBookTable.cshtml", tableList);


        }


        [Authorize]
        public ActionResult ShowBookTable()
        {
            BookTableDBEntities db = new BookTableDBEntities();
            var data = (from d in db.BookTableTbl select d).ToList();

            List<BookTableTbl> tableList = new List<BookTableTbl>();

            foreach(var item in data)
            {
                if(item.UserId == User.Identity.GetUserId())
                {
                    tableList.Add(item);
                }
            }

            return View(tableList);
        }
        [HttpGet]
        public ActionResult Contact()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Contact(Feedback2 f)
        {
          

            FeedbackTable feedback = new FeedbackTable();

            feedback.UserId = User.Identity.Name;
            feedback.FullName = f.FullName;
            feedback.PhoneNo = f.PhoneNo;
            feedback.Email = f.Email;
            feedback.Feedback = f.Feedback;

            using (FeedbackDBEntities db = new FeedbackDBEntities())
            {
                db.FeedbackTable.Add(feedback);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.feedback = "Thank you";

            }

            return View();
        }
    }
}
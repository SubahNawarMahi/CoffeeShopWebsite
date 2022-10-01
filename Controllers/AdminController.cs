using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeShopProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

using System.IO;



namespace CoffeeShopProject.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddMenuItem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddMenuItem(MenuTable item)
        {

            string fileName = Path.GetFileNameWithoutExtension(item.ImageFile.FileName);
            string extension = Path.GetExtension(item.ImageFile.FileName);

            fileName = fileName + extension;
            item.Path = "~/menu images/" + fileName;



            fileName = Path.Combine(Server.MapPath("~/menu images/"), fileName);
            item.ImageFile.SaveAs(fileName);

            /* item.Path = "~/menu_images/" + item.Path;*/

            using (MenuDBEntities1 db = new MenuDBEntities1())
            {

                db.MenuTable.Add(item);
                db.SaveChanges();
                //ViewBag.Message("Item added successfully!!!");

            }
            ModelState.Clear();

            return View();
        }
        [HttpGet]
        public ActionResult GetMenuItem(int id)
        {
            MenuTable image = new MenuTable();

            using (MenuDBEntities1 db = new MenuDBEntities1())
            {

                image = db.MenuTable.Where(x => x.Id == id).FirstOrDefault();

            }

            return View(image);

        }
        public ActionResult GetMenuList()
        {
            MenuDBEntities1 db = new MenuDBEntities1();
            var data = db.MenuTable.ToList();

            return View(data);
        }

        public ActionResult EditMenuItem(int id)
        {
            MenuTable image = new MenuTable();

            using (MenuDBEntities1 db = new MenuDBEntities1())
            {

                image = db.MenuTable.Where(x => x.Id == id).FirstOrDefault();

            }

            return View(image);

        }
        [HttpPost]
        public ActionResult EditMenuItem(MenuTable image)
        {


            string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            string extension = Path.GetExtension(image.ImageFile.FileName);

            fileName = fileName + extension;
            image.Path = "~/menu images/" + fileName;



            fileName = Path.Combine(Server.MapPath("~/menu images/"), fileName);
            image.ImageFile.SaveAs(fileName);



            //string path = "~/menu images/" + image.Path;
            //image.Path = path;

            bool f = EditSubmitedMenuItem(image.Id, image);
            if (f)
            {
                return RedirectToAction("GetMenuList");
            }

            return View();

        }


        public bool EditSubmitedMenuItem(int id, MenuTable it)
        {

            using (MenuDBEntities1 db = new MenuDBEntities1())
            {
                var image = db.MenuTable.FirstOrDefault(x => x.Id == id);

                if (image != null)
                {
                    image.Id = it.Id;
                    image.Title = it.Title;
                    image.Price = it.Price;
                    image.Category = it.Category;
                    image.Des = it.Des;
                    image.Path = it.Path;
                }

                db.SaveChanges();
                return true;
            }


        }

        public ActionResult DeleteMenu(MenuTable image)
        {
            bool f = DeleteMenuItem(image.Id);

            if (f)
            {
                return RedirectToAction("GetMenuList");
            }

            return View();
        }

        [HttpPost]
        public bool DeleteMenuItem(int id)
        {
            using (var db = new MenuDBEntities1())
            {
                var image = db.MenuTable.FirstOrDefault(x => x.Id == id);

                if (image != null)
                {
                    db.MenuTable.Remove(image);
                    db.SaveChanges();
                    return true;
                }

                return false;
            }

        }

        public ActionResult AddGalleryImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddGalleryImage(GalleryTable image)
        {
            string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            string extension = Path.GetExtension(image.ImageFile.FileName);

            fileName = fileName + extension;
            image.Path = "~/gallery_images/" + fileName;


            fileName = Path.Combine(Server.MapPath("~/gallery_images/"), fileName);
            image.ImageFile.SaveAs(fileName);

            /* item.Path = "~/menu_images/" + item.Path;*/

            using (GalleryDBEntities db = new GalleryDBEntities())
            {

                db.GalleryTable.Add(image);
                db.SaveChanges();
               /* ViewBag.Message("Item added successfully!!!");*/

            }
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult GetGalleryImage(int id)
        {
            GalleryTable image = new GalleryTable();

            using (GalleryDBEntities db = new GalleryDBEntities())
            {

                image = db.GalleryTable.Where(x => x.Id == id).FirstOrDefault();

            }

            return View(image);

        }
        public ActionResult GetGalleryList()
        {
            GalleryDBEntities db = new GalleryDBEntities();
            var data = db.GalleryTable.ToList();

            return View(data);
        }

        public ActionResult EditGalleryImage (int id)
        {
            GalleryTable image = new GalleryTable();

            using (GalleryDBEntities db = new GalleryDBEntities())
            {

                image = db.GalleryTable.Where(x => x.Id == id).FirstOrDefault();

            }

            return View(image);

        }
        [HttpPost]
        public ActionResult EditGalleryImage(GalleryTable image)
        {
            string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            string extension = Path.GetExtension(image.ImageFile.FileName);

            fileName = fileName + extension;
            image.Path = "~/gallery_images/" + fileName;


            fileName = Path.Combine(Server.MapPath("~/gallery_images/"), fileName);
            image.ImageFile.SaveAs(fileName);

            //string path = "~/gallery_images/" + image.Path;
            //image.Path = path;

            bool f = EditSubmitGalleryImage(image.Id, image);
            if (f)
            {
                return RedirectToAction("GetGalleryList");
            }

            return View();

        }


        public bool EditSubmitGalleryImage(int id, GalleryTable it)
        {

            using (GalleryDBEntities db = new GalleryDBEntities())
            {
                var image = db.GalleryTable.FirstOrDefault(x => x.Id == id);

                if (image != null)
                {
                    image.Id = it.Id;
                    image.Path = it.Path;
                }

                db.SaveChanges();
                return true;
            }


        }

        public ActionResult Delete(GalleryTable image)
        {
            bool f = DeleteGalleryImage(image.Id);

            if (f)
            {
                return RedirectToAction("GetGalleryList");
            }

            return View();
        }

        [HttpPost]
        public bool DeleteGalleryImage(int id)
        {
            using (var db = new GalleryDBEntities())
            {
                var image = db.GalleryTable.FirstOrDefault(x => x.Id == id);

                if (image != null)
                {
                    db.GalleryTable.Remove(image);
                    db.SaveChanges();
                    return true;
                }

                return false;
            }

        }

        public ActionResult GetAllUserCartItem()
        {
            AdminCartDBEntities db = new AdminCartDBEntities();
            var data = db.AdminCartTbl.ToList();

            return View(data);
            
        }

        public ActionResult AcceptOrderItem(int id)
        {
            id++;
            string userId;
            using (var context = new AdminCartDBEntities())
            {
                //int _id = Int32.Parse(id);
                var table = context.AdminCartTbl.FirstOrDefault(x => x.Id == id);
                userId = table.UserId;
                table.Status = "Accept";
                context.SaveChanges();
            }
            using (var context = new UserCartDBEntities())
            {
                //int _id = Int32.Parse(id);
                var table = context.CartTable.FirstOrDefault(x => x.UserId == userId);
                table.Status = "Accept\n Payment: Cash on delivery";
                context.SaveChanges();
            }

            /*UserCartDBEntities db = new UserCartDBEntities();
            var data = db.CartTable.ToList();*/

            AdminCartDBEntities db = new AdminCartDBEntities();
            var data = db.AdminCartTbl.ToList();

            return View("~/Views/Admin/GetAllUserCartItem.cshtml",data);
        }

        public ActionResult RejectOrderItem(int id)
        {
            id++;
            string userId;
            using (var context = new AdminCartDBEntities())
            {
                //int _id = Int32.Parse(id);
                var table = context.AdminCartTbl.FirstOrDefault(x => x.Id == id);
                userId = table.UserId;
                table.Status = "Rejected";
                context.SaveChanges();
            }
            using (var context = new UserCartDBEntities())
            {
                //int _id = Int32.Parse(id);
                var table = context.CartTable.FirstOrDefault(x => x.UserId == userId);
                table.Status = "Rejected";
                context.SaveChanges();
            }

            AdminCartDBEntities db = new AdminCartDBEntities();
            var data = db.AdminCartTbl.ToList();

            return View("~/Views/Admin/GetAllUserCartItem.cshtml", data);
        }

        public ActionResult DeleteCartAdmin(int id)
        {
            id++;
            using (var context = new AdminCartDBEntities())
            {
                var cart = context.AdminCartTbl.FirstOrDefault(x => x.Id == id);

                if (cart != null)
                {
                    context.AdminCartTbl.Remove(cart);
                    context.SaveChanges();
                    
                }

              
            }

            AdminCartDBEntities db = new AdminCartDBEntities();
            var data = db.AdminCartTbl.ToList();

            return View("~/Views/Admin/GetAllUserCartItem.cshtml", data);

        }

        public ActionResult GetAllUserTableList()
        {
            BookTableDBEntities1 db = new BookTableDBEntities1();
            var data = db.AdminBookTable.ToList();

            return View(data);

        }


        public ActionResult AcceptTableRequest(int id)
        {
           
            string userId;
            using (var context = new BookTableDBEntities1())
            {
                //int _id = Int32.Parse(id);
                var table = context.AdminBookTable.FirstOrDefault(x => x.Id == id);
                userId = table.UserId;
                table.Status = "Accept";
                context.SaveChanges();
            }
            id++;
            using (var context = new BookTableDBEntities())
            {
                //int _id = Int32.Parse(id);
                var table = context.BookTableTbl.FirstOrDefault(x => x.UserId == userId);
                table.Status = "Accept";
                context.SaveChanges();
            }

            BookTableDBEntities1 db = new BookTableDBEntities1();
            var data = db.AdminBookTable.ToList();

            return View("~/Views/Admin/GetAllUserTableList.cshtml", data);
        }

        public ActionResult RejectTableIRequest(int id)
        {
           
            string userId;
            using (var context = new BookTableDBEntities1())
            {
                //int _id = Int32.Parse(id);
                var table = context.AdminBookTable.FirstOrDefault(x => x.Id == id);
                userId = table.UserId;
                table.Status = "Rejected";
                context.SaveChanges();
            }
            id++;
            using (var context = new BookTableDBEntities())
            {
                //int _id = Int32.Parse(id);
                var table = context.BookTableTbl.FirstOrDefault(x => x.UserId == userId);
                table.Status = "Rejected";
                context.SaveChanges();
            }

            BookTableDBEntities1 db = new BookTableDBEntities1();
            var data = db.AdminBookTable.ToList();

            return View("~/Views/Admin/GetAllUserTableList.cshtml", data);
        }

        public ActionResult DeleteTableRequest(int id)
        {
            
            using (var context = new BookTableDBEntities1())
            {
                var cart = context.AdminBookTable.FirstOrDefault(x => x.Id == id);

                if (cart != null)
                {
                    context.AdminBookTable.Remove(cart);
                    context.SaveChanges();

                }


            }

            BookTableDBEntities1 db = new BookTableDBEntities1();
            var data = db.AdminBookTable.ToList();

            return View("~/Views/Admin/GetAllUserTableList.cshtml", data);

        }

        public ActionResult GetFeedBackList()
        {
            FeedbackDBEntities db = new FeedbackDBEntities();
            var data = db.FeedbackTable.ToList();

            return View(data);
        }

    }
}
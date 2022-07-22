using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CoffeeShopProject.Models;
using System.IO;

namespace CoffeeShopProject.Controllers
{
    [Authorize]
    public class SliderController : Controller
    {
        // GET: Slider
        [HttpGet]
        public ActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Add(TblSlider it)
        {
            string fileName = Path.GetFileNameWithoutExtension(it.ImageFile.FileName);
            string extension = Path.GetExtension(it.ImageFile.FileName);

            fileName = fileName + extension;
            it.Path = "~/images/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
            it.ImageFile.SaveAs(fileName);
            using (SliderDBEntities1 db = new SliderDBEntities1())
            {
                db.TblSliders.Add(it);
                db.SaveChanges();
                ViewBag.Message = "Upload Successful";
            }
            ModelState.Clear();
            return View();
        }
        [HttpGet]
        public ActionResult GetASlider(int id)
        {
            TblSlider image = new TblSlider();

            using (SliderDBEntities1 db = new SliderDBEntities1())
            {

                image = db.TblSliders.Where(x => x.ID == id).FirstOrDefault();

            }

            return View(image);

        }
        public ActionResult GetSliderList()
        {
            SliderDBEntities1 db = new SliderDBEntities1();
            var data = db.TblSliders.ToList();

            return View(data);
        }

        public ActionResult EditSlider(int id)
        {
            TblSlider image = new TblSlider();

            using (SliderDBEntities1 db = new SliderDBEntities1())
            {

                image = db.TblSliders.Where(x => x.ID == id).FirstOrDefault();

            }

            return View(image);

        }
        [HttpPost]
        public ActionResult EditSlider(TblSlider slider)
        {


            //string fileName = Path.GetFileNameWithoutExtension(slider.ImageFile.FileName);
            //string extension = Path.GetExtension(slider.ImageFile.FileName);

            //fileName = fileName + extension;
            //slider.Path = "~/images/" + fileName;



            //fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
            //slider.ImageFile.SaveAs(fileName);



            string path = "~/images/" + slider.Path;
            slider.Path = path;

            bool f = EditSubmitSlider(slider.ID, slider);
            if (f)
            {
                return RedirectToAction("GetSliderList");
            }

            return View();

        }


        public bool EditSubmitSlider(int id, TblSlider it)
        {

            using (SliderDBEntities1 db = new SliderDBEntities1())
            {
                var image = db.TblSliders.FirstOrDefault(x => x.ID == id);

                if (image != null)
                {
                    image.Title = it.Title;
                    image.Path = it.Path;
                }

                db.SaveChanges();
                return true;
            }


        }

        public ActionResult Delete(TblSlider image)
        {
            bool f = DeleteSlider(image.ID);

            if (f)
            {
                return RedirectToAction("GetSliderList");
            }

            return View();
        }

        [HttpPost]
        public bool DeleteSlider(int id)
        {
            using (var db = new SliderDBEntities1())
            {
                var image = db.TblSliders.FirstOrDefault(x => x.ID == id);

                if (image != null)
                {
                    db.TblSliders.Remove(image);
                    db.SaveChanges();
                    return true;
                }

                return false;
            }

        }


    }


}

    

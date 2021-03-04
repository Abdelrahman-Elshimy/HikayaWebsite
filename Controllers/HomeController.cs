using Hikaya.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hikaya.Controllers
{
    public class HomeController : Controller
    {
        IStoryReposo storyReposo;
        public HomeController(IStoryReposo storyReposo)
        {
            this.storyReposo = storyReposo;
        }
        public ActionResult Index()
        {
            var stories = this.storyReposo.GetAllStories().Where(p => p.IsPublished != null).OrderByDescending(p => p.PostDate).ToList();
            
            return View(stories);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
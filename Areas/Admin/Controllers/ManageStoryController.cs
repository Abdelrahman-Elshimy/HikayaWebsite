using Hikaya.Business;
using Hikaya.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hikaya.Areas.Admin.Controllers
{
   
    public class ManageStoryController : Controller
    {
        IStoryReposo storyReposo;
        ISavedStoryReposo savedStoryReposo;
        public ManageStoryController(IStoryReposo reposo, ISavedStoryReposo reposo1)
        {
            this.storyReposo = reposo;
            this.savedStoryReposo = reposo1;
        }

        [Authorize(Roles"Admin")]
        // GET: Admin/ManageStory
        public ActionResult Index()
        {
            var stories = this.storyReposo.GetAllStories()
                .Where(p => !p.IsPublished.HasValue || p.IsPublished == false)
                .OrderBy(p => p.PostDate)
                .ToList();
            return View(stories);
        }
        [Authorize(Roles"Admin")]
        public ActionResult Details(int id)
        {
            Story story = storyReposo.GetStoryById(id);
            return View(story);
        }
        [Authorize(Roles"Admin")]
        public ActionResult Publish(int id)
        {
            Story story = storyReposo.GetStoryById(id);
            story.IsPublished = true;
            storyReposo.Update(story);
            TempData["SuccessMessageAdmin"] = "The Story has been published successfully";
            return RedirectToAction("Index");
        }
        
    }
}

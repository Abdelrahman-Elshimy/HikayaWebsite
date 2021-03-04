using Hikaya.Business;
using Hikaya.DAL;
using Hikaya.Helpers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hikaya.Controllers
{
    public class StoryController : Controller
    {
        IStoryReposo storyReposo;
        ISavedStoryReposo savedStoryReposo;
        public StoryController(IStoryReposo reposo, ISavedStoryReposo reposo1)
        {
            this.storyReposo = reposo;
            this.savedStoryReposo = reposo1;

        }
        // GET: Story
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Create()
        {
            Story story = new Story();
            story.StoryPlots = new List<StoryPlot>();
            story.StoryPlots.Add(new StoryPlot());

            return View(story);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(Story model, IEnumerable<HttpPostedFileBase>files)
        {
            model.PostDate = DateTime.Now;
            model.UserId = UserHelper.GetCurrentUser();
            int i = 0;
            foreach(StoryPlot storyPlot in model.StoryPlots)
            {
                
                if(files != null && files.Count() > i && files.ElementAt(i) != null)
                {
                    var file = files.ElementAt(i);
                    string image = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Images"), image);
                    file.SaveAs(path);
                    storyPlot.ImageUrl = "/Images/" + image;

                }
                i++;
            }
            StoryRep storyRep = new StoryRep();
            storyRep.Add(model);
            TempData["SuccessMessage"] = "The Story has been Added Successfully";
            return Redirect(Url.Content("~/"));
        }

        public ActionResult Details(int id)
        {
            Story story = this.storyReposo.GetStoryById(id);
            return View(story);
        }
        [Authorize]
        public ActionResult SavedStory(int id)
        {
            SavedStory savedStory = new SavedStory();
            savedStory.StoryId = id;
            savedStory.UserId = UserHelper.GetCurrentUser();
            savedStory.Date = DateTime.Now;
            savedStoryReposo.Add(savedStory);
            TempData["SuccessMessage"] = "The Story Has Been Saved Successfully";
            return RedirectToAction("Index", "Home");
            
        }
        [Authorize]
        public ActionResult DeleteSavedStory(int id)
        {
            savedStoryReposo.Delete(id);
            TempData["SuccessMessage"] = "The Saved Story has been Deleted successfully";
            return RedirectToAction("Index", "Home");

        }

        [Authorize]
        public ActionResult SavedStories()
        {
            var stories = savedStoryReposo.GetAllSavedStories(UserHelper.GetCurrentUser());
            return View(stories);
        }

    }
}
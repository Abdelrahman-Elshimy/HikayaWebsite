using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hikaya.Business;
using Hikaya.Business.Interfaces;
using Hikaya.DAL;
using Hikaya.Helpers;

namespace Hikaya.Controllers
{
    public class MessageController : Controller
    {
        IStoryReposo storyReposo;
        IFeedBackMessage feedBackMessage;
        public MessageController(IStoryReposo storyReposo, IFeedBackMessage feedBackMessage)
        {
            this.storyReposo = storyReposo;
            this.feedBackMessage = feedBackMessage;
        }
        
        [Authorize]
        public ActionResult Send(int id)
        {
            FeedBack feedBack = new FeedBack();
            feedBack.StoryId = id;
            feedBack.Story = this.storyReposo.GetStoryById(id);
            return View(feedBack);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Send(FeedBack model)
        {
            model.Date = DateTime.Now;
            model.UserId = UserHelper.GetCurrentUser();
            feedBackMessage.Add(model);
            TempData["SuccessMessage"] = "Your Feed Back Has Been Added Successfully";
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult Inbox()
        {
            var messages = feedBackMessage.GetFeedBacks(UserHelper.GetCurrentUser()).ToList();
            return View(messages);
        }
        
    }
}
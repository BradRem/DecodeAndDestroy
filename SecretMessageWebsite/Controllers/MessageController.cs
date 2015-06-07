using SecretMessageWebsite.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SecretMessageWebsite.Models;
namespace SecretMessageWebsite.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        [HttpGet]
        public ActionResult Index(string id, string code = "")
        {
            return View(new PromptRetrieveModel
            {   
                LinkId = id,
                Code = code
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowMessage(string id, string code, string key = "")
        {
            var service = new SecretMessageRetrieverService();
            try
            {
                var message = service.RetrieveMessage(id, code, key);
                service.DeleteMessage(id);

                var viewModel = new DisplayMessageModel
                {
                    Message = message
                };
                return View(viewModel);
            }
            catch
            {
            }

            return View(new DisplayMessageModel
            {
                ErrorMessage = "This entry does not exist or incorrect information was entered."
            });
        }
    }
}
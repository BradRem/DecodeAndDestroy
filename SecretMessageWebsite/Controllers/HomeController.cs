using SecretMessageWebsite.Models;
using SecretMessageWebsite.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SecretMessageWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new SecretMessageModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string plainText, string key)
        {
            var service = new SecretMessageSaverService(key);
            service.SaveSecretMessage(plainText);

            var linkUrl = GetLinkUrl(service.LinkId, service.SystemCreatedPassword);

            var viewModel = new SecretMessageModel
            {
                PlainText = plainText,
                LinkUrl = linkUrl
            };

            return View(viewModel);
        }

        private string GetLinkUrl(string link, string code)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("https://{0}", Request.Url.Host);
            if (Request.Url.Port != 80 && Request.Url.Port != 443)
            {
                sb.AppendFormat(":{0}", Request.Url.Port);
            }
            sb.AppendFormat("/msg/{0}", Url.Encode(link));

            if(!string.IsNullOrWhiteSpace(code))
            {
                sb.AppendFormat("/{0}", code);
            }

            return sb.ToString();
        }

        public class RemoveOldMessageModel
        {
            public DateTime Date { get; set; }
        }

        [HttpPost]
        public void RemoveOldMessages(RemoveOldMessageModel model)
        {
            var service = new ManageDatabaseService();
            service.RemoveOldMessages(model.Date);
        }

        public ActionResult About()
        {
            return View();
        }

    
    }
}
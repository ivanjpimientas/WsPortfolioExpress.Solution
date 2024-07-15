using Markdig;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WsPortfolioExpress.Common.Entities;
using WsPortfolioExpress.Web.Models;
using WsPortfolioExpress.Web.Services;
using static WsPortfolioExpress.Web.Enums.CommonEnums;

namespace WsPortfolioExpress.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public void SwalAlert(string message, NotifyType notificationType)
        {
            var msg = "<script>Swal.fire({'icon': '" + notificationType.ToString() + "', 'title': '" + message + "', 'showConfirmButton': true, 'confirmButtonText': 'Ok' })</script>";
            TempData["notification"] = msg;
        }

        private BrandViewModel GetStaticBrandViewModel(string strfile)
        {
            string filePath = Path.Combine(strfile, "branding.json");

            var json = System.IO.File.ReadAllText(filePath);

            try
            {
                BrandViewModel model;
                var jObject = JObject.Parse(json);
                if (jObject != null)
                {
                    model = new BrandViewModel()
                    {
                        Guid_app = (string)jObject["guid_app"],
                        Brand_icon = (string)jObject["brand_icon"],
                        User_icon = (string)jObject["user_icon"],
                        App_title = (string)jObject["app_title"],
                        App_username = (string)jObject["app_username"],
                        Content_index = (string)jObject["content_index"],
                        Footer_string = (string)jObject["footer_string"],
                        Website_url = (string)jObject["website_url"],
                        Facebook_url = (string)jObject["facebook_url"],
                        Twitter_url = (string)jObject["twitter_url"],
                        Instagram_url = (string)jObject["instagram_url"],
                        Linkedin_url = (string)jObject["linkedin_url"],
                        Youtube_channel = (string)jObject["youtube_channel"],
                        Updated_at = (string)jObject["updated_at"]
                    };

                    return model;
                }
                else
                {
                    return new BrandViewModel();
                }
            }
            catch (Exception)
            {
                return new BrandViewModel();
            }
        }

        public void GetUserLoginInfo(User user)
        {
            TempData["username"] = user.Name;
            TempData["useremail"] = user.Email;
            TempData["usertoken"] = user.Token;
            UserService.UserLogin = user;
        }

        public void GetHeaderDataInfo(string strfilePath)
        {
            var model = GetStaticBrandViewModel(strfilePath);
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            TempData["apptitle"] = model.App_title;
            TempData["icobrand"] = model.Brand_icon;
            TempData["appuser"] = model.App_username;
            TempData["icouser"] = model.User_icon;
            TempData["content"] = Markdown.ToHtml(model.Content_index, pipeline);
            TempData["footer"] = model.Footer_string;
            TempData["facebook_url"] = model.Facebook_url;
            TempData["twitter_url"] = model.Twitter_url;
            TempData["instagram_url"] = model.Instagram_url;
            TempData["linkedin_url"] = model.Linkedin_url;
            TempData["youtube_channel"] = model.Youtube_channel;
            TempData["website_url"] = model.Website_url;
        }
    }
}

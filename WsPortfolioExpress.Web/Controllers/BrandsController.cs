using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WsPortfolioExpress.Web.Enums;
using WsPortfolioExpress.Web.Models;
using WsPortfolioExpress.Web.Services;

namespace WsPortfolioExpress.Web.Controllers
{
    public class BrandsController : BaseController
    {
        private readonly IWebHostEnvironment environment;

        public BrandsController(IWebHostEnvironment _environment)
        {
            environment = _environment;
        }

        public IActionResult Index()
        {
            string uniqueFile = "branding.json";
            var model = GetBrandViewModel(uniqueFile);
            GetUserLoginInfo(UserService.UserLogin);
            OnLoadHeaderComponent();
            return View(model);
        }

        private BrandViewModel GetBrandViewModel(string strfile)
        {
            var pFolder = Path.Combine(environment.WebRootPath, "settings/");
            string filePath = Path.Combine(pFolder, strfile);

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

        [HttpPost]
        public IActionResult Update(BrandViewModel model)
        {
            try
            {
                var pFolder = Path.Combine(environment.WebRootPath, "settings/");
                string filePath = Path.Combine(pFolder, "branding.json");

                if (model.BrandIconFile != null)
                {
                    string uniqueBrandIcon = ProcessUploadedFile(model, CommonEnums.fileType.icon);
                    model.Brand_icon = string.Format("/settings/img/{0}", uniqueBrandIcon);
                }
                if (model.UserIconFile != null)
                {
                    string uniqueUserIcon = ProcessUploadedFile(model, CommonEnums.fileType.symbol);
                    model.User_icon = string.Format("/settings/img/{0}", uniqueUserIcon);
                }

                var json = System.IO.File.ReadAllText(filePath);

                var jObject = JObject.Parse(json);

                jObject["guid_app"] = model.Guid_app;
                jObject["brand_icon"] = model.Brand_icon;
                jObject["user_icon"] = model.User_icon;
                jObject["app_title"] = model.App_title;
                jObject["app_username"] = model.App_username;
                jObject["content_index"] = model.Content_index;
                jObject["footer_string"] = model.Footer_string;
                jObject["website_url"] = model.Website_url;
                jObject["facebook_url"] = model.Facebook_url;
                jObject["twitter_url"] = model.Twitter_url;
                jObject["instagram_url"] = model.Instagram_url;
                jObject["linkedin_url"] = model.Linkedin_url;
                jObject["youtube_channel"] = model.Youtube_channel;
                jObject["updated_at"] = model.Updated_at;

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(filePath, output);

                SwalAlert("Configuración de marca Guardado Correctamente !!!", Enums.CommonEnums.NotifyType.success);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string ProcessUploadedFile(BrandViewModel model, CommonEnums.fileType type)
        {
            string uniqueFileName = string.Empty;
            string filePath = string.Empty;
            string uploadsFolder = Path.Combine(environment.WebRootPath, "settings/img/");
            switch (type)
            {
                case CommonEnums.fileType.icon:
                    uniqueFileName = Guid.NewGuid().ToString() + "_brand_" + model.BrandIconFile.FileName;
                    filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.BrandIconFile.CopyTo(fileStream);
                    }
                    break;
                case CommonEnums.fileType.symbol:
                    uniqueFileName = Guid.NewGuid().ToString() + "_user_" + model.UserIconFile.FileName;
                    filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.UserIconFile.CopyTo(fileStream);
                    }
                    break;
            }

            return uniqueFileName;
        }

        private void OnLoadHeaderComponent()
        {
            var pFolder = Path.Combine(environment.WebRootPath, "settings/");
            string pfilePath = Path.Combine(pFolder, "");
            GetHeaderDataInfo(pfilePath);
        }
    }
}

using System.ComponentModel;

namespace WsPortfolioExpress.Web.Models
{
    public class BrandViewModel
    {
        [DisplayName("GUID")]
        public string? Guid_app { get; set; }  //"guid_app":  "",
        public string? Brand_icon { get; set; }  //"brand_icon": "",
        [DisplayName("Upload Brand Icon")]
        public IFormFile? BrandIconFile { get; set; }
        public string? User_icon { get; set; }  //"user_icon": "",
        [DisplayName("Upload User Icon")]
        public IFormFile? UserIconFile { get; set; }
        [DisplayName("Title")]
        public string? App_title { get; set; }  //"app_title": "",
        [DisplayName("User Name")]
        public string? App_username { get; set; } //"app_username": "",
        [DisplayName("Content")]
        public string? Content_index { get; set; }  //"content_index": "",
        [DisplayName("Footer Content")]
        public string? Footer_string { get; set; }  //"footer_string": "",
        [DisplayName("Web Site")]
        public string? Website_url { get; set; }  //"website_url": "",
        [DisplayName("Facebook")]
        public string? Facebook_url { get; set; }  //"facebook_url": "",
        [DisplayName("Twitter")]
        public string? Twitter_url { get; set; }   //"twitter_url": "",
        [DisplayName("Instagram")]
        public string? Instagram_url { get; set; }   //"instagram_url": "",
        [DisplayName("LinkedIn")]
        public string? Linkedin_url { get; set; }   //"linkedin_url": "",
        [DisplayName("Youtube Channel")]
        public string? Youtube_channel { get; set; }   //"youtube_channel": "",
        public string? Updated_at { get; set; }   //"updated_at":  ""
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace MusicSearch.Controllers
{
    public class VkLoginController : Controller
    {
        // GET: VkLogin
        public ActionResult Index(string code)
        {
            string accessToken = Request["access_token"];
            string expire = Request["expires_in"];
            string userid = Request["user_id"];
            Console.WriteLine("Login result : {0}, Expires in: {1}, User id: {2}", accessToken, expire, userid);

            int client_id = 5189124;
            string client_secret = "Aj2qJy2ZLGtkrVCjZaju";
            string redirectUri = "http://localhost:65171/VkLogin/";
           
            String url = String.Format("https://oauth.vk.com/access_token?client_id={0}&client_secret={1}&code={2}&redirect_uri={3}", client_id, client_secret, code, redirectUri);

            
    //        using (var client = new WebClient())
    //        {
    //            var values = new NameValueCollection
    //{
    //    { "client_id", "5189124" },
    //    { "client_secret", "Aj2qJy2ZLGtkrVCjZaju" },
    //    { "redirectUri", "http://localhost:65171/VkLogin/" },
    //};
    //            var result = client.UploadValues("https://oauth.vk.com/access_token", values);
    //            Console.WriteLine("");
    //        }
            return Redirect("http://localhost:65171/Home/Account");
        }

        public ActionResult GetTokenId(string access_token, int expires_in, string user_id)
        {
            System.Console.WriteLine("Login result : {0}, Expires in: {1}, User id: {2}", access_token, expires_in, user_id);

            return Redirect("/");
        }

    }
}
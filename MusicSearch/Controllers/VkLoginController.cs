using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MusicSearch.Models;

namespace MusicSearch.Controllers
{
    public class VkLoginController : Controller
    {
        // GET: VkLogin
        [HttpGet]
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
            var respUrl = VkHelpers.GetRequest(url);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            VKToken vkToken = serializer.Deserialize<VKToken>(respUrl);
            Response.Cookies["VkToken"]["AccessToken"] = vkToken.access_token;
            Response.Cookies["VkToken"]["ExpiresIn"] = vkToken.expires_in.ToString();
            Response.Cookies["VkToken"]["UserId"] = vkToken.user_id.ToString();
            string uInfo = String.Format("https://api.vk.com/method/users.get?user_ids={0}&fields={1}&name_case=nom", Request.Cookies["VkToken"]["UserId"], "first_name,last_name,city,photo_50");
            var userInfo = VkHelpers.GetRequest(uInfo);
            
            
            using(var db = new MusicSearchContext())
            {
                db.Users.Add(serializer.Deserialize<User>(userInfo));    
            }

            return Redirect("http://localhost:65171/Home/Account");
        }



        public static class VkHelpers
        {
            public static string GetRequest(string url)
            {
                WebRequest wr = WebRequest.Create(url);

                Stream objStream = wr.GetResponse().GetResponseStream();

                StreamReader objReader = new StreamReader(objStream);

                StringBuilder sb = new StringBuilder();
                string line = "";
                while (true)
                {
                    line = objReader.ReadLine();
                    if (line != null) sb.Append(line);

                    else
                    {

                        return sb.ToString();
                    }
                }
            }           

            public static string GetAccessTokenUrl(String app_id, String app_secret, String code)
            {
                return String.Format(@"https://oauth.vk.com/access_token?client_id={0}&client_secret={1}&code={2}", app_id, app_secret, code);
            }
        }


        
        public class VKToken
        {
            private string _access_token;
            
            public string access_token
            {
                get { return _access_token; }
                set { _access_token = value; }
            }

            private int _expires_in;
            
            public int expires_in
            {
                get { return _expires_in; }
                set { _expires_in = value; }
            }

            private int _user_id;

            
            public int user_id
            {
                get { return _user_id; }
                set { _user_id = value; }
            }

        }


        public enum VkResponseType
        {
            code,
            token
        }
        public enum VkDisplay
        {
            page,// – форма авторизации в отдельном окне
            popup,// – всплывающее окно
            touch,// – авторизация для мобильных Touch-устройств
            wap// – авторизация для мобильных устройств с маленьким экраном или без поддержки Javascript
        }
        public enum VkAuthSetting
        {
            notify,//	Пользователь разрешил отправлять ему уведомления.
            friends,//	Доступ к друзьям.
            photos,//	Доступ к фотографиям.
            audio,//	Доступ к аудиозаписям.
            video,//	Доступ к видеозаписям.
            docs,//	Доступ к документам.
            notes,//	Доступ заметкам пользователя.
            pages,//	Доступ к wiki-страницам.
            offers,//	Доступ к предложениям (устаревшие методы).
            questions,//	Доступ к вопросам (устаревшие методы).
            wall,//	Доступ к обычным и расширенным методам работы со стеной.
            groups,//	Доступ к группам пользователя.
            messages,//	(для Standalone-приложений) Доступ к расширенным методам работы с сообщениями.
            notifications,//	Доступ к оповещениям об ответах пользователю.
            ads,//	Доступ к расширенным методам работы с рекламным API.
            offline,//	Доступ к API в любое время со стороннего сервера.
            nohttps//	Возможность осуществлять запросы к API без HTTPS.
        }

        public class VkAuthSettingsBuilder
        {
            private SortedSet<string> value = new SortedSet<string>();
            public override string ToString()
            {
                return value.Aggregate((current, next) => current.ToString() + ", " + next.ToString());
            }

            public VkAuthSettingsBuilder Add(VkAuthSetting set)
            {
                value.Add(set.ToString());
                return this;
            }

            public static VkAuthSettingsBuilder Common()//fix
            {
                return new VkAuthSettingsBuilder().Add(VkAuthSetting.friends).Add(VkAuthSetting.wall);
            }
        }
        [HttpPost]
        public ActionResult Index(string access_token, int expires_in, string user_id)
        {
            System.Console.WriteLine("Login result : {0}, Expires in: {1}, User id: {2}", access_token, expires_in, user_id);

            return Redirect("/");
        }

    }
}
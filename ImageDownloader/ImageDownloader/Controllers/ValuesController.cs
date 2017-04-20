using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;

namespace ImageDownloader.Controllers
{

    public class ValuesController : ApiController
    {
        static List<string> images = new List<string>();


        public IHttpActionResult Post(string url)
        {
            string code = String.Empty;
            var task = Task.Run(() =>
            {
                using (var client = new HttpClient())
                {
                    if (url == null)
                        throw new ArgumentNullException();
                    WebClient wc = new WebClient();
                    var uri = new Uri(url);
                    code = wc.DownloadString(uri);
                }
            });
            task.Wait();
            string m = "<img .*? src=\"(.*?)\"/*?";
            MatchCollection matches = Regex.Matches(code, m, RegexOptions.Singleline);
            foreach (Match x in matches)
            {
                GroupCollection Group = x.Groups;
                var newimg = Group[1].Value.Trim();
                images.Add(newimg);
            }
            return Ok(images);


        }
    }
}


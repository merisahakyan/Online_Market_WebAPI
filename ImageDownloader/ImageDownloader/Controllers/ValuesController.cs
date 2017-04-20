using System;
using System.Collections.Generic;
using System.Drawing;
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
        public IHttpActionResult Post_Download(string imgurl, bool download )
        {
            if (download)
            {
                WebRequest requestPic = WebRequest.Create(imgurl);

                WebResponse responsePic = requestPic.GetResponse();

                Image webImage = Image.FromStream(responsePic.GetResponseStream());

                webImage.Save("C:\\Users\\Dell\\Desktop\\" + "Image" + ".jpg");
                return Ok("Image saved!");
            }
            else
                return
                    Ok("Image does'n save!");
        }
    }
}


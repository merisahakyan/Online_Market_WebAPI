using JSONPDemo.Controllers;
using System;
using System.Net.Http.Formatting;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace JSONPDemo
{
    internal class MyJSONPFormatter : MediaTypeFormatter
    {
        public MyJSONPFormatter()
        {
            this.SupportedMediaTypes.Add
                (new System.Net.Http.Headers.MediaTypeHeaderValue("application/javascript"));

        }
        public override bool CanReadType(Type type)
        {
            return false;
        }
        public override bool CanWriteType(Type type)
        {
            return type == (typeof(JSONPReturn));
        }
        public override Task WriteToStreamAsync(Type type, object value,
            Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            return Task.Factory.StartNew(() =>
            {
                var jsonp = (JSONPReturn)value;
                var sw = new StreamWriter(writeStream, UTF8Encoding.Default);
                sw.Write("{0}({1})", jsonp.Callback, jsonp.JSON);
                sw.Flush();
            });
        }
    }
}
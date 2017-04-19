using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;

namespace SelfHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost:4841/");
            var server = new HttpSelfHostServer(config,
                new MySingleHttpMessageHandler());
            var task = server.OpenAsync();
            task.Wait();

            Console.WriteLine("Server is up and runing");
            Console.ReadLine();
        }
    }
}

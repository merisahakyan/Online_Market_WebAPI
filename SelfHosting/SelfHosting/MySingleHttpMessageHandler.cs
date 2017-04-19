using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SelfHosting
{
    class MySingleHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine("received an http message");
            var task = new Task<HttpResponseMessage>(() =>
            {
                var msg = new HttpResponseMessage();
                msg.Content = new StringContent("Hello");
                return msg;
            });
            task.Start();
            return task;
        }
    }
}
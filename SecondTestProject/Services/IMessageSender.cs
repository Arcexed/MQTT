using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SecondTestProject
{
    public class MessageMiddleware
    {
        private RequestDelegate _next;
        public MessageMiddleware(RequestDelegate next)
        {
                
        }

        public async Task InvokeAsync(HttpContext context,MessageService service)
        {
            await context.Response.WriteAsync(service.SendMessage());
        }
    }
    public class MessageService
    {
        private IMessageSender _sender;
        public MessageService(IMessageSender sender)
        {
            _sender = sender;
        }

        public string SendMessage()
        {
            return _sender.Send();
        }
    }
    public interface IMessageSender
    {
        string Send();
    }

    public class EmailMessageSender : IMessageSender
    {
        public string Send()
        {
            return "Message is sent by email";
        }
    }

    public class SmsMessageSender : IMessageSender
    {
        public string Send()
        {
            return "Message is sent by sms";
        }
    }
}

﻿namespace SIS.WebServer.Results
{
    using System.Text;
    using HTTP.Enums;
    using HTTP.Headers;
    using HTTP.Responses;
    
    public class TextResult : HttpResponse
    {
        public TextResult(string content, HttpResponseStatusCode responseStatusCode) 
            : base(responseStatusCode)
        {
            this.Headers.Add(new HttpHeader(HttpHeader.ContentType, "text/plain; charset=utf-8"));
            this.Content = Encoding.UTF8.GetBytes(content);
        }

    }
}
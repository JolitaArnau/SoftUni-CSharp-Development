namespace SIS.WebServer.Results
{
    using System.Text;
    using HTTP.Responses;
    using HTTP.Enums;
    using HTTP.Headers;

    public class BadRequestResult : HttpResponse
    {
        private const string DefaultErrorHandling = "<h1>Error occured, see details</h1>";

        public BadRequestResult(string content, HttpResponseStatusCode responseStatusCode) : base(responseStatusCode)
        {
            content = DefaultErrorHandling + content;

            this.Headers.Add(new HttpHeader(HttpHeader.ContentType, "text/html"));
            this.Content = Encoding.UTF8.GetBytes(content);
        }
    }
}
using SIS.HTTP.Sessions;

namespace SIS.HTTP.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Contracts;
    using Enums;
    using Exceptions;
    using Headers;
    using Cookies;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requesString)
        {
            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();

            this.ParseRequest(requesString);
        }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        public IHttpCookieCollection Cookies { get; }
        
        public IHttpSession Session { get; set; }

        //public HttpSessionStorage SessionStorage { get; set; }

        private void ParseRequest(string requesString)
        {
            string[] splitRequestContent = requesString.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

            string[] requestLine =
                splitRequestContent[0].Trim().Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);

            if (!this.IsValidRequestLine(requestLine))
            {
                throw new BadRequestException();
            }

            this.ParseRequestMethod(requestLine);
            this.ParseRequestUrl(requestLine);
            this.ParseRequestPath(requestLine);

            this.ParseHeaders(splitRequestContent.Skip(1).ToArray());
            this.ParseCookies();

            this.ParseRequestParameters(splitRequestContent);
        }

        private void ParseCookies()
        {
            if (!this.Headers.ContainsHeader("Cookie"))
            {
                return;
            }

            var cookiesRow = this.Headers.GetHeader("Cookie").Value;
            var cookies = cookiesRow.Split("; ", StringSplitOptions.RemoveEmptyEntries);

            foreach (var rawCookie in cookies)
            {
                var cookieKvp = rawCookie.Split("=", 2);
                if (cookieKvp.Length != 2)
                {
                    throw new BadRequestException();
                }

                var cookieName = cookieKvp[0];
                var cookieValue = cookieKvp[1];
                
                this.Cookies.Add(new HttpCookie(cookieName, cookieValue));
            }
        }

        private void ParseRequestParameters(string[] splitRequestContent)
        {
            ParseFormDataParameters(splitRequestContent[splitRequestContent.Length - 1]);

            ParseQueryParameters(this.Url);
        }

        private void ParseFormDataParameters(string requestContentLastRow)
        {
            if (string.IsNullOrWhiteSpace(requestContentLastRow))
            {
                return;
            }

            ParseDataParameters(requestContentLastRow, this.FormData);
        }

        private void ParseQueryParameters(string url)
        {
            if (!url.Contains("?"))
            {
                return;
            }

            string[] urlTokens = url.Split(new[] {"?", "#"}, StringSplitOptions.RemoveEmptyEntries);

            ParseDataParameters(urlTokens[1], this.QueryData);
        }

        private void ParseDataParameters(string queryParametersAsString, Dictionary<string, object> dictRequestData)
        {
            string[] queryParameters =
                queryParametersAsString.Split(new[] {"&"}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var queryParameter in queryParameters)
            {
                string[] splitParameters = queryParameter.Split(new[] {"="}, StringSplitOptions.RemoveEmptyEntries);

                if (splitParameters.Length != 2)
                {
                    throw new BadRequestException();
                }

                string key = splitParameters[0];
                string value = splitParameters[1];

                dictRequestData.Add(key, value);
            }
        }

        private void ParseHeaders(string[] requestHeaders)
        {
            foreach (var requestLine in requestHeaders)
            {
                string[] splitKvp = requestLine.Split(new[] {": "}, StringSplitOptions.RemoveEmptyEntries);

                if (string.IsNullOrWhiteSpace(requestLine))
                {
                    break;
                }

                if (splitKvp.Length != 2)
                {
                    throw new BadRequestException();
                }

                string key = splitKvp[0];
                string value = splitKvp[1];
                var header = new HttpHeader(key, value);

                this.Headers.Add(header);
            }

            if (!this.Headers.ContainsHeader(GlobalConstants.HostHeaderKey))
            {
                throw new BadRequestException();
            }
        }

        private void ParseRequestPath(string[] requestLine)
        {
            this.Path = requestLine[1].Split(new[] {"?"}, StringSplitOptions.None).First();
        }

        private void ParseRequestUrl(string[] requestLine)
        {
            this.Url = requestLine[1];
        }

        private void ParseRequestMethod(string[] requestLine)
        {
            bool isValidMetod =
                Enum.TryParse<HttpRequestMethod>(requestLine[0], out HttpRequestMethod httpRequestMethod);

            if (!isValidMetod)
            {
                throw new BadRequestException();
            }

            this.RequestMethod = httpRequestMethod;
        }

        private bool IsValidRequestLine(string[] requestLine)
        {
            if (requestLine.Length != 3 || requestLine[2] != GlobalConstants.HttpOneProtocolFragment)
            {
                return false;
            }

            return true;
        }
    }
}
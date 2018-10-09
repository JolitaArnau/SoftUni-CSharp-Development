using SIS.HTTP.Sessions;

namespace SIS.HTTP.Contracts
{
    using System.Collections.Generic;
    using Enums;
    
    public interface IHttpRequest
    {
        string Path { get; }

        string Url { get; }
        
        Dictionary<string, object> FormData { get; }
        
        Dictionary<string, object> QueryData { get; }
        
        IHttpHeaderCollection Headers { get; }

        HttpRequestMethod RequestMethod { get; }

        IHttpCookieCollection Cookies { get; }

        IHttpSession Session { get; set; }
    }
}
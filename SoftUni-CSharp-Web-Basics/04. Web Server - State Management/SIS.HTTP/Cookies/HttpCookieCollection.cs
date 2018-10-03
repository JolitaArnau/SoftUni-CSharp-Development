namespace SIS.HTTP.Cookies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;

    public class HttpCookieCollection : IHttpCookieCollection
    {
        private readonly IDictionary<string, HttpCookie> cookies;

        public HttpCookieCollection()
        {
            this.cookies = new Dictionary<string, HttpCookie>();
        }

        public void Add(HttpCookie cookie)
        {
            if (cookie == null)
            {
                throw new ArgumentNullException();
            }

            //TODO: overwrite cookies?

//            if (this.ContainsCookie(cookie.Key))
//            {
//                throw new Exception();   
//            }

            this.cookies.Add(cookie.Key, cookie);
        }

        public bool ContainsCookie(string key)
        {
            return this.cookies.ContainsKey(key);
        }

        public HttpCookie GetCookie(string key)
        {
            if (!this.ContainsCookie(key))
            {
                return null;
            }

            return this.cookies[key];
        }

        public bool HasCookies() => this.cookies.Any();

        public override string ToString()
        {
            return string.Join("; ", this.cookies.Values);
        }
    }
}
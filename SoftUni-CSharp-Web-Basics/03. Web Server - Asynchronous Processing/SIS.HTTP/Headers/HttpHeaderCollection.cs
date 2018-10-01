namespace SIS.HTTP.Headers
{
    using System;
    using System.Collections.Generic;
    
    using Contracts;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public void Add(HttpHeader header)
        {
            var headerKey = header.Key;

            if (header == null || string.IsNullOrEmpty(header.Key) || string.IsNullOrEmpty(header.Value))
            {
                throw new Exception();
            }

            this.headers.Add(headerKey, header);
        }

        public bool ContainsHeader(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException($"{nameof(key)} cannot be null");
            }

            return this.headers.ContainsKey(key);
        }

        public HttpHeader GetHeader(string key)
        {
            if (!headers.ContainsKey(key))
            {
                return null;
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException();
            }

            return headers[key];
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.headers);
        }
    }
}
﻿namespace SIS.WebServer
{
    using System;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    using HTTP.Cookies;
    using HTTP.Requests;
    using HTTP.Responses;
    using HTTP.Sessions;
    using Api.Contracts;

    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IHttpHandler httpHandler;

        public ConnectionHandler(Socket client, IHttpHandler httpHandler)
        {
            this.client = client;
            this.httpHandler = httpHandler;
        }

        private async Task<IHttpRequest> ReadRequest()
        {
            var result = new StringBuilder();

            var data = new ArraySegment<byte>(new byte[1024]);

            while (true)
            {
                int numberOfBytesRead = await this.client.ReceiveAsync(data.Array, SocketFlags.None);

                if (numberOfBytesRead == 0)
                {
                    break;
                }

                var bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesRead);
                result.Append(bytesAsString);

                if (numberOfBytesRead < 1023)
                {
                    break;
                }
            }

            if (result.Length == 0)
            {
                return null;
            }

            return new HttpRequest(result.ToString());
        }


        private async Task PrepareResponse(IHttpResponse httpResponse)
        {
            byte[] byteSegments = httpResponse.GetBytes();

            await this.client.SendAsync(byteSegments, SocketFlags.None);
        }

        public async Task ProcessRequestAsync()
        {
            var httpRequest = await this.ReadRequest();

            if (httpRequest != null)
            {
                var isNewSession = !httpRequest.Cookies.ContainsCookie(HttpSessionStorage.SessionCookieKey);

                var sessionId = this.SetRequestSession(httpRequest);

                var httpResponse = this.httpHandler.Handle(httpRequest);

                this.SetResponseSession(httpResponse, sessionId, isNewSession);

                await this.PrepareResponse(httpResponse);
            }

            this.client.Shutdown(SocketShutdown.Both);
        }

        private string SetRequestSession(IHttpRequest httpRequest)
        {
            string sessionId = null;

            if (httpRequest.Cookies.ContainsCookie(HttpSessionStorage.SessionCookieKey))
            {
                var cookie = httpRequest.Cookies.GetCookie(HttpSessionStorage.SessionCookieKey);
                sessionId = cookie.Value;
                httpRequest.Session = HttpSessionStorage.GetSession(sessionId);
            }
            else
            {
                sessionId = Guid.NewGuid().ToString();
                httpRequest.Session = HttpSessionStorage.GetSession(sessionId);
            }

            return sessionId;
        }

        private void SetResponseSession(IHttpResponse httpResponse, string sessionId, bool isNewSession)
        {
            if (isNewSession)
            {
                httpResponse.AddCookie(new HttpCookie(HttpSessionStorage.SessionCookieKey, $"{sessionId}"));
            }
        }
    }
}
namespace SIS.Framework
{
    using System.IO;
    using System.Linq;
    using HTTP.Common;
    using HTTP.Enums;
    using HTTP.Requests;
    using HTTP.Responses;
    using WebServer.Api.Contracts;
    using WebServer.Results;
    using WebServer.Routing;
    
    public class HttpHandler : IHttpHandler
    {
        private ServerRoutingTable serverRoutingTable;
        
        public HttpHandler(ServerRoutingTable routingTable)
        {
            this.serverRoutingTable = routingTable;
        }
        
        public IHttpResponse Handle(IHttpRequest httpRequest)
        {
            var isResourceRequest = this.IsResourceRequest(httpRequest);
            if (isResourceRequest)
            {
                return this.HandleRequestResponse(httpRequest.Path);
            }
            if (!this.serverRoutingTable.Routes.ContainsKey(httpRequest.RequestMethod)
                || !this.serverRoutingTable.Routes[httpRequest.RequestMethod].ContainsKey(httpRequest.Path.ToLower()))
            {
                return new HttpResponse(HttpResponseStatusCode.NotFound);
            }

            return this.serverRoutingTable.Routes[httpRequest.RequestMethod][httpRequest.Path].Invoke(httpRequest);
        }
        
        private IHttpResponse HandleRequestResponse(string httpRequestPath)
        {
            var indexOfStartOfExtension = httpRequestPath.LastIndexOf('.');
            var indexOfStartOfNameOfResource = httpRequestPath.LastIndexOf('/');

            var requestPathExtension = httpRequestPath
                .Substring(indexOfStartOfExtension);

            var resourceName = httpRequestPath
                .Substring(
                    indexOfStartOfNameOfResource);

            var resourcePath = 
                               "/Resources"
                               + $"/{requestPathExtension.Substring(1)}"
                               + resourceName;

            if (!File.Exists(resourcePath))
            {
                return new HttpResponse(HttpResponseStatusCode.NotFound);
            }

            var fileContent = File.ReadAllBytes(resourcePath);

            return new InlineResourceResult(fileContent, HttpResponseStatusCode.Ok);
        }
        
        private bool IsResourceRequest(IHttpRequest httpRequest)
        {
            var requestPath = httpRequest.Path;
            if (requestPath.Contains('.'))
            {
                var requestPathExtension = requestPath
                    .Substring(requestPath.LastIndexOf('.'));
                return GlobalConstants.ResourceExtensions.Contains(requestPathExtension);
            }
            return false;
        }
    }
}
namespace SIS.WebServer.Api.Contracts
{
    using HTTP.Requests;
    using HTTP.Responses;

    public interface IHttpHandler
    {
        IHttpResponse Handle(IHttpRequest request);
    }
}
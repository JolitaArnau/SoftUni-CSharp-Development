namespace SIS.Framework.Attributes.Methods
{
    using Base;

    public class HttpGetAttribute : HttpMethodAttribute
    {
        public HttpGetAttribute(string path)
        {
            
        }
        
        public override bool IsValid(string requestMethod)
        {
            if (requestMethod.ToLower() == "get")
            {
                return true;
            }
            
            return false;
        }
    }
}
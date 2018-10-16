using SIS.Framework.Attributes.Methods.Base;
using SIS.HTTP.Enums;

namespace SIS.Framework.Attributes.Methods
{
    public class HttpDeleteAttribute : HttpMethodAttribute
    {
        public override bool IsValid(string requestMethod)
        {
            if (requestMethod.ToLower() == "delete")
            {
                return true;
            }
            
            return false;
        }
    }
}
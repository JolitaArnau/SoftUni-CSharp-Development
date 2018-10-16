namespace SIS.Framework.Routers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using ActionsResults.Base;
    using Attributes.Methods.Base;
    using Controllers;
    using HTTP.Requests;
    using HTTP.Responses;
    using WebServer.Api.Contracts;
    using ActionsResults.Contracts;
    using HTTP.Enums;
    using WebServer.Results;

    
    public class ControllerRouter : IHttpHandler
    {
        public IHttpResponse Handle(IHttpRequest request)
        {
            var controllerName = string.Empty;
            var actionName = string.Empty;
            var requestMethod = request.RequestMethod.ToString();

            if (request.Url == "/")
            {
                controllerName = "HomeController";
                actionName = "Index";
            }
            else
            {
                var requestUrlSplit = request.Url.Split("/", StringSplitOptions.RemoveEmptyEntries);

                if (requestUrlSplit.Length >= 2)
                {
                    controllerName = requestUrlSplit[0];
                    actionName = requestUrlSplit[1];
                }
            }

            var controller = this.GetController(controllerName, request);
            controller.Request = request;

            var action = this.GetAction(requestMethod, controller, actionName);

            if (action == null)
            {
                return new HttpResponse(HttpResponseStatusCode.NotFound);
            }

            return this.PrepareResponse(controller, action);
        }

        private IHttpResponse PrepareResponse(Controller controller, MethodInfo action)
        {
            var actionResult = (IActionResult)action.Invoke(controller, null);
            var invocationResult = actionResult.Invoke();

            if (actionResult is IViewable)
            {
                return new HtmlResult(invocationResult, HttpResponseStatusCode.Ok);
            }

            if (actionResult is IRenderable)
            {
                return new RedirectResult(invocationResult);
            }
            
            throw new InvalidOperationException("The view result is not supported.");

        }

        private MethodInfo GetAction(
            string requestMethod,
            Controller controller,
            string actionName)
        {
            var actions = this.GetSuitableMethods(controller, actionName) .ToList();

            if (!actions.Any())
            {
                return null;
            }

            foreach (var action in actions)
            {
                var httpMethodAttributes = action.GetCustomAttributes()
                    .Where(ca => ca is HttpMethodAttribute)
                    .Cast<HttpMethodAttribute>()
                    .ToList();

                if (!httpMethodAttributes.Any() && requestMethod.ToLower() == "get")
                {
                    return action;
                }

                foreach (var httpMethodAttribute in httpMethodAttributes)
                {
                    if (httpMethodAttribute.IsValid(requestMethod))
                    {
                        return action;
                    }
                }
            }

            return null;
        }

        private IEnumerable<MethodInfo> GetSuitableMethods(
            Controller controller,
            string actionName)
        {
            if (controller == null)
            {
                return new MethodInfo[0];
            }

            return controller
                .GetType()
                .GetMethods()
                .Where(mi => mi.Name.ToLower() == actionName.ToLower());
        }

        private Controller GetController(string controllerName, IHttpRequest request)
        {
            if (string.IsNullOrWhiteSpace(controllerName))
            {
                return null;
            }

            var fullyQualifiedControllerName = string.Format("{0}.{1}.{2}{3}, {0}",
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ControllersFolder,
                controllerName,
                MvcContext.Get.ControllerSuffix);
            
            var controllerType = Type.GetType(fullyQualifiedControllerName);
            var controller = (Controller) Activator.CreateInstance(controllerType);
            return controller;
        }
    }
}
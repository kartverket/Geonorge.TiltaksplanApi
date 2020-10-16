using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;
using Geonorge.TiltaksplanApi.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Geonorge.TiltaksplanApi.Web
{
    public class ActionPlanUrlProvider : IUrlProvider
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IUrlHelperFactory _urlHelperFactory;

        public ActionPlanUrlProvider(
            IActionContextAccessor actionContextAccessor,
            IUrlHelperFactory urlHelperFactory)
        {
            _actionContextAccessor = actionContextAccessor;
            _urlHelperFactory = urlHelperFactory;
        }
    
        public ExpandoObject ApiUrls()
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);

            dynamic apiUrls = new ExpandoObject();

            apiUrls.actionPlan = new ExpandoObject();
            apiUrls.actionPlan.get = GetControllerUrl(urlHelper, "GetById", "ActionPlan", new { id = 0 });
            apiUrls.actionPlan.getAll = GetControllerUrl(urlHelper, "GetAll", "ActionPlan");
            apiUrls.actionPlan.create = GetControllerUrl(urlHelper, "Create", "ActionPlan");
            apiUrls.actionPlan.update = GetControllerUrl(urlHelper, "Update", "ActionPlan", new { id = 0 });
            apiUrls.actionPlan.delete = GetControllerUrl(urlHelper, "Delete", "ActionPlan", new { id = 0 });

            return apiUrls;
        }

        private string GetControllerUrl(IUrlHelper urlHelper, string action, string controller, object values = null)
        {
            var url = urlHelper.Action(new UrlActionContext
            {
                Action = action,
                Controller = controller,
                Values = values,
                Protocol = GetProtocol(),
                Host = GetHost()
            });

            var uri = new Uri(url);
            var parameters = GetParameters(values);
            var counter = 0;
            var localPath = Regex.Replace(uri.LocalPath, @"\d+", m => $"{{{parameters[counter++]}}}", RegexOptions.IgnoreCase);

            return $"{GetProtocol()}://{GetHost()}{localPath}";
        }

        private static List<string> GetParameters(object values)
        {
            return values != null ?
                values.GetType().GetProperties()
                    .Select(prop => prop.Name)
                    .Select(propName => char.ToLowerInvariant(propName[0]) + propName.Substring(1))
                    .ToList() :
                new List<string>();
        }

        private string GetProtocol()
        {
            return _actionContextAccessor.ActionContext.HttpContext.Request.Scheme;
        }

        private string GetHost()
        {
            return _actionContextAccessor.ActionContext.HttpContext.Request.Host.Value;
        }
    }
}

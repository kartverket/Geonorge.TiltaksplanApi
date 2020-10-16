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

            return apiUrls;
        }

        private string GetControllerUrl(IUrlHelper urlHelper, string action, string controller, object values = null)
        {
            var hasValues = values != null;
            var names = new List<string>();

            if (hasValues)
            {
                names.AddRange(values.GetType().GetProperties()
                    .Select(prop => prop.Name)
                    .Select(propName => char.ToLowerInvariant(propName[0]) + propName.Substring(1))
                );
            }

            var url = urlHelper.Action(new UrlActionContext
            {
                Action = action,
                Controller = controller,
                Values = values,
                Protocol = GetProtocol(),
                Host = GetHost()
            });

            var uri = new Uri(url);
            var counter = 0;

            return Regex.Replace(uri.LocalPath, @"\d+", m => $"{{{names[counter++]}}}", RegexOptions.IgnoreCase);
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

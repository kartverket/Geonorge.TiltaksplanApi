﻿using System;
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
    public class MeasurePlanUrlProvider : IUrlProvider
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IUrlHelperFactory _urlHelperFactory;

        public MeasurePlanUrlProvider(
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

            apiUrls.measure = new ExpandoObject();
            apiUrls.measure.get = GetControllerUrl(urlHelper, "GetById", "Measure", new { id = 0, culture = "" });
            apiUrls.measure.getAll = GetControllerUrl(urlHelper, "GetAll", "Measure");
            apiUrls.measure.create = GetControllerUrl(urlHelper, "Create", "Measure");
            apiUrls.measure.update = GetControllerUrl(urlHelper, "Update", "Measure", new { id = 0 });
            apiUrls.measure.delete = GetControllerUrl(urlHelper, "Delete", "Measure", new { id = 0 });

            apiUrls.activity = new ExpandoObject();
            apiUrls.activity.get = GetControllerUrl(urlHelper, "GetById", "Activity", new { id = 0 });
            apiUrls.activity.getAll = GetControllerUrl(urlHelper, "GetAll", "Activity");
            apiUrls.activity.create = GetControllerUrl(urlHelper, "Create", "Activity");
            apiUrls.activity.update = GetControllerUrl(urlHelper, "Update", "Activity", new { id = 0 });
            apiUrls.activity.delete = GetControllerUrl(urlHelper, "Delete", "Activity", new { id = 0 });

            apiUrls.participant = new ExpandoObject();
            apiUrls.participant.getAll = GetControllerUrl(urlHelper, "GetAll", "Participant");

            apiUrls.config = new ExpandoObject();
            apiUrls.config.get = GetControllerUrl(urlHelper, "Get", "Config");

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
                    .Select(prop => char.ToLowerInvariant(prop.Name[0]) + prop.Name.Substring(1))
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
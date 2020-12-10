using System.Dynamic;

namespace Geonorge.TiltaksplanApi.Application
{
    public interface IUrlProvider
    { 
        ExpandoObject ApiUrls();
    }
}

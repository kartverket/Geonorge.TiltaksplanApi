@echo off

dotnet ef migrations add %1  --startup-project ..\src\Geonorge.TiltaksplanApi.Web --project ..\src\Geonorge.TiltaksplanApi.Infrastructure

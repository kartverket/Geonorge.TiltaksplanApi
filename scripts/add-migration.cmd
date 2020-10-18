@echo off

set /p name="Enter migration name: "
dotnet ef migrations add %name%  --startup-project ..\src\Geonorge.TiltaksplanApi.Web --project ..\src\Geonorge.TiltaksplanApi.Infrastructure

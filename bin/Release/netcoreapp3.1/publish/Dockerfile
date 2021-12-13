FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app

CMD ASPNETCORE_URLS=http://*:$PORT dotnet dichothue.dll

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
ENTRYPOINT ["dotnet", "DiChoThue.dll"]
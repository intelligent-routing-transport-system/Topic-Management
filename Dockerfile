FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
LABEL version="1.0" maintainer="TCC"
WORKDIR /app
COPY ./dist .
ENV ASPNETCORE_URLS=http://+:5005
EXPOSE 5005
ENTRYPOINT ["dotnet", "Topic-Manager.dll"]
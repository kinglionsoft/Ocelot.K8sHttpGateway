FROM microsoft/dotnet:2.2.0-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT=Production 
ENV ASPNETCORE_URLS="http://*:80"

COPY out/ .
ENTRYPOINT ["dotnet", "Ocelot.K8sHttpGateway.dll"]
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Ocelot.K8S/Ocelot.K8S.csproj", "Ocelot.K8S/"]
RUN dotnet restore "Ocelot.K8S/Ocelot.K8S.csproj"
COPY . .
WORKDIR "/src/Ocelot.K8S"
RUN dotnet build "Ocelot.K8S.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Ocelot.K8S.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Ocelot.K8S.dll"]
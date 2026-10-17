FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
# Projenin gerçek klasör yolunu gösteriyoruz:
COPY ["ApiGateway/manav.OcelotGateway/manav.OcelotGateway.csproj", "ApiGateway/manav.OcelotGateway/"]
RUN dotnet restore "ApiGateway/manav.OcelotGateway/manav.OcelotGateway.csproj"

COPY . .
WORKDIR "/src/ApiGateway/manav.OcelotGateway"
RUN dotnet build "manav.OcelotGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "manav.OcelotGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "manav.OcelotGateway.dll"]
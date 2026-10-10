FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Frontend/manav.webUI/manav.webUI.csproj", "Frontend/manav.webUI/"]
COPY ["Frontend/manav.DtoLayer/manav.DtoLayer.csproj", "Frontend/manav.DtoLayer/"]
RUN dotnet restore "Frontend/manav.webUI/manav.webUI.csproj"
COPY . .
WORKDIR "/src/Frontend/manav.webUI"
RUN dotnet build "./manav.webUI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./manav.webUI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "manav.webUI.dll"]

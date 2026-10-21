FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Cargo/manav.cargo.webApi/manav.cargo.webApi.csproj", "Services/Cargo/manav.cargo.webApi/"]
COPY ["Services/Cargo/manav.cargo.businessLayer/manav.cargo.businessLayer.csproj", "Services/Cargo/manav.cargo.businessLayer/"]
COPY ["Services/Cargo/manav.cargo.dataAccessLayer/manav.cargo.dataAccessLayer.csproj", "Services/Cargo/manav.cargo.dataAccessLayer/"]
COPY ["Services/Cargo/manav.cargo.entityLayer/manav.cargo.entityLayer.csproj", "Services/Cargo/manav.cargo.entityLayer/"]
COPY ["Services/Cargo/manav.cargo.dtoLayer/manav.cargo.dtoLayer.csproj", "Services/Cargo/manav.cargo.dtoLayer/"]
RUN dotnet restore "Services/Cargo/manav.cargo.webApi/manav.cargo.webApi.csproj"
COPY . .
WORKDIR "/src/Services/Cargo/manav.cargo.webApi"
RUN dotnet build "./manav.cargo.webApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./manav.cargo.webApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "manav.cargo.webApi.dll"]

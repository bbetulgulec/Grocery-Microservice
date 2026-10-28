FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Order/Presentation/manav.order.webApi/manav.order.webApi.csproj", "Services/Order/Presentation/manav.order.webApi/"]
COPY ["Services/Order/Core/manav.order.application/manav.order.application.csproj", "Services/Order/Core/manav.order.application/"]
COPY ["Services/Order/Core/manav.order.domain/manav.order.domain.csproj", "Services/Order/Core/manav.order.domain/"]
COPY ["Services/Order/Infrastructure/manav.order.persstance/manav.order.persstance.csproj", "Services/Order/Infrastructure/manav.order.persstance/"]
RUN dotnet restore "Services/Order/Presentation/manav.order.webApi/manav.order.webApi.csproj"
COPY . .
WORKDIR "/src/Services/Order/Presentation/manav.order.webApi"
RUN dotnet build "./manav.order.webApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./manav.order.webApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "manav.order.webApi.dll"]

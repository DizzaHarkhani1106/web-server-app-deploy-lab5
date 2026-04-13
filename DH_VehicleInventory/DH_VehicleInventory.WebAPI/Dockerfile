# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DH_VehicleInventory/DH_VehicleInventory.WebAPI/DH_VehicleInventory.WebAPI.csproj", "DH_VehicleInventory/DH_VehicleInventory.WebAPI/"]
COPY ["DH_VehicleInventory/DH_VehicleInventory.Application/DH_VehicleInventory.Application.csproj", "DH_VehicleInventory/DH_VehicleInventory.Application/"]
COPY ["DH_VehicleInventory/DH_VehicleInventory.Domain/DH_VehicleInventory.Domain.csproj", "DH_VehicleInventory/DH_VehicleInventory.Domain/"]
COPY ["DH_VehicleInventory/DH_VehicleInventory.Infrastructure/DH_VehicleInventory.Infrastructure.csproj", "DH_VehicleInventory/DH_VehicleInventory.Infrastructure/"]
RUN dotnet restore "./DH_VehicleInventory/DH_VehicleInventory.WebAPI/DH_VehicleInventory.WebAPI.csproj"
COPY . .
WORKDIR "/src/DH_VehicleInventory/DH_VehicleInventory.WebAPI"
RUN dotnet build "./DH_VehicleInventory.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build
# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DH_VehicleInventory.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false /p:ErrorOnDuplicatePublishOutputFiles=false
# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DH_VehicleInventory.WebAPI.dll"]
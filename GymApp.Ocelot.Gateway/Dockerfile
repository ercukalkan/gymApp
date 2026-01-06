# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /

# Copy the solution file
COPY GymApp.sln .

# Copy project files
COPY GymApp.Ocelot.Gateway/ GymApp.Ocelot.Gateway/
COPY Shared/GymApp.Shared/ Shared/GymApp.Shared/
COPY src/Services/GymTrackingService/ src/Services/GymTrackingService/
COPY src/Services/NutritionService/ src/Services/NutritionService/
COPY src/Services/InventoryService/ src/Services/InventoryService/

# Restore dependencies
RUN dotnet restore GymApp.sln

# Build the project
RUN dotnet build GymApp.Ocelot.Gateway/OcelotGateway.csproj -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish GymApp.Ocelot.Gateway/OcelotGateway.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "OcelotGateway.dll"]

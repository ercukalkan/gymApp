FROM mcr.microsoft.com/dotnet/sdk:9.0 AS base
WORKDIR /src
COPY . .
RUN dotnet restore "src/Services/NutritionService/GymApp.NutritionService.API/GymApp.NutritionService.API.csproj"
RUN dotnet publish "src/Services/NutritionService/GymApp.NutritionService.API/GymApp.NutritionService.API.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=base /app/publish .
ENTRYPOINT ["dotnet", "GymApp.NutritionService.API.dll"]
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
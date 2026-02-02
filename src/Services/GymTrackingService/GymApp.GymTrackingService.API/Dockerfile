FROM mcr.microsoft.com/dotnet/sdk:9.0 AS base
WORKDIR /src
COPY . .
RUN dotnet restore "src/Services/GymTrackingService/GymApp.GymTrackingService.API/GymApp.GymTrackingService.API.csproj"
RUN dotnet publish "src/Services/GymTrackingService/GymApp.GymTrackingService.API/GymApp.GymTrackingService.API.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=base /app/publish .
ENTRYPOINT ["dotnet", "GymApp.GymTrackingService.API.dll"]
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EmployeeMotivationSystem.API/EmployeeMotivationSystem.API.csproj", "EmployeeMotivationSystem.API/"]
COPY ["EmployeeMotivationSystem.DAL/EmployeeMotivationSystem.DAL.csproj", "EmployeeMotivationSystem.DAL/"]
RUN dotnet restore "EmployeeMotivationSystem.API/EmployeeMotivationSystem.API.csproj"
COPY . .
WORKDIR "/src/EmployeeMotivationSystem.API"
RUN dotnet build "EmployeeMotivationSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

#WORKDIR "/src/EmployeeMotivationSystem.DAL"
#RUN dotnet tool install --global dotnet-ef
#RUN dotnet ef

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EmployeeMotivationSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "EmployeeMotivationSystem.API.dll"]


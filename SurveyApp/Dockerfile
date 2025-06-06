# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
# For more information, please see https://aka.ms/containercompat

# This stage is used when running from VS in fast mode (Default for Debug configuration)
# FROM mcr.microsoft.com/dotnet/aspnet:8.0-windowsservercore-ltsc2019 AS base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER ContainerUser
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
# FROM mcr.microsoft.com/dotnet/sdk:8.0-windowsservercore-ltsc2019 AS build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# copy csproj and restore as distinct layers
COPY ["SurveyApp.csproj", "./SurveyApp/"]
RUN dotnet restore "./SurveyApp/SurveyApp.csproj"

# copy everything else and build app
COPY . ./SurveyApp/
WORKDIR "/src/SurveyApp"
RUN dotnet build "./SurveyApp.csproj" -c %BUILD_CONFIGURATION% -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SurveyApp.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false --no-restore

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SurveyApp.dll"]

# Use ASP.NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Use .NET SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy and restore only the project file first to use Docker caching
COPY ["CustomerApi/CustomerApi.csproj", "CustomerApi/"]
RUN dotnet restore "CustomerApi/CustomerApi.csproj"

# Copy the remaining files and build the app
COPY . .
WORKDIR "/src/CustomerApi"
RUN dotnet build "CustomerApi.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "CustomerApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerApi.dll"]

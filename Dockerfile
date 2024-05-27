# Use the official ASP.NET Core image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8000

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DocApp.csproj", "./"]
RUN dotnet restore "DocApp.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DocApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DocApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage: Run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DocApp.dll"]
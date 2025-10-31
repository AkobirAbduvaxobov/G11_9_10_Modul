# 1. Build bosqichi
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# .csproj faylni ko‘chir va restore qil
COPY *.csproj ./
RUN dotnet restore

# Barcha fayllarni ko‘chir va publish qil
COPY . ./
RUN dotnet publish -c Release -o out

# 2. Runtime bosqichi
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Agar background service bo‘lsa, 80-port optional
EXPOSE 8080

ENTRYPOINT ["dotnet", "RabbitMQListener.Api.dll"]

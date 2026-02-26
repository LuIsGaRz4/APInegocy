# Base SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar sólo el proyecto
COPY APInegocy/APInegocy.csproj APInegocy/
# Restaurar dependencias desde ese proyecto
RUN dotnet restore APInegocy/APInegocy.csproj

# Copiar todo el código
COPY . .

# Publicar el proyecto
RUN dotnet publish APInegocy/APInegocy.csproj -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar la salida publicada desde la etapa de build
COPY --from=build /app/publish .

# Puerto que se usará
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

ENTRYPOINT ["dotnet", "APInegocy.dll"]
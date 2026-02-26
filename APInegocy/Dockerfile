# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia csproj y restaura dependencias
COPY *.csproj ./
RUN dotnet restore

# Copia todo el proyecto y publica
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Puerto que Render asignará
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

# Comando para ejecutar la API
ENTRYPOINT ["dotnet", "APInegocy.dll"]
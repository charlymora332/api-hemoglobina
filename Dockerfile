# ============================
# Etapa de build
# ============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj y restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar todo el proyecto y compilar en modo Release
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# ============================
# Etapa de runtime
# ============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar la publicación del build
COPY --from=build /app/publish ./

# Ejecutar la API
ENTRYPOINT ["dotnet", "RetoHemoglobina.dll"]

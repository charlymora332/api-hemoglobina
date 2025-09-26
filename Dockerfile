# ============================
# Etapa de build
# ============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj y restaurar dependencias
COPY RetoHemoglobina/*.csproj ./RetoHemoglobina/
RUN dotnet restore ./RetoHemoglobina/RetoHemoglobina.csproj

# Copiar todo y compilar en modo Release
COPY . .
WORKDIR /src/RetoHemoglobina
RUN dotnet publish -c Release -o /app/publish

# ============================
# Etapa de runtime
# ============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "RetoHemoglobina.dll"]

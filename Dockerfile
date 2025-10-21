# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar la solución y todos los proyectos
COPY *.sln ./
COPY RetoHemoglobina.Api/*.csproj ./RetoHemoglobina.Api/
COPY RetoHemoglobina.Application/*.csproj ./RetoHemoglobina.Application/
COPY RetoHemoglobina.Domain/*.csproj ./RetoHemoglobina.Domain/

# Restaurar paquetes NuGet de la solución
RUN dotnet restore RetoHemoglobina.sln

# Copiar todo el código
COPY . ./

# Publicar el proyecto Api
WORKDIR /src/RetoHemoglobina.Api
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "RetoHemoglobina.Api.dll"]

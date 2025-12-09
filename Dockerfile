# ---------- BUILD STAGE ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Proje dosyasını kopyala ve restore et
COPY Lab.Microservice.csproj ./
RUN dotnet restore Lab.Microservice.csproj

# Tüm kaynak kodunu kopyala
COPY . ./

# Release build al
RUN dotnet publish Lab.Microservice.csproj -c Release -o /app/publish

# ---------- RUNTIME STAGE ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish ./

# Container içinde 8080'de dinle
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Lab.Microservice.dll"]

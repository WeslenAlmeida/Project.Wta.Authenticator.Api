FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["*.sln", "./"]
COPY ["Application/Project.Wta.Authenticator.Api.csproj", "Application/"]
COPY ["CrossCutting.Configuration/Project.Wta.Authenticator.CrossCutting.Configuration.csproj", "CrossCutting.Configuration/"]
COPY ["CrossCutting.Exception/Project.Wta.Authenticator.CrossCutting.Exception.csproj", "CrossCutting.Exception/"]
COPY ["Domain/Project.Wta.Authenticator.Domain.csproj", "Domain/"]
COPY ["Infrastructure.Data/Project.Wta.Authenticator.Infrastructure.Data.csproj", "Infrastructure.Data/"]
COPY ["Infrastructure.Cache/Project.Wta.Authenticator.Infrastructure.Cache.csproj", "Infrastructure.Cache/"]
COPY ["Tests.Shared/Project.Wta.Authenticator.Tests.Shared.csproj", "Tests.Shared/"]
COPY ["Tests.Unity/Project.Wta.Authenticator.Tests.Unity.csproj", "Tests.Unity/"]

RUN dotnet restore "Application/Project.Wta.Authenticator.Api.csproj"
COPY . . 
WORKDIR "/src/Application"

RUN mv appsettings.Docker.json appsettings.json
RUN dotnet build "Project.Wta.Authenticator.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Project.Wta.Authenticator.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/CipherString = DEFAULT@SECLEVEL=2/CipherString = DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
ENTRYPOINT ["dotnet", "Project.Wta.Authenticator.Api.dll"]


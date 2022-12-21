#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/AZ_SSOTest.Web/AZ_SSOTest.Web.csproj", "src/AZ_SSOTest.Web/"]
COPY ["src/AZ_SSOTest.Application/AZ_SSOTest.Application.csproj", "src/AZ_SSOTest.Application/"]
COPY ["src/AZ_SSOTest.Domain/AZ_SSOTest.Domain.csproj", "src/AZ_SSOTest.Domain/"]
COPY ["src/AZ_SSOTest.Domain.Shared/AZ_SSOTest.Domain.Shared.csproj", "src/AZ_SSOTest.Domain.Shared/"]
COPY ["src/AZ_SSOTest.Application.Contracts/AZ_SSOTest.Application.Contracts.csproj", "src/AZ_SSOTest.Application.Contracts/"]
COPY ["src/AZ_SSOTest.HttpApi/AZ_SSOTest.HttpApi.csproj", "src/AZ_SSOTest.HttpApi/"]
COPY ["src/AZ_SSOTest.EntityFrameworkCore/AZ_SSOTest.EntityFrameworkCore.csproj", "src/AZ_SSOTest.EntityFrameworkCore/"]
RUN dotnet restore "src/AZ_SSOTest.Web/AZ_SSOTest.Web.csproj"
COPY . .
WORKDIR "/src/src/AZ_SSOTest.Web"
RUN dotnet build "AZ_SSOTest.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AZ_SSOTest.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AZ_SSOTest.Web.dll"]
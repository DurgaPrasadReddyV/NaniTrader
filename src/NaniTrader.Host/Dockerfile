#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/NaniTrader.Host/NaniTrader.Host.csproj", "src/NaniTrader.Host/"]
COPY ["src/NaniTrader.Application/NaniTrader.Application.csproj", "src/NaniTrader.Application/"]
COPY ["src/NaniTrader.Domain/NaniTrader.Domain.csproj", "src/NaniTrader.Domain/"]
COPY ["src/NaniTrader.Domain.Shared/NaniTrader.Domain.Shared.csproj", "src/NaniTrader.Domain.Shared/"]
COPY ["src/NaniTrader.Application.Contracts/NaniTrader.Application.Contracts.csproj", "src/NaniTrader.Application.Contracts/"]
COPY ["src/NaniTrader.Blazor/NaniTrader.Blazor.csproj", "src/NaniTrader.Blazor/"]
COPY ["src/NaniTrader.HttpApi.Client/NaniTrader.HttpApi.Client.csproj", "src/NaniTrader.HttpApi.Client/"]
COPY ["src/NaniTrader.EntityFrameworkCore/NaniTrader.EntityFrameworkCore.csproj", "src/NaniTrader.EntityFrameworkCore/"]
COPY ["src/NaniTrader.HttpApi/NaniTrader.HttpApi.csproj", "src/NaniTrader.HttpApi/"]
COPY ["src/NaniTrader.DbMigrator/NaniTrader.DbMigrator.csproj", "src/NaniTrader.DbMigrator/"]
RUN dotnet restore "src/NaniTrader.Host/NaniTrader.Host.csproj"
COPY . .
WORKDIR "/src/src/NaniTrader.Host"
RUN dotnet build "NaniTrader.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NaniTrader.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NaniTrader.Host.dll"]
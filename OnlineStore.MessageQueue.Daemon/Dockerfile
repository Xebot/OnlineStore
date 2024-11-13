#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OnlineStore.MessageQueue.Daemon/OnlineStore.MessageQueue.Daemon.csproj", "OnlineStore.MessageQueue.Daemon/"]
COPY ["Clients/OnlineStore.ApiClient/OnlineStore.ApiClient.csproj", "Clients/OnlineStore.ApiClient/"]
COPY ["Contracts/OnlineStore.Contracts/OnlineStore.Contracts.csproj", "Contracts/OnlineStore.Contracts/"]
COPY ["Infrastructure/OnlineStore.Infrastructure/OnlineStore.Infrastructure.csproj", "Infrastructure/OnlineStore.Infrastructure/"]
COPY ["Domain/OnlineStore.Domain/OnlineStore.Domain.csproj", "Domain/OnlineStore.Domain/"]
RUN dotnet restore "./OnlineStore.MessageQueue.Daemon/OnlineStore.MessageQueue.Daemon.csproj"
COPY . .
WORKDIR "/src/OnlineStore.MessageQueue.Daemon"
RUN dotnet build "./OnlineStore.MessageQueue.Daemon.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OnlineStore.MessageQueue.Daemon.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineStore.MessageQueue.Daemon.dll"]
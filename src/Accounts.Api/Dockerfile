FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt-get update && apt-get install -y curl

HEALTHCHECK  --interval=30s --timeout=5s --start-period=10s \
  CMD curl --fail http://localhost/health || exit 1

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Accounts.Api.csproj", "Accounts.Api/"]
RUN dotnet restore "Accounts.Api/Accounts.Api.csproj"

WORKDIR "/src/Accounts.Api"
COPY . .
RUN dotnet build "Accounts.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Accounts.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Accounts.Api.dll"]
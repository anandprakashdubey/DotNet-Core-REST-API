#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CoreRestAPI/CoreRestAPI.csproj", "CoreRestAPI/"]
COPY ["DBLayer/DBLayer.csproj", "DBLayer/"]
COPY ["DataModel/DataModel.csproj", "DataModel/"]
RUN dotnet restore "CoreRestAPI/CoreRestAPI.csproj"
COPY . .
WORKDIR "/src/CoreRestAPI"
RUN dotnet build "CoreRestAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoreRestAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoreRestAPI.dll"]
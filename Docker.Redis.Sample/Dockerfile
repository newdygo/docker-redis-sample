#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Docker.Redis.Sample/Docker.Redis.Sample.csproj", "Docker.Redis.Sample/"]
RUN dotnet restore "Docker.Redis.Sample/Docker.Redis.Sample.csproj"
COPY . .
WORKDIR "/src/Docker.Redis.Sample"
RUN dotnet build "Docker.Redis.Sample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Docker.Redis.Sample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Docker.Redis.Sample.dll"]
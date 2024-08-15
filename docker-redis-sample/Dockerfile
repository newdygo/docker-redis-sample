#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#See https://docs.docker.com/reference/dockerfile/ to understand sintaxe

#Create a new build stage from a base image.
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

#Change working directory.
WORKDIR /app

#Describe which ports your application is listening on.
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

#Copy files and directories.
COPY ["docker-redis-sample/docker-redis-sample.csproj", "docker-redis-sample/"]

#Execute build commands.
RUN dotnet restore "docker-redis-sample/docker-redis-sample.csproj"
COPY . .
WORKDIR "/src/docker-redis-sample"
RUN dotnet build "docker-redis-sample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "docker-redis-sample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#Specify default executable.
ENTRYPOINT ["dotnet", "docker-redis-sample.dll"]
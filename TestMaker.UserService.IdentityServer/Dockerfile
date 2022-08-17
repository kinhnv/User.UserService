#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-core
WORKDIR /src
COPY . .

FROM build-core AS build
RUN dotnet restore "TestMaker.UserService.IdentityServer/TestMaker.UserService.IdentityServer.csproj"
WORKDIR "/src/TestMaker.UserService.IdentityServer"
RUN dotnet build "TestMaker.UserService.IdentityServer.csproj" -c Release -o /app/build

FROM build-core AS build-infrastructure
WORKDIR "/src/TestMaker.UserService.Infrastructure"
RUN dotnet build "TestMaker.UserService.Infrastructure.csproj" -o /app/build

FROM build AS publish
RUN dotnet publish "TestMaker.UserService.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /
COPY --from=publish /app/publish app
COPY --from=publish /app/build TestMaker.UserService.Infrastructure/bin/Debug/net6.0/
WORKDIR /app
ENTRYPOINT ["dotnet", "TestMaker.UserService.IdentityServer.dll"]
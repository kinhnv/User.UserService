#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestMaker.UserService.Api/TestMaker.UserService.Api.csproj", "TestMaker.UserService.Api/"]
RUN dotnet restore "TestMaker.UserService.Api/TestMaker.UserService.Api.csproj"
COPY . .
WORKDIR "/src/TestMaker.UserService.Api"
RUN dotnet build "TestMaker.UserService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestMaker.UserService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestMaker.UserService.Api.dll"]
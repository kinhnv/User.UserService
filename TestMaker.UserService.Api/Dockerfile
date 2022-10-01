#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM pre-build-params_registry/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM pre-build-params_registry/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY TestMaker.UserService.Api TestMaker.UserService.Api
COPY TestMaker.UserService.Domain TestMaker.UserService.Domain
COPY TestMaker.UserService.Infrastructure TestMaker.UserService.Infrastructure
WORKDIR "/src/TestMaker.UserService.Api"
RUN sed -i "s/ReferenceOutputAssembly=\"false\"//g" ./TestMaker.UserService.Api.csproj
RUN dotnet restore "TestMaker.UserService.Api.csproj"
RUN dotnet build "TestMaker.UserService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestMaker.UserService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /
COPY --from=publish /app/publish app
COPY --from=publish /app/build TestMaker.UserService.Infrastructure/bin/Debug/net6.0/
WORKDIR /app
ENTRYPOINT ["dotnet", "TestMaker.UserService.Api.dll"]
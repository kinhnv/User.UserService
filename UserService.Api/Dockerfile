#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM pre-build-params_registry/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM pre-build-params_registry/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY UserService.Api UserService.Api
COPY UserService.Domain UserService.Domain
COPY UserService.Infrastructure UserService.Infrastructure
WORKDIR "/src/UserService.Api"
RUN sed -i "s,<ReferenceOutputAssembly>False</ReferenceOutputAssembly>,<ReferenceOutputAssembly>True</ReferenceOutputAssembly>,g" ./TestMaker.TestService.Api.csproj
RUN dotnet nuget add source --username pre-build-params_github-source-username --password pre-build-params_github-source-token --store-password-in-clear-text --name github "pre-build-params_github-source-url"
RUN dotnet restore "UserService.Api.csproj"
RUN dotnet build "UserService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /
COPY --from=publish /app/publish app
COPY --from=publish /app/build UserService.Infrastructure/bin/Debug/net6.0/
WORKDIR /app
ENTRYPOINT ["dotnet", "UserService.Api.dll"]
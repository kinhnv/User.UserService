#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG GITLAB_SOURCE_USERNAME
ARG GITLAB_SOURCE_TOKEN
ARG GITLAB_SOURCE_URL
WORKDIR /src
COPY UserService.Api UserService.Api
COPY UserService.Domain UserService.Domain
COPY UserService.Infrastructure UserService.Infrastructure
WORKDIR "/src/UserService.Api"
RUN sed -i \
    "s,<ReferenceOutputAssembly>False</ReferenceOutputAssembly>,<ReferenceOutputAssembly>True</ReferenceOutputAssembly>,g" \
    ./UserService.Api.csproj
RUN dotnet nuget add source \
    --username $GITLAB_SOURCE_USERNAME \
    --password $GITLAB_SOURCE_TOKEN \
    --store-password-in-clear-text \
    --name github "$GITLAB_SOURCE_URL"
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
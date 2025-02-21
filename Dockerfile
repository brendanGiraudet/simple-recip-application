# Build
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILDPLATFORM

WORKDIR /src

COPY ./simple_recip_application .

RUN dotnet restore

RUN dotnet build -c Release -o /app/build

RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Final
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/aspnet:9.0 
ARG BUILDPLATFORM

WORKDIR /app

EXPOSE 4242

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "simple_recip_application.dll"]
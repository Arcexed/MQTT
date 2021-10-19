FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MQTT.Api/MQTT.Api.csproj", "MQTT.Api/"]
RUN dotnet restore "MQTT.Api/MQTT.Api.csproj"
COPY . .
WORKDIR "/src/MQTT.Api"
RUN dotnet build "MQTT.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MQTT.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MQTT.Api.dll"]

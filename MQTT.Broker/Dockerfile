FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MQTT.Broker/MQTT.Broker.csproj", "MQTT.Broker/"]
RUN dotnet restore "MQTT.Broker/MQTT.Broker.csproj"
COPY . .
WORKDIR "/src/MQTT.Broker"
RUN dotnet build "MQTT.Broker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MQTT.Broker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MQTT.Broker.dll"]

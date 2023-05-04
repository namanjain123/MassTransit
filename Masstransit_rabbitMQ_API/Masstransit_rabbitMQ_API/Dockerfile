#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Masstransit_rabbitMQ_API/Masstransit_rabbitMQ_API.csproj", "Masstransit_rabbitMQ_API/"]
RUN dotnet restore "Masstransit_rabbitMQ_API/Masstransit_rabbitMQ_API.csproj"
COPY . .
WORKDIR "/src/Masstransit_rabbitMQ_API"
RUN dotnet build "Masstransit_rabbitMQ_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Masstransit_rabbitMQ_API.csproj" -c Release -o /app/publish /p:UseAppHost=false
# Start RabbitMQ container
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Masstransit_rabbitMQ_API.dll"]
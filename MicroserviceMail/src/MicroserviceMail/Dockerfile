#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/MicroserviceMail/MicroserviceMail.csproj", "src/MicroserviceMail/"]
RUN dotnet restore "src/MicroserviceMail/MicroserviceMail.csproj"
COPY . .
WORKDIR "/src/src/MicroserviceMail"
RUN dotnet build "MicroserviceMail.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroserviceMail.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroserviceMail.dll"]
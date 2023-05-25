FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["AppWebApi/AppWebApi.csproj", "AppWebApi/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Shared/Shared.csproj", "Shared/"]

RUN dotnet restore "AppWebApi/AppWebApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "AppWebApi/AppWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AppWebApi/AppWebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppWebApi.dll"]
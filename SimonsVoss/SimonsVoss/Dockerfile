FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SimonsVoss/SimonsVoss.csproj", "SimonsVoss/"]
RUN dotnet restore "SimonsVoss/SimonsVoss.csproj"
COPY . .
WORKDIR "/src/SimonsVoss"
RUN dotnet build "SimonsVoss.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimonsVoss.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimonsVoss.dll"]

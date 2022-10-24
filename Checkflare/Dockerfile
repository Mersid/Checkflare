FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Checkflare/Checkflare.csproj", "Checkflare/"]
RUN dotnet restore "Checkflare/Checkflare.csproj"
COPY . .
WORKDIR "/src/Checkflare"
RUN dotnet build "Checkflare.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Checkflare.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Checkflare.dll"]

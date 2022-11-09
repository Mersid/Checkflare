FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS run
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
EXPOSE 443
EXPOSE 3000
ENTRYPOINT ["dotnet", "Checkflare.dll", "--urls \"http://localhost:3000\""]

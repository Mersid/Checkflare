FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN #dotnet publish -c Release -r linux-x64 -p:PublishSingleFile=true --self-contained -o /app
RUN dotnet publish -c Release -o /app

FROM dorowu/ubuntu-desktop-lxde-vnc AS environment
WORKDIR /setup
RUN #wget https://dot.net/v1/dotnet-install.sh ; chmod +x ./dotnet-install.sh ; sudo ./dotnet-install.sh ; /root/.dotnet/dotnet dev-certs https
RUN wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb ; sudo dpkg -i packages-microsoft-prod.deb ; rm packages-microsoft-prod.deb
RUN sudo apt update ; sudo apt install -y aspnetcore-runtime-6.0
EXPOSE 80
EXPOSE 443
EXPOSE 5000
EXPOSE 5001
#ENTRYPOINT ["dotnet", "Checkflare.dll", "--urls \"http://localhost:3000\""]

FROM environment AS run
WORKDIR /app
COPY --from=build /app .
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

FROM dorowu/ubuntu-desktop-lxde-vnc AS environment
WORKDIR /setup
RUN wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb; \
    sudo dpkg -i packages-microsoft-prod.deb; \
    rm packages-microsoft-prod.deb; \
    sudo apt update; \
    sudo apt install -y aspnetcore-runtime-6.0 powershell

FROM environment AS run
WORKDIR /app
COPY --from=build /app .
COPY startup.sh .
RUN pwsh playwright.ps1 install
# Browsers installed at /home/ubuntu/.cache/ms-playwright. Copy it to /root/.cache/ms-playwright allows us to run from entrypoint or as root manually
RUN sudo cp -r /home/ubuntu/.cache/ms-playwright /root/.cache/
EXPOSE 80
EXPOSE 443
# VNC server
EXPOSE 6080
# Checkflare
EXPOSE 5000

ENTRYPOINT ["./startup.sh"]

# Run this docker image with "docker run -p 6080:80 -p 5000:5000 checkflare"

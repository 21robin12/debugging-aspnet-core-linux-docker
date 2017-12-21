FROM microsoft/aspnetcore:2.0
ARG source
WORKDIR /app
#exposes port 80 to linked services
EXPOSE 80

COPY . .

RUN mkdir ~/.ssh/
RUN mv /app/authorized_keys ~/.ssh/authorized_keys

RUN apt-get update
RUN apt-get install unzip
RUN apt-get install openssh-server unzip curl -y

# Alternatively, can use dropbear as SSH server. 
# With these settings, attach from VS using user "root" and password "hello"
#   apt-get install dropbear
#   edit /etc/default/dropbear, change NO_START=1
#   service dropbear start
#   passwd -> set to "hello"

# useful to have
RUN apt-get install emacs24-nox -y

ENTRYPOINT service ssh start && dotnet /app/bin/Debug/netcoreapp2.0/DockerDebug.dll && /bin/bash
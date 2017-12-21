# Attaching to an existing ASP.NET Core process running inside a Linux Docker container

> Debugging a Linux container using Visual Studio via SSH in this manner is pretty slow (~10 seconds to attach, and ~1s to perform any action through Visual Studio, such as expanding a node in the Locals tree)

## Quick Start

*~5 minutes, provided you have Visual Studio 2017 and Docker tools installed*

### Prerequisites

 - Visual Studio 2017 is installed & updated to the most recent version
 - Docker for Windows is installed
 - Linux containers are enabled (right-click system tray icon -> Switch to Linux containers)
 - PuTTYgen.exe is downloaded (used to generate SSH keys) 
 
### 1 - Generate SSH key

 - Open PuTTYgen.exe
 - Select `SSH-2 RSA` and 2048 bits
 - Click Generate and follow instructions to generate a key
 - Clone this repository
 - Copy the public key (entire contents of the textarea) into `DockerDebug/authorized_keys`
 - Conversions -> Export OpenSSH key -> (no passphrase) -> overwrite `DockerDebug/openssh_privatekey`
 
### 2 - Starting dotnet & attaching the debugger
 
 - Open Visual Studio 2017 in administrator mode and build the DockerDebug solution
 - `docker-compose up --force-recreate --build`
 - Visit http://172.20.128.2/api/test/get to check that the site is running
 - Attach to Process (`ctrl+alt+p`) 
 - Connection Type: SSH, Connection Target: 172.20.128.2 -> press enter
 - Host name: 172.20.128.2, Port: 22, User name: root, Authentication type: Private Key, Private key file: browse to `openssh_privatekey`
 - Click Connect
 - Attach to `dotnet` process, and select "Managed (.NET Core for Unix)"
 
## Troubleshooting 
 
### The container won't start

```
ERROR: for b4580af4d7ab_dockerdebuglinux_corelinux_1  Cannot start service corelinux: driver failed programming external connectivity on endpoint dockerdebuglinux_corelinux_1 (1689550ddf5db86ea5733d1e7153a82a0221f74163acc527e6a7c0532f07f25b): Error starting userland proxy: mkdir /port/tcp:0.0.0.0:9000:tcp:172.20.128.2:80: input/output error
```

Open Docker settings, Daemon tab, disable Experimental features
 
### The debugger won't attach

 - Install chocolatey
 - `choco install openssh`
 - Move openssh_privatekey into `C:\Users\<USERNAME>` - otherwise openssh will refuse to use it
 - `ssh root@localhost -p 9022 -i C:\Users\<USERNAME>\openssh_privatekey` to ensure that we can access the container via SSH - this should provide some more helpful error messages if not
 
## Relevant articles

 - Debugging from Visual Studio: https://blogs.msdn.microsoft.com/devops/2017/01/26/debugging-net-core-on-unix-over-ssh/
 - Debugging from VS Code: https://github.com/Microsoft/MIEngine/wiki/Offroad-Debugging-of-.NET-Core-on-Linux---OSX-from-Visual-Studio
 - Better way to get SSH key into container: https://stackoverflow.com/questions/27068596/how-to-include-files-outside-of-dockers-build-context
 - Connecting to containers by IP: https://stackoverflow.com/questions/39154408/connecting-to-containers-ip-address-is-impossible-in-docker-for-windows and https://www.dotnetcatch.com/2016/11/11/access-a-docker-container-from-the-docker-for-windows-host/


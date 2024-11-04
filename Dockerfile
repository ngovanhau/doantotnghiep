# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy solution and project files
COPY *.sln .
COPY BPMaster/*.csproj ./BPMaster/
RUN dotnet restore

# Copy everything else and build app
COPY BPMaster/ ./BPMaster/
WORKDIR /source/BPMaster

# Chỉ định cụ thể file .csproj trong lệnh publish
RUN dotnet publish RPMSMaster.csproj -c Release -o /app --no-restore

# Stage 2: Final Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "RPMSMaster.dll"]

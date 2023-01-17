#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MS.Accountant/Api/MS.Accountant.Api.csproj", "MS.Accountant/Api/"]
COPY ["MS.Accountant/Application/MS.Accountant.Application.csproj", "MS.Accountant/Application/"]
COPY ["MS.Accountant/Domain/MS.Accountant.Domain.csproj", "MS.Accountant/Domain/"]
RUN dotnet restore "MS.Accountant/Api/MS.Accountant.Api.csproj"
COPY . .
WORKDIR "/src/MS.Accountant/Api"
RUN dotnet build "MS.Accountant.Api.csproj" -c Local -o /app/build

FROM build AS publish
RUN dotnet publish "MS.Accountant.Api.csproj" -c Local -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MS.Accountant.Api.dll"]
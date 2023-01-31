#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
ARG CONFIGURATION=Release
ARG VERSIONSUFFIX

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG CONFIGURATION
ARG VERSIONSUFFIX
ENV CONFIGURATION=${CONFIGURATION}
ENV VERSIONSUFFIX=${VERSIONSUFFIX}
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["clients/nuget.config", "clients/"]
COPY ["clients/blazor/CodeBreaker.Blazor.Host/CodeBreaker.Blazor.Host.csproj", "clients/blazor/CodeBreaker.Blazor.Host/"]
COPY ["clients/blazor/CodeBreaker.Blazor/CodeBreaker.Blazor.csproj", "clients/blazor/CodeBreaker.Blazor/"]
COPY ["ui/blazor/CodeBreaker.UI.MudBlazor/CodeBreaker.UI.MudBlazor.csproj", "ui/blazor/CodeBreaker.UI.MudBlazor/"]
COPY ["ui/blazor/CodeBreaker.UI.Shared/CodeBreaker.UI.Shared.csproj", "ui/blazor/CodeBreaker.UI.Shared/"]
RUN dotnet restore "clients/blazor/CodeBreaker.Blazor.Host/CodeBreaker.Blazor.Host.csproj"
COPY . .
WORKDIR "/src/clients/blazor/CodeBreaker.Blazor.Host"
RUN dotnet build "CodeBreaker.Blazor.Host.csproj" -c ${CONFIGURATION} --version-suffix ${VERSIONSUFFIX} -o /app/build

FROM build AS publish
RUN dotnet publish "CodeBreaker.Blazor.Host.csproj" -c ${CONFIGURATION} --version-suffix ${VERSIONSUFFIX} -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CodeBreaker.Blazor.Host.dll"]
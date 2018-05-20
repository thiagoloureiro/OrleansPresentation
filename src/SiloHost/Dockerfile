FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY ConsoleApp2/SiloHost.csproj ConsoleApp2/
COPY HelloGrain/HelloGrain.csproj HelloGrain/
COPY IHelloGrain/HelloGrainContract.csproj IHelloGrain/
RUN dotnet restore ConsoleApp2/SiloHost.csproj
COPY . .
WORKDIR /src/ConsoleApp2
RUN dotnet build SiloHost.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SiloHost.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SiloHost.dll"]

FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /SimpleAPI
COPY . .
RUN dotnet restore
RUN dotnet publish -o /published-app

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /SimpleAPI
COPY --from=build /published-app /SimpleAPI
ENTRYPOINT [ "dotnet", "/SimpleAPI/SimpleAPI.dll" ]

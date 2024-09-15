FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /webapp

COPY ./Api/Api.csproj ./Api/
RUN dotnet restore ./Api/Api.csproj

COPY . .
RUN dotnet publish ./Api/Api.csproj -c Release -o /out

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /webapp
COPY --from=build /out .

EXPOSE 5435

ENTRYPOINT ["dotnet", "Tastys.Api.dll"]
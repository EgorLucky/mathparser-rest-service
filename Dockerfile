FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY RestService/*.csproj ./RestService/
COPY MathParserService.DAL/*.csproj ./MathParserService.DAL/
COPY MathParserService.DL/*.csproj ./MathParserService.DL/
COPY MathParserService.DL.Implementions/*.csproj ./MathParserService.DL.Implementions/
RUN dotnet restore

# copy everything else and build app
COPY RestService/. ./RestService/
COPY MathParserService.DAL/. ./MathParserService.DAL/
COPY MathParserService.DL/. ./MathParserService.DL/
COPY MathParserService.DL.Implementions/. ./MathParserService.DL.Implementions/

WORKDIR /app/RestService
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/RestService/out ./

CMD ASPNETCORE_URLS=http://*:$PORT dotnet RestService.dll
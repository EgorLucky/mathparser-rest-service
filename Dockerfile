FROM microsoft/dotnet:2.2-sdk AS build
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

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/RestService/out ./

CMD ASPNETCORE_URLS=http://*:$PORT dotnet RestService.dll
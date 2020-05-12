FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY RestService/*.csproj ./RestService/
COPY MathParser/*.csproj ./MathParser/
RUN dotnet restore

# copy everything else and build app
COPY RestService/. ./RestService/
COPY MathParser/. ./MathParser/

WORKDIR /app/RestService
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/RestService/out ./

CMD SENTRY_DSN=$SENTRY_DSN ASPNETCORE_URLS=http://*:$PORT dotnet RestService.dll
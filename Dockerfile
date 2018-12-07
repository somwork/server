FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./TaskHouseApi/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY ./TaskHouseApi ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:sdk

ENV ASPNETCORE_ENVIRONMENT=Production

WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 5000
ENTRYPOINT ["dotnet", "TaskHouseApi.dll"]

# Versions of the Docker base images
ARG SDK_VERSION=6.0.407
ARG ALPINE_VERSION=alpine3.17
ARG ASPNET_VERSION=6.0.15

# Project name
ARG PROJECT_NAME=ConsumeMqtt

# -------------------------------------------------
# Build developement image, named 'build-env'
# -------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:${SDK_VERSION}-${ALPINE_VERSION} AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# -------------------------------------------------
# Build runtime image using result of 'build-env'
# -------------------------------------------------
# The ASP.NET Core runtime image is used intentionally here, although the mcr.microsoft.com/dotnet/runtime:7.0 image could have been used.
FROM mcr.microsoft.com/dotnet/aspnet:${ASPNET_VERSION}-${ALPINE_VERSION}
# To use an arg in multiple stages, each stage must include the ARG instruction
ARG PROJECT_NAME
ENV PROJECT_NAME_ENV=${PROJECT_NAME}

WORKDIR /App
COPY --from=build-env /App/out .

# Start the application
CMD ["sh", "-c", "dotnet ${PROJECT_NAME_ENV}.dll"]

# Générer une librairie cliente C# à partir de la définition OpenAPI

A partir de la définition OpenAPI d'un web service (fichier `.yaml` ou `.json`), génère un paquet NuGet qui sera utilisé pour appeler ce Web Service.

## Générer les sources C#

### Pour .Net Core

#### OpenAPI-generator

Générer les fichiers sources de la librairie cliente de l'API en C# :

```bash
# Version of OpenAPI Generator : see https://hub.docker.com/r/openapitools/openapi-generator-cli/tags
OPENAPI_GENERATOR_VERSION=v6.6.0
# Project name: WARNING SHALL NOT CONTAIN SPECIAL CHARACTERS (LIKE '-')
PROJECT_NAME=ReadFromMqttClient
# OpenAPI Specification file name
OAS_FILE_NAME=swagger.json
# Folder containing the OpenAPI Specification file (with respect to working directory)
FOLDER_CONTAINING_OAS=ReadFromMqttAPI

docker run --rm -v "${PWD}:/local" openapitools/openapi-generator-cli:${OPENAPI_GENERATOR_VERSION} generate \
    -i /local/${FOLDER_CONTAINING_OAS}/${OAS_FILE_NAME} \
    -g csharp-netcore \
    -o /local/${PROJECT_NAME} \
    --package-name ${PROJECT_NAME}
```

Les fichiers générés appartiennent à l'utilisateur `root` (celui du container).
Donner les droits à l'utilisateur courant :

```bash
# Change the owner of the generated folder
sudo chown ${USER}:${USER} -R ${PWD}/${PROJECT_NAME}
```

Donner à la libraire cliente le numéro de version de l'API (par défaut vaut `1.0.0`):

```bash
# API version (must be X.Y.Z)
API_VERSION=0.1.0
sed -i "s/1.0.0/${API_VERSION}/g" ${PWD}/${PROJECT_NAME}/src/${PROJECT_NAME}/${PROJECT_NAME}.csproj
```

Générer le paquet Nuget :

```bash
OUTPUT_FOLDER=${PWD}/${PROJECT_NAME}/NugetPackage
# Create folder if needed
mkdir -p ${OUTPUT_FOLDER}
dotnet restore ${PWD}/${PROJECT_NAME}/${PROJECT_NAME}.sln
dotnet pack ${PWD}/${PROJECT_NAME}/${PROJECT_NAME}.sln -o ${OUTPUT_FOLDER}
```

Rendre le paquet Nuget accessible, soit dans un registry Nuget privé, soit en local (fonction du `Nuget.config`) :

```bash
# Nuget package local source, must exist in `Nuget.config` file
LOCAL_NUGET_SOURCE=~/Temp/NugetPackages
mkdir -p ${LOCAL_NUGET_SOURCE}
cp ${OUTPUT_FOLDER}/${PROJECT_NAME}.${API_VERSION}.nupkg ${LOCAL_NUGET_SOURCE}
```

### Pour .Net framework

```bash
docker run --rm -v ${PWD}:/local swaggerapi/swagger-codegen-cli-v3:3.0.35 generate \
    -i /local/interface/swagger.json \
    -l csharp -c /local/swagger-conf-net-framework.json \
    -o /local/out/csharp
```



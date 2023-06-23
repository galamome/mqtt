
## Consommer la librairie cliente d'une API

### Configurer une source locale pour les paquets Nuget (si développement)

Dans le fichier `Nuget.config` :

```xml
<packageSources>
    <!-- Development time package source on Linux -->
    <add key="Linux temp package source" value="~/Temp/NugetPackages" />
```

Vérifier la présence du paquet dans le répertoire source locale.

### Ajouter la référence au paquet Nuget

```bash
PROJECT_CONSUMING_API_FOLDER=./ConsumeMqtt
# Get to the directory containing the CSPROJ where you want to add the Nuget package
cd PROJECT_CONSUMING_API_FOLDER

PROJECT_NAME=ReadFromMqttClient
API_VERSION=0.1.0
dotnet add package ${PROJECT_NAME} --version ${API_VERSION}
```

### Consommer l'API

Voir ![service consommant l'API](./Consume_client_library.cs)
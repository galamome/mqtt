
# Simple containerized application reading Mqtt and posting to a REST API

Proof Of Concept of a containerised application, that reads a Mqtt topic, and persists the content of each message read :

- **either** to a REST API (`ApiService` implementation of `IPersistService`)
  - environnement variable
    - `READFROMMQTT_API_HOST_PORT` defines the hostname and the port hosting the API
    - `READFROMMQTT_API_PROTOCOL` defines the protocol
  - [video example](./Documentation/images/Subscribe_to_MQTT_post_to_API.webm)
- **either** writing the content of each message read in a text file (`FileWriterService` implementation of `IPersistService`)
  - environnement variable `FILE_TO_WRITE` defines the path of the file to write
  - [video example](./Documentation/images/Persist_to_file.webm)

> NOTICE: to change the persistence mode, just change which service is injected for implementation of `IPersistService` in file [ConsumeMqtt/Program.cs](./ConsumeMqtt/Program.cs)

```csharp
    // Persist to file
    .AddSingleton<IPersistService, FileWriterService>()
    // Persist to API
    //.AddSingleton<IPersistService, ApiService>()
```

## Launch producer

### Python 3.3+

```python
python3 -m venv venv
```

### Activate the virtualenv (OS X & Linux)

```
source venv/bin/activate
```

### Load an existing configuration

```
pip install -r requirements.txt
```


# Simple containerise application reading Mqtt and posting to a REST API

Proof Of Concept of a containerised application, that reads a Mqtt topic, and posts the content of each message read to a REST API

## Build all container images

```bash
docker-compose build
```

## Launch system

```bash
docker-compose up -d
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

### Launch producer script

```bash
python Producer.py
```

## Check the messages are reflected in the API

Browse `http://localhost:88/swagger/index.html` (port corresponds to `docker-compose.yml`, part `readfrommqttapi`).


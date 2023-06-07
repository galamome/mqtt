# rabbitmq_mqtt

https://www.architect.io/blog/2021-01-19/rabbitmq-docker-tutorial/

## Build image with 

```bash
docker build . --tag rabbitmq_mqtt -f Dockerfile
```

## Run

```bash
docker run --rm -it \
    -p 15672:15672 \
    -p 5672:5672 \
    -p 1883:1883 \
    rabbitmq_mqtt
```

Access the UI (`guest` / `guest`):

 http://localhost:15672

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

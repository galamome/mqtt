https://medium.com/python-point/mqtt-basics-with-python-examples-7c758e605d4

# Run Mosquitto MQTT broker

https://cedalo.com/blog/mosquitto-docker-configuration-ultimate-guide/

```bash
docker run -it -d --name mos1 \
    -p 1883:1883 \
    -p 9001:9001 \
    -v ./mosquitto/mosquitto.conf:/mosquitto/config/mosquitto.conf \
    eclipse-mosquitto:2.0.15-openssl
```
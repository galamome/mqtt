# Username and password of a MQTT user
ARG TEST_USER=mqtt-test
ARG TEST_PSWD=mqtt-test

FROM rabbitmq:3.12.0-management

# Enable MQTT plugin
RUN rabbitmq-plugins enable --offline rabbitmq_mqtt

# username and password are both "mqtt-test"
#RUN rabbitmqctl add_user ${TEST_USER} ${TEST_PSWD}
#RUN rabbitmqctl set_permissions -p / ${TEST_USER} ".*" ".*" ".*"
#RUN rabbitmqctl set_user_tags ${TEST_USER} management
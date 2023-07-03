import random
import paho.mqtt.client as mqtt 
import time
from datetime import datetime

broker_hostname = "localhost"
port = 1883
topic = "Measurement_tool"
nbMessageToPost = 100

# List of boolean variables
BOOLVARIABLES = ["CSPA_LOW_POS", "CSPA_HIGH_POS"]
# List of real variables, with range of possible values
REALVARIABLES = [
    ["FLIM_FDS", 20, 30],
    ["T_START_CLUTCH", 0, 3],
    ["SP_UP_SUB_PH", 0, 5],
    ["LIM_CSV_MSV", 2, 18]
]

def get_message_real_variable_random() -> str:
    """
    Return a message with a real variable name and a real value,
    randomly generated in the possible range

    Returns
    -------
    A message formatted as real_variable_name=random_real_value_in_the_range
    """
    index = random.randint(0, len(REALVARIABLES) - 1)
    realVar = random.uniform(REALVARIABLES[index][1], REALVARIABLES[index][2])
    return f'{REALVARIABLES[index][0]}={realVar}'

def get_message_bool_variable_random() -> str:
    """
    Return a message with a boolean variable name and a boolean value randomly generated

    Returns
    -------
    A message formatted as boolean_variable_name=True / False
    """

    index = random.randint(0, len(BOOLVARIABLES) - 1)    
    return f'{BOOLVARIABLES[index]}={random.choice([True, False])}'

def on_connect(client, userdata, flags, return_code):
    if return_code == 0:
        print("connected")
    else:
        print("could not connect, return code:", return_code)

client = mqtt.Client("Client1")
# client.username_pw_set(username="user_name", password="password") # uncomment if you use password auth
client.on_connect=on_connect

client.connect(broker_hostname, port)
client.loop_start()

try:
    while True:
        time.sleep(random.uniform(0, 1))

        # Get current ISO 8601 datetime in string format
        iso_timestamp = datetime.now().isoformat()
        # Create a message
        msg = f'{iso_timestamp};{get_message_real_variable_random()}'
        result = client.publish(topic, msg)
        status = result[0]
        if status == 0:
            print(f'Message {str(msg)} is published to topic {topic}')
        else:
            print(f'Failed to send message to topic {topic}')

        msg = f'{iso_timestamp};{get_message_bool_variable_random()}'
        result = client.publish(topic, msg)
        status = result[0]
        if status == 0:
            print(f'Message {str(msg)} is published to topic {topic}')
        else:
            print(f'Failed to send message to topic {topic}')


finally:
    client.loop_stop()
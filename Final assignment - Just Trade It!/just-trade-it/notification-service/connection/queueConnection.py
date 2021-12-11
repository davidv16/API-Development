import pika
from os import environ
from time import sleep

exchange_name = 'just-trade-it-exchange'

def connect_to_mb():
    error = False
    queue_host = environ.get('RMQ_HOST') or 'localhost'
    while not error:
        try:
            credentials=pika.PlainCredentials(
                environ.get('RMQ_USER') or 'guest',
                environ.get('RMQ_PASS') or 'guest'
            )
            connection = pika.BlockingConnection(
                pika.ConnectionParameters(
                    queue_host,
                    5672,
                    '/',
                    credentials        
                )
            )
            channel = connection.channel()
            return (channel, connection)
        except:
            sleep(5)
            continue

def setup():
    '''
    error = False
    queue_host = environ.get('RMQ_HOST') or 'localhost'
    credentials=pika.PlainCredentials(
        environ.get('RMQ_USER') or 'guest',
        environ.get('RMQ_PASS') or 'guest'
    )

    while not error:
        try:
            connection = pika.BlockingConnection(
                pika.ConnectionParameters(
                    queue_host,
                    5672,
                    '/',
                    credentials        
                )
            )
            channel = connection.channel()
        except:
            sleep(5)
            continue
    '''
    channel, connection = connect_to_mb()
    
    # Declare the exchange, if it doesn't exist
    channel.exchange_declare(
        exchange=exchange_name,
        exchange_type='direct',
        durable=True
    )

    return (
        channel,
        connection,
        exchange_name
    )
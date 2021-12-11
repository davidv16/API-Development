from services import emailService

routing_key = 'new-trade-request'
email_queue_name = 'new-trade-queue'

def setup_handler(channel, exchange_name):
    # Declare the exchange, if it doesn't exist
    channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)
    # Declare the queue, if it doesn't exist
    channel.queue_declare(queue=email_queue_name, durable=True)
    # Bind the queue to a specific exchange with a routing key
    channel.queue_bind(exchange=exchange_name, queue=email_queue_name, routing_key=routing_key)

    channel.basic_consume(email_queue_name,
                      emailService.send_new_trade_email,
                      auto_ack=False)

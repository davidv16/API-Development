const amqp = require('amqplib/callback_api')
const { Order, OrderItem } = require('./data/db');

const messageBrokerInfo = {
    exchanges: {
        order: 'order_exchange'
    },
    queues: {
        orderQueue: 'order_queue'
    },
    routingKeys: {
        createOrder: 'create_order'
    }
}

const createMessageBrokerConnection = () => new Promise((resolve, reject) => {
    amqp.connect('amqp://localhost', (err, conn) => {
        if (err) { reject(err); }
        resolve(conn);
    });
});

const createChannel = connection => new Promise((resolve, reject) => {
    connection.createChannel((err, channel) => {
        if (err) { reject(err); }
        resolve(channel);
    });
});

const configureMessageBroker = channel => {
    const { exchanges, queues, routingKeys } = messageBrokerInfo;

    channel.assertExchange(exchanges.order, 'direct', { durable: true});
    channel.assertQueue(queues.orderQueue, {durable: true});
    channel.bindQueue(queues.orderQueue, exchanges.order, routingKeys.createOrder);
};

(async () => {
    const messageBrokerConnection = await createMessageBrokerConnection();
    const channel = await createChannel(messageBrokerConnection);

    configureMessageBroker(channel);

    const { queues } = messageBrokerInfo;

    channel.consume(queues.orderQueue, data => {
        const dataJson = JSON.parse(data.content.toString());

        let totalPrice = 0;
        dataJson.items.forEach(item => {
            totalPrice += item.unitPrice * item.quantity
        })

        const order = new Order({
            customerEmail: dataJson.email,
            totalPrice: totalPrice,
            orderDate: Date.now()
        })
        order.save();
        console.log(order)

        dataJson.items.forEach(item => {
            OrderItem.create({
                ...item,
                rowPrice: item.quantity * item.unitPrice,
                orderId: order._id
            })
        })

    }, { noAck: true });

})().catch(e => console.error(e));
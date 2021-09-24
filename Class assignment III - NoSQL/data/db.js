const mongoose = require('mongoose');
const sharkSchema = require('../schemas/sharkSchema');
const attackSchema = require('../schemas/attackSchema');
const areaSchema = require('../schemas/areaSchema');

const connection = mongoose.createConnection('mongodb+srv://ufo-user:1234@spy-on-shark.nh9sy.mongodb.net/myFirstDatabase?retryWrites=true&w=majority', {
    useNewUrlParser: true
});

module.exports = {
    Shark: connection.model('Shark', sharkSchema),
    Attack: connection.model('Attack', attackSchema),
    Area: connection.model('Area', areaSchema),
    connection
};

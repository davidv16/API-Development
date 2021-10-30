const loadDatabase = require('../data/db');

const getCollection = async () => {
    const database = await loadDatabase();
    return database.collection('donuts');
}

module.exports = {
    getAllDonuts: async () => {
        const collection = await getCollection();
        return collection.find({}).toArray();
    }
}

const Schema = require('mongoose').Schema

module.exports = new Schema({
  pickupGameId: { type: String, required: true },
  playerId: { type: String, required: true }
})

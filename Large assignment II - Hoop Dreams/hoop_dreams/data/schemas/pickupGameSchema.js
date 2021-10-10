const Schema = require('mongoose').Schema

module.exports = new Schema({
  start: { type: Date, required: true },
  end: { type: Date, required: true },
  locationId: { type: String, required: true },
  hostId: { type: String, required: true }
})
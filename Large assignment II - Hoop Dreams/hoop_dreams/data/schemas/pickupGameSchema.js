const Schema = require('mongoose').Schema

module.exports = new Schema({
  start: { type: Date, required: true },
  end: { type: Date, required: true },
  basketballFieldId: { type: String, required: true },
  hostId: { type: String, required: true },
  deleted: { type: Boolean, default: false }
})

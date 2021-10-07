const Schema = require('mongoose').Schema

module.exports = new Schema({
  name: { type: String, required: true },
  playedGameIds: { type: [String], required: true }
})

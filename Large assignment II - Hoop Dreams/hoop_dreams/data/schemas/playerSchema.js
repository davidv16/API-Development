const Schema = require('mongoose').Schema

module.exports = new Schema({
  name: { type: String, required: true },
  deleted: { type: Boolean, default: false }
})

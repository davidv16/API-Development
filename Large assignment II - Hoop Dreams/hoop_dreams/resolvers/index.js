const basketballFieldResolver = require('./basketballFieldResolver')
const pickupGameResolver = require('./pickupGameResolver')
const playerResolver = require('./playerResolver')

module.exports = {
  Query: {
    ...basketballFieldResolver.queries,
    ...pickupGameResolver.queries,
    ...playerResolver.queries
  },
  ...basketballFieldResolver.types,
  ...pickupGameResolver.types
}
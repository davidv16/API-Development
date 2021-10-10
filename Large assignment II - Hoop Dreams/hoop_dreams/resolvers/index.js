const moment = require('moment');
const { GraphQLScalarType } = require('graphql')
const basketballFieldResolver = require('./basketballFieldResolver')
const pickupGameResolver = require('./pickupGameResolver')
const playerResolver = require('./playerResolver')

module.exports = {
  Query: {
    ...basketballFieldResolver.queries,
    ...pickupGameResolver.queries,
    ...playerResolver.queries
  },
  Mutation: {
    ...pickupGameResolver.mutations,
    ...playerResolver.mutations
  },
  ...basketballFieldResolver.types,
  ...pickupGameResolver.types,
  ...playerResolver.types,
  Moment: new GraphQLScalarType({
    name: 'Moment',
    serialize: (date) => moment(date).locale('is').format('llll'),
    parseValue: (date) => { return date },
    parseLiteral: (date) => { return date }
  })
}

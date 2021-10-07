const { ApolloServer } = require('apollo-server')
const typeDefs = require('./schema')
const resolvers = require('./resolvers')
const database = require('./data/db')
const serviceCollection = require('./services')

const server = new ApolloServer({
  typeDefs,
  resolvers,
  context: {
    db: database,
    services: serviceCollection
  }
})

server.listen()
  .then(({ url }) => console.log(`GraphQL Service is running on ${url}`))

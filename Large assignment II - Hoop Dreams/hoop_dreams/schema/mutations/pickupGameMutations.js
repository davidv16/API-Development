module.exports = `
  createPickupGame(input: PickupGameInput!): PickupGame!
  removePickupGame(id: String!): Boolean!
  addPlayerToPickupGame(playerId: String! pickupGameId: String): PickupGame!
  removePlayerFromPickupGame(playerId: String! pickupGameId: String): Boolean!
`

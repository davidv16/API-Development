const { ObjectId } = require('mongodb')
const { PickupGame } = require('../data/db')

module.exports = {
  queries: {
    allPickupGames: (parent, args, { db }) => {
      
      return PickupGame.find()
        .then(pickupGames => {
          return pickupGames
        }).catch(err => {
          console.error(err)
        })
    },
    pickupGame: (parent, { id }, { db }) => {
      return PickupGame.findOne(ObjectId(id))
        .then(pickupGame => {
          return pickupGame
        }).catch(err => {
          console.error(err)
        })
    }
  },

  mutations: {
    createPickupGame: (parent , {input}) => {
      const { start, end, basketballFieldId, hostId } = input;
      const newPickupGame = new PickupGame({
        start,
        end,
        basketballFieldId,
        hostId
      })
      return newPickupGame.save()
        .then(result => {
          return {...result._doc}
        }).catch(err => {
          console.error(err)
        })
    },
    removePickupGame: () => {},
    addPlayerToPickupGame: () => {},
    removePlayerFromPickupGame: () => {}
  },

  types: {
    PickupGame: {
      location: async (parent, args, { services }) => {
        const service = services.basketballFieldService

        return await service.getBasketballField(parent.locationId)
      },
      host: async (parent, args, { db }) => {
        const { Player } = db

        return await Player.findOne({ _id: ObjectId(parent.hostId) })
      },
      registeredPlayers: async (parent, args, { db }) => {
        const { PickupGamePlayers, Player } = db
        const playerIds = await PickupGamePlayers.find({ pickupGameId: parent.id }).then((d) => d.map(i => i.playerId))

        return await Player.find({ _id: { $in: playerIds } })
      }
    }
  }
}

/**
 * input PickupGameInput {
    start: Moment!
    end: Moment!
    basketballFieldId: String!
    hostId: String!
  }
 */
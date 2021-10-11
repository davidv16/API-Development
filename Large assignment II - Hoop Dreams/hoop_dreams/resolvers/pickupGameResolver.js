const { ObjectId } = require('mongodb')

module.exports = {
  queries: {
    allPickupGames: (parent, args, { db }) => {
      const { PickupGame } = db

      return PickupGame.find()
        .then(pickupGames => {
          return pickupGames
        }).catch(err => {
          console.error(err)
        })
    },
    pickupGame: (parent, { id }, { db }) => {
      const { PickupGame } = db

      return PickupGame.findOne({ _id: ObjectId(id) })
        .then(pickupGame => {
          return pickupGame
        }).catch(err => {
          console.error(err)
        })
    }
  },

  mutations: {
    createPickupGame: (parent, { input }, { db }) => {
      const { pickupGame } = db
      const { start, end, basketballFieldId, hostId } = input

      const newPickupGame = new PickupGame({
        start,
        end,
        basketballFieldId,
        hostId
      })

      return newPickupGame.save()
        .then(result => {
          return { ...result._doc }
        }).catch(err => {
          console.error(err)
        })
    },
    removePickupGame: (parent, { id }, { db }) => {
      const { PickupGame } = db

      return PickupGame.findByIdAndRemove({ _id: ObjectId(id) })
        .then(result => {
          return result
        }).catch(err => {
          console.error(err)
        })
    },
    addPlayerToPickupGame: (parent, { playerId, pickupGameId }, { db }) => {
      const { PickupGame, PickupGamePlayers, Player } = db

      // TODO: check if player exists
      // TODO: check if pickup game exists
      // TODO: check if pickup game capacity has exceded
      // TODO: check if pickup game has elapsed
    },
    removePlayerFromPickupGame: (parent, { playerId, pickupGameId }, { db }) => {
      const { PickupGame, PickupGamePlayers, Player } = db

      // TODO: check if player exists
      // TODO: check if pickup game exists
      // TODO: check if pickup game has elapsed
    }
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

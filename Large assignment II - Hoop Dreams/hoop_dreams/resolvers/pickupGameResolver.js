const moment = require('moment')
const { ObjectId } = require('mongodb')
const errors = require('../errors')

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
    createPickupGame: async (parent, { input }, { db, services }) => {
      const { PickupGame, PickupGamePlayers, Player } = db
      const service = services.basketballFieldService
      const { start, end, basketballFieldId, hostId } = input

      const basketballField = await service.getBasketballField(basketballFieldId)
      const player = await Player.findOne({ _id: ObjectId(hostId), delete: false })
      const pickupGames = await PickupGame.find({ basketballFieldId: basketballFieldId })

      console.log(pickupGames)
      
      // Check if player exists
      if (!player) { return new errors.NotFoundError() }

      // Check if new game is on a closed field
      if (basketballField.status === 'CLOSED') { return new errors.BasketballFieldClosedError() }
      
      // Check if new game is not created at a time that has passed
      const dateHasPassed = moment.duration(moment(start.value).diff(moment(new Date()))).asMinutes() < 0
      if (dateHasPassed) { return new errors.PickupGameAlreadyPassedError() }

      const duration = moment.duration(moment(end.value).diff(moment(start.value))).asMinutes()
      console.log(duration)
      // Check if new game has an end time that is after the start time
      if (duration < 0) { return new errors.PickupGameAlreadyPassedError() }

      // Check if new game is within allowed time limit of min 5 minutes, max 2 hours
      if (duration < 5 || duration > 120) { return new errors.PickupGameMinMaxTimeError() }

      // TODO: check if new game overlaps with another game on selected field
      
      const newPickupGame = new PickupGame({
        start: start.value,
        end: end.value,
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

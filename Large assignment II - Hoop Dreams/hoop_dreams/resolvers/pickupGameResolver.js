const { Types: { ObjectId } } = require('mongoose')
const moment = require('moment')
const errors = require('../errors')

module.exports = {
  queries: {
    allPickupGames: async (parent, args, { db }) => {
      const { PickupGame } = db
      const pickupGames = await PickupGame.find({ deleted: false })

      return pickupGames
    },
    pickupGame: async (parent, { id }, { db }) => {
      const { PickupGame } = db
      const pickupGame = await PickupGame.findOne({ _id: ObjectId(id), deleted: false })

      // Check if pickup game exists
      if (!pickupGame) { return new errors.NotFoundError() }

      return pickupGame
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

      // Check if player exists
      if (!player) { return new errors.NotFoundError() }

      // Check if basketball field exists
      if (!basketballField) { return new errors.NotFoundError() }

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

      // Check if new game overlaps with another game on selected field
      const overlappedGames = pickupGames.filter((game) => {
        return (game.start <= new Date(start.value) && new Date(start.value) <= game.end) ||
          (game.start <= new Date(end.value) && new Date(end.value) <= game.end) ||
          (new Date(start.value) <= game.start && game.start <= new Date(end.value)) ||
          (new Date(start.value) <= game.end && game.end <= new Date(end.value))
      })
      if (overlappedGames.length > 0) { return new errors.PickupGameOverlapError() }

      const newPickupGame = new PickupGame({
        start: start.value,
        end: end.value,
        basketballFieldId,
        hostId
      })

      await newPickupGame.save()
        .catch(err => { throw new Error(err) })

      const newPickupGamePlayer = new PickupGamePlayers({
        playerId: hostId,
        pickupGameId: newPickupGame._id
      })

      await newPickupGamePlayer.save()
        .catch(err => { throw new Error(err) })

      return newPickupGame
    },
    removePickupGame: async (parent, { id }, { db }) => {
      const { PickupGame } = db

      const pickupGame = await PickupGame.findOne({ _id: id, deleted: false })

      // Check if pickup game exists
      if (!pickupGame) { return new errors.NotFoundError() }

      // Check if pickup game has passed
      const dateHasPassed = moment.duration(moment(pickupGame.start).diff(moment(new Date()))).asMinutes() < 0
      if (dateHasPassed) { return new errors.PickupGameAlreadyPassedError() }

      pickupGame.deleted = true
      await pickupGame.save()
        .catch(err => { throw new Error(err) })

      return true
    },
    addPlayerToPickupGame: async (parent, { playerId, pickupGameId }, { db, services }) => {
      const { PickupGame, PickupGamePlayers, Player } = db
      const service = services.basketballFieldService

      const player = await Player.findOne({ _id: playerId })
      const pickupGame = await PickupGame.findOne({ _id: pickupGameId, deleted: false })

      // Check if player exists
      if (!player) { return new errors.NotFoundError() }

      // Check if pickup game exists
      if (!pickupGame) { return new errors.NotFoundError() }

      // Check if pickup game capacity has exceded
      const basketballField = await service.getBasketballField(pickupGame.basketballFieldId)
      const pickupGames = await PickupGamePlayers.find({ pickupGameId })
      if (basketballField.capacity <= pickupGames.length) { return new errors.PickupGameExceedMaximumError() }

      // Check if pickup game has passed
      const dateHasPassed = moment.duration(moment(pickupGame.start).diff(moment(new Date()))).asMinutes() < 0
      if (dateHasPassed) { return new errors.PickupGameAlreadyPassedError() }

      // Check if player is registered to pickup game
      const alreadyRegistered = await PickupGamePlayers.find({ playerId, pickupGameId })
      if (alreadyRegistered.length !== 0) { return new errors.PickupGamePlayerAlreadyRegisteredError() }

      // Check if player has any overlappsing games
      const playerPickupGameIds = await PickupGamePlayers.find({ playerId }).then((d) => d.map((i) => i.pickupGameId))
      const playerPickupGames = await PickupGame.find({ _id: { $in: playerPickupGameIds } })
      const overlappedGames = playerPickupGames.filter((game) => {
        return (game.start <= new Date(pickupGame.start) && new Date(pickupGame.start) <= game.end) ||
          (game.start <= new Date(pickupGame.end) && new Date(pickupGame.end) <= game.end) ||
          (new Date(pickupGame.start) <= game.start && game.start <= new Date(pickupGame.end)) ||
          (new Date(pickupGame.start) <= game.end && game.end <= new Date(pickupGame.end))
      })
      if (overlappedGames.length > 0) { return new errors.PickupGameOverlapError() }

      const newPickupGamePlayers = PickupGamePlayers({
        playerId,
        pickupGameId
      })

      await newPickupGamePlayers.save()

      return pickupGame
    },
    removePlayerFromPickupGame: async (parent, { playerId, pickupGameId }, { db }) => {
      const { PickupGame, PickupGamePlayers, Player } = db

      const player = await Player.findOne({ _id: playerId })
      const pickupGame = await PickupGame.findOne({ _id: pickupGameId, deleted: false })

      // Check if player exists
      if (!player) { return new errors.NotFoundError() }

      // Check if pickup game exists
      if (!pickupGame) { return new errors.NotFoundError() }

      // Check if player is registered to pickup game
      const alreadyRegistered = await PickupGamePlayers.find({ playerId, pickupGameId })
      if (alreadyRegistered.length === 0) { return new errors.PickupGamePlayerNotRegisteredError() }

      // Check if pickup game has passed
      const dateHasPassed = moment.duration(moment(pickupGame.start).diff(moment(new Date()))).asMinutes() < 0
      if (dateHasPassed) { return new errors.PickupGameAlreadyPassedError() }

      return PickupGamePlayers.deleteOne({ playerId, pickupGameId })
        .then(async () => {
          // If player was host assign new host to game, if no player is left mark game as deleted
          const remainingPlayerIds = await PickupGamePlayers.find({ pickupGameId }).then((d) => d.map(i => i.playerId))
          console.log(remainingPlayerIds)

          if (remainingPlayerIds.length === 0) {
            pickupGame.deleted = true
            pickupGame.save()
          } else if (playerId === pickupGame.hostId) {
            const newHost = await Player.findOne({ _id: remainingPlayerIds }).sort({ name: 1 })

            pickupGame.hostId = newHost._id
            pickupGame.save()
          }

          return true
        })
        .catch((err) => { return new Error(err) })
    }
  },

  types: {
    PickupGame: {
      location: async ({ basketballFieldId }, args, { services }) => {
        const service = services.basketballFieldService

        return await service.getBasketballField(basketballFieldId)
      },
      host: async ({ hostId }, args, { db }) => {
        const { Player } = db

        return await Player.findOne({ _id: ObjectId(hostId) })
      },
      registeredPlayers: async ({ id }, args, { db }) => {
        const { PickupGamePlayers, Player } = db
        const playerIds = await PickupGamePlayers.find({ pickupGameId: id }).then((d) => d.map(i => i.playerId))

        return await Player.find({ _id: { $in: playerIds } })
      }
    }
  }
}

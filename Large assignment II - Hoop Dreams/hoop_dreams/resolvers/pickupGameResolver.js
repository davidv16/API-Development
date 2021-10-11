const moment = require('moment')
const { Types: { ObjectId } } = require('mongoose')
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

      /** 6. A query or mutation which accepts an id as a field argument must check whether the
      resource with the provided id exists */
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

      console.log(pickupGames)
      
      // Check if player exists
      /** 6. A query or mutation which accepts an id as a field argument must check whether the
      resource with the provided id exists */
      if (!player) { return new errors.NotFoundError() }

      // Check if basketball field exists
      /** 6. A query or mutation which accepts an id as a field argument must check whether the
      resource with the provided id exists */
      if (!basketballField) { return new errors.NotFoundError() }

      /** 1. Pickup games cannot be added to a basketball field which has a status of */
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
      /** 2. Pickup games cannot overlap if they are being played in the same basketball field */

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
    removePickupGame: async (parent, { id }, { db }) => {
      const { PickupGame } = db

      const pickupGame = await PickupGame.findOne({ _id: id })

      // Check if pickup game exists
      /** 6. A query or mutation which accepts an id as a field argument must check whether the
      resource with the provided id exists */
      if (!pickupGame) { return new errors.NotFoundError() }

      return PickupGame.findByIdAndUpdate(ObjectId(id), { deleted: true })
        .then(() => { return true })
        .catch((err) => { return new Error(err) })
    },
    addPlayerToPickupGame: async (parent, { playerId, pickupGameId }, { db, services }) => {
      const { PickupGame, PickupGamePlayers, Player } = db
      const service = services.basketballFieldService

      const player = await Player.findOne({ _id: playerId })
      const pickupGame = await PickupGame.findOne({ _id: pickupGameId })
      
      // Check if player exists
      /** 6. A query or mutation which accepts an id as a field argument must check whether the
      resource with the provided id exists */
      if (!player) { return new errors.NotFoundError() }
      
      // Check if pickup game exists
      /** 6. A query or mutation which accepts an id as a field argument must check whether the
      resource with the provided id exists */
      if (!pickupGame) { return new errors.NotFoundError() }
      
      // Check if pickup game capacity has exceded
      /** 4. Players cannot be added to pickup games, if the maximum capacity has been reached for that basketball field */
      const basketballField = await service.getBasketballField(pickupGame.basketballFieldId)
      const pickupGames = await PickupGamePlayers.find({ pickupGameId })
      if (basketballField.capacity <= pickupGames.length) { return new errors.PickupGameExceedMaximumError() }
      
      // Check if pickup game has passed
      /** 3. Players cannot be added to pickup games that have already passed */
      const dateHasPassed = moment.duration(moment(pickupGame.start).diff(moment(new Date()))).asMinutes() < 0
      if (dateHasPassed) { return new errors.PickupGameAlreadyPassedError() }

      // Check if player is registered to pickup game
      const alreadyRegistered = await PickupGamePlayers.find({ playerId, pickupGameId })
      if (alreadyRegistered.length !== 0) { return new errors.PickupGamePlayerAlreadyRegisteredError() }

      // TODO: check if player is registered to a game that overlaps

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
      const pickupGame = await PickupGame.findOne({ _id: pickupGameId })

      // Check if player exists
      /** 6. A query or mutation which accepts an id as a field argument must check whether the
      resource with the provided id exists */
      if (!player) { return new errors.NotFoundError() }
      
      // Check if pickup game exists
      /** 6. A query or mutation which accepts an id as a field argument must check whether the
      resource with the provided id exists */
      if (!pickupGame) { return new errors.NotFoundError() }

      // Check if player is registered to pickup game
      const alreadyRegistered = await PickupGamePlayers.find({ playerId, pickupGameId })
      if (alreadyRegistered.length === 0) { return new errors.PickupGamePlayerNotRegisteredError() }

      // Check if pickup game has passed
      /** 5. Players cannot be removed from pickup games that have already passed  */
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
      location: async (parent, args, { services }) => {
        const service = services.basketballFieldService

        return await service.getBasketballField(parent.basketballFieldId)
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

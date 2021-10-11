const { Types: { ObjectId } } = require('mongoose')
const errors = require('../errors')

module.exports = {
  queries: {
    allPlayers: async (parent, args, { db }) => {
      const { Player } = db
      const players = await Player.find({ deleted: false })
        .catch(err => { throw new Error(err) })

      return players
    },
    player: async (parent, { id }, { db }) => {
      const { Player } = db
      const player = await Player.findOne({ _id: ObjectId(id), deleted: false })

      // Check if player exists
      if (!player) { return new errors.NotFoundError() }

      return player
    }
  },
  mutations: {
    createPlayer: async (parent, { input }, { db }) => {
      const { Player } = db
      const { name } = input
      const newPlayer = new Player({
        name
      })

      await newPlayer.save()
        .catch(err => { throw new Error(err) })

      return newPlayer
    },
    updatePlayer: async (parent, { id, name }, { db }) => {
      const { Player } = db

      const player = await Player.findOne({ _id: ObjectId(id) })

      // Check if player exists
      if (!player) { return new errors.NotFoundError() }

      player.name = name
      await player.save()
        .catch(err => { throw new Error(err) })

      return player
    },
    removePlayer: async (parent, { id }, { db }) => {
      const { Player } = db
      const player = await Player.findOne({ _id: ObjectId(id), deleted: false })

      // Check if player exists
      if (!player) { return new errors.NotFoundError() }

      player.deleted = true
      await player.save()
        .catch(err => { throw new Error(err) })

      return true
    }
  },
  types: {
    Player: {
      playedGames: async (parent, args, { db }) => {
        const { PickupGamePlayers, PickupGame } = db
        const pickupGameIds = await PickupGamePlayers.find({ playerId: parent.id }).then((d) => d.map(i => i.pickupGameId))

        return await PickupGame.find({ _id: { $in: pickupGameIds } })
      }
    }
  }
}

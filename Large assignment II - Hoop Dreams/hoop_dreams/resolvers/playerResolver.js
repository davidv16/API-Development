const { Types: { ObjectId } } = require('mongoose')
const errors = require('../errors')

module.exports = {
  queries: {
    allPlayers: (parent, args, { db }) => {
      const { Player } = db

      return Player.find({ deleted: false })
        .then(players => {
          return players
        }).catch(err => {
          console.error(err)
        })
    },
    player: (parent, { id }, { db }) => {
      const { Player } = db

      return Player.findOne({ _id: ObjectId(id) })
        .then(player => {
          return player
        }).catch(err => {
          console.error(err)
        })
    }
  },
  mutations: {
    createPlayer: (parent, { input }, { db }) => {
      const { Player } = db
      const { name } = input
      const newPlayer = new Player({
        name
      })

      return newPlayer.save()
        .then(result => {
          return { ...result._doc }
        }).catch(err => {
          console.error(err)
        })
    },
    updatePlayer: async (parent, { id, name }, { db }) => {
      const { Player } = db

      const player = await Player.findOne({ _id: ObjectId(id) })

      // Check if player exists
      /** 6. A query or mutation which accepts an id as a field argument must check whether the
    resource with the provided id exists */
      if (!player) { return new errors.NotFoundError() }

      player.name = name
      player.save()

      return player
    },
    removePlayer: async (parent, { id }, { db }) => {
      const { Player } = db

      return await Player.findByIdAndUpdate(ObjectId(id), { deleted: true })
        .then(() => { return true })
        .catch(() => { return new errors.NoFoundError() })
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

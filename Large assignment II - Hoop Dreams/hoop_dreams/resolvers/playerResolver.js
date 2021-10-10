const { ObjectId } = require('mongodb')

module.exports = {
  queries: {
    allPlayers: (parent, args, {db}) => {
      return db.Player.find()
        .then(players => {
          return players
        }).catch(err => {
          console.error(err)
        })
    },
    player: (parent, {id}, {db}) => {
      return db.Player.findOne(ObjectId(id))
        .then(player => {
          return player
        }).catch(err => {
          console.error(err)
        })
    }
  },
  mutations: {
    createPlayer: () => {},
    updatePlayer: () => {},
    removePlayer: () => {}
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


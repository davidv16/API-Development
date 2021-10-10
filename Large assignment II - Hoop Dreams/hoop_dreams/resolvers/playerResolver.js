const { ObjectId } = require('mongodb')
const { Player } = require('../data/db')

module.exports = {
  queries: {
    allPlayers: () => {
      return Player.find()
        .then(players => {
          return players
        }).catch(err => {
          console.error(err)
        })
    },
    player: (parent, { id }) => {
      return Player.findOne({ _id: ObjectId(id) })
        .then(player => {
          return player
        }).catch(err => {
          console.error(err)
        })
    }
  },
  mutations: {
    createPlayer: (parent , {input}) => {
      const { name } = input;
      const newPlayer = new Player({
        name
      })
      return newPlayer.save()
        .then(result => {
          return {...result._doc}
        }).catch(err => {
          console.error(err)
        })
    },
    updatePlayer: (parent, {id, name}) => {
      //const { id, name } = input;
      return Player.findByIdAndUpdate(id, name)
        .then(result => {
          return {...result._doc}
        }).catch(err => {
          console.error(err)
        })
    },
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

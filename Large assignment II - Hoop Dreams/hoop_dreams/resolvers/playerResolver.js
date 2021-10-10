const { ObjectId } = require('mongodb')
const { Player } = require('../data/schemas/playerSchema')

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
    createPlayer: (parent , args, context, info) => {
      const { name } = args.input;
      const newPlayer = new Player({
        name
      })
      return newPlayer.save()
        .then(result => {
          return{...result._doc}
        }).catch(err => {
          console.error(err)
        })
    },
    updatePlayer: () => {},
    removePlayer: () => {}
  }
}


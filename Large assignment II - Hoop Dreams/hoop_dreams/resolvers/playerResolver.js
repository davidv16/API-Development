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
  }
}


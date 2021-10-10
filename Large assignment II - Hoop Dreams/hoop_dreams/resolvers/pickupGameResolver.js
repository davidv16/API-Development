const { ObjectId } = require('mongodb')

module.exports = {
  queries: {
    allPickupGames: (parent, args, {db}) => {
      return db.PickupGame.find()
        .then(pickupGames => {
          return pickupGames
        }).catch(err => {
          console.error(err)
        })
      },
      pickupGame: (parent, {id}, {db}) => {
        return db.PickupGame.findOne(ObjectId(id))
          .then(pickupGame => {
            return pickupGame
          }).catch(err => {
            console.error(err)
          })
      }
  },
    
  mutations: {
    createPickupGame: () => {},
    removePickupGame: () => {},
    addPlayerToPickupGame: () => {},
    removePlayerFromPickupGame: () => {}


  }
}

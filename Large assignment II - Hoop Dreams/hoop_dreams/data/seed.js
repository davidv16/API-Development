const { Player, PickupGame, PickupGamePlayers, connection } = require('./db')
const cliProgress = require('cli-progress')

const bar = new cliProgress.Bar({}, cliProgress.Presets.rect)
bar.start(100, 0)

// Drop all collections before execution
Object.keys(connection.collections).forEach(collection => {
  if (collection === 'PickupGamePlayers') { PickupGamePlayers.collection.drop() }
  if (collection === 'PickupGame') { PickupGame.collection.drop() }
  if (collection === 'Player') { Player.collection.drop() }
})

// Create players
Player.insertMany([
  { name: 'Einar' },
  { name: 'Davíð' },
  { name: 'Michael Jordan' },
  { name: 'Magic Johnson' },
  { name: 'Shaquille O\'Neal' },
  { name: 'Charles Barkley' }
], (err, players) => {
  if (err) { throw new Error(err) }

  console.log(players)
  bar.update(33);
  // Create pickup games
  PickupGame.insertMany([
    {
      start: new Date('2021-01-01'),
      end: new Date('2021-01-02'),
      locationId: 'ef42039e-77bc-40a3-8121-c2a5424ebcdb',
      hostId: players[0]._id.toString()
    },
    {
      start: new Date('2021-01-02'),
      end: new Date('2021-01-03'),
      locationId: 'ef42039e-77bc-40a3-8121-c2a5424ebcdb',
      hostId: players[1]._id.toString()
    }
  ], (err, games) => {
    if (err) { throw new Error(err) }

    console.log(games)
    bar.update(66);
    // Assign players to pickup games
    PickupGamePlayers.insertMany([
      {
        pickupGameId: games[0]._id.toString(),
        playerId: players[0]._id.toString()
      },
      {
        pickupGameId: games[0]._id.toString(),
        playerId: players[2]._id.toString()
      },
      {
        pickupGameId: games[0]._id.toString(),
        playerId: players[3]._id.toString()
      },
      {
        pickupGameId: games[1]._id.toString(),
        playerId: players[1]._id.toString()
      },
      {
        pickupGameId: games[1]._id.toString(),
        playerId: players[4]._id.toString()
      },
      {
        pickupGameId: games[1]._id.toString(),
        playerId: players[5]._id.toString()
      },
    ], (err, gamePlayers) => {
      if (err) { throw new Error(err) }

      console.log(gamePlayers)
      bar.update(100);
      bar.stop()
      connection.close()
    })
  })
})

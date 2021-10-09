//const { Area, Shark, Attack, connection } = require('./db');
const { Player, PickupGame, connection } = require('./db');
//const { TIGER_SHARK, HAMMERHEAD_SHARK, GREAT_WHITE_SHARK, BULL_SHARK, WHALE_SHARK, NORTH_AMERICA, AUSTRALIA, EUROPE, MEXICO_BEACH, MALIBU_BEACH, GOLD_COAST, BONDI_BEACH, REYKJANES_BEACH } = require('../constants');
const cliProgress = require('cli-progress');

//const getResourceIdByName = (resources, prop, value) => resources.find(elem => elem[prop] === value);
const bar = new cliProgress.Bar({}, cliProgress.Presets.rect);
bar.start(100, 0);

// Drop all collections before execution
Object.keys(connection.collections).forEach(collection => {
    if (collection === 'PickupGame') { PickupGame.collection.drop(); }
    if (collection === 'Player') { Player.collection.drop(); }
});

// CHRISTMAS TREE!!
//           *
//          /.\
//         /..'\
//         /'.'\
//        /.''.'\
//        /.'.'.\
// "'""""/'.''.'.\""'"'"
//       ^^^[_]^^^
PickupGame.insertMany([
    { 
        start: Date.now(),
        end: Date.now(),
        locationId: "basketballfield",
        registeredPlayerIds: ["player1", "player2"],
        host: "player"
     
    },
    { 
        start: Date.now(),
        end: Date.now(),
        locationId: "basketballfield1",
        registeredPlayerIds: ["player2", "player4"],
        host: "player1"
     
    },
    { 
        start: Date.now(),
        end: Date.now(),
        locationId: "basketballfield2",
        registeredPlayerIds: ["player3", "player4"],
        host: "player2"
     
    },
    { 
        start: Date.now(),
        end: Date.now(),
        locationId: "basketballfield3",
        registeredPlayerIds: ["player11", "player21"],
        host: "player3"
     
    }
    
], err => {
    if (err) { throw new Error(err); }
    bar.update(50);
    Player.insertMany([
            {
                name: "magic johnson",
                playedGames: ["game 1", "game 2"]
            },
            {
                name: "michael jordan",
                playedGames: ["game 3", "game 4"]
            },
            {
                name: "shaquille oneal",
                playedGames: ["game 5", "game 6"]
            },
            {
                name: "hinn gaurinn",
                playedGames: ["game 7", "game 8"]
            }
        ], err => {
            if (err) { throw new Error(err); }
            bar.update(100);
    
            bar.stop();
            connection.close();
        
        });

});
    

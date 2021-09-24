const { Area, Shark, Attack, connection } = require('./data/db');
const { TIGER_SHARK, HAMMERHEAD_SHARK, GREAT_WHITE_SHARK, BULL_SHARK } = require('./constants');

// 1.1. Get all sharks

Shark.find({}, (err, sharks) => {
  if(err) { throw new Error(err);}
  console.log(sharks);
});
// 1.2. Get all tiger sharks
Shark.find({"species": "tiger shark"}, (err, AllTigerSharks) => {
  if(err) { throw new Error(err); }
  console.log(AllTigerSharks)
})

// 1.3. Get all tiger and bull sharks
//var filter = {}
//Shark.filter({"species" == "tiger shark" && "species"== "bull shark" }, (err, AllTigerSharks) => {
//  if(err) { throw new Error(err); }
//  console.log(AllTigerSharks)
//})

// 1.4. Get all sharks except great white sharks

// 1.5. Get all sharks that have been known to attack

// 1.6. Get all areas with registered attacks

// 1.7. Get all areas with more than 5 registered attacks

// 1.8. Get the area with the most registered shark attacks

// 1.9. Get the total count of great white shark attacks

// 1.10. Get the total count of hammerhead and tiger shark attacks

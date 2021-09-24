const { Area, Shark, Attack, connection } = require('./data/db')
const { TIGER_SHARK, HAMMERHEAD_SHARK, GREAT_WHITE_SHARK, BULL_SHARK } = require('./constants')

;(async () => {
  // 1.1. Get all sharks
  await Shark.find({}, (err, sharks) => {
    if (err) { throw new Error(err) }
    console.log('1.1. Get all sharks')
    console.log(sharks)
  })
  
  // 1.2. Get all tiger sharks
  await Shark.find({ species: TIGER_SHARK }, (err, sharks) => {
    if (err) { throw new Error(err) }
    console.log('1.2. Get all tiger sharks')
    console.log(sharks)
  })
  
  // 1.3. Get all tiger and bull sharks
  var filter = [TIGER_SHARK, BULL_SHARK]
  await Shark.find({ species: { $in: filter } }, (err, sharks) => {
    if (err) { throw new Error(err) }
    console.log('1.3. Get all tiger and bull sharks')
    console.log(sharks)
  })
  
  // 1.4. Get all sharks except great white sharks
  await Shark.find({ species: { $ne: GREAT_WHITE_SHARK } }, (err, sharks) => {
    if (err) { throw new Error(err) }
    console.log('1.4. Get all sharks except great white sharks')
    console.log(sharks)
  })
  
  // 1.5. Get all sharks that have been known to attack
  await Shark.aggregate([
    {
      $lookup: {
        from: 'attacks',
        localField: '_id',
        foreignField: 'sharkId',
        as: 'attacks'
      }
    },
    {
      $unwind: {
        path: '$attacks',
        preserveNullAndEmptyArrays: false
      }
    },
    {
      $group: {
        _id: '$species'
      }
    },
  ], (err, sharks) => {
    if (err) { throw new Error(err) }
    //console.log(sharks.filter(shark => shark.attacks.length > 0))
    console.log('1.5. Get all sharks that have been known to attack')
    console.log(sharks)
  })
  
  // 1.6. Get all areas with registered attacks
  await Area.aggregate([
    {
      $lookup: {
        from: 'attacks',
        localField: '_id',
        foreignField: 'areaId',
        as: 'attacks'
      }
    },
    {
      $unwind: {
        path: '$attacks',
        preserveNullAndEmptyArrays: false
      }
    },
    {
      $group: {
        _id: '$name'
      }
    },
  ], (err, sharks) => {
    if (err) { throw new Error(err) }
    //console.log(sharks.filter(shark => shark.attacks.length > 0))
    console.log('1.6. Get all areas with registered attacks')
    console.log(sharks)
  })
  
  // 1.7. Get all areas with more than 5 registered attacks
  await Area.aggregate([
    {
      $lookup: {
        from: 'attacks',
        localField: '_id',
        foreignField: 'areaId',
        as: 'attacks'
      }
    },
    {
      $unwind: {
        path: '$attacks',
        preserveNullAndEmptyArrays: false
      }
    },
    {
      $group: {
        _id: '$name',
        count: { $sum: 1 }
      }
    },
    {
      $match: {
        count: { $gt: 5 }
      }
    },
    {
      $project: { count: 0 }
    }
  ], (err, sharks) => {
    if (err) { throw new Error(err) }
    //console.log(sharks.filter(shark => shark.attacks.length > 0))
    console.log('1.7. Get all areas with more than 5 registered attacks')
    console.log(sharks)
  })
  
  // 1.8. Get the area with the most registered shark attacks
  await Area.aggregate([
    {
      $lookup: {
        from: 'attacks',
        localField: '_id',
        foreignField: 'areaId',
        as: 'attacks'
      }
    },
    {
      $unwind: {
        path: '$attacks',
        preserveNullAndEmptyArrays: false
      }
    },
    {
      $group: {
        _id: '$name',
        count: { $sum: 1 }
      }
    },
    {
      $sort: { count: -1 }
    },
    {
      $limit: 1
    },
    {
      $project: { count: 0 }
    }
  ], (err, sharks) => {
    if (err) { throw new Error(err) }
    console.log('1.8. Get the area with the most registered shark attacks')
    //console.log(sharks.filter(shark => shark.attacks.length > 0))
    console.log(sharks)
  })
  
  // 1.9. Get the total count of great white shark attacks
  await Shark.aggregate([
    {
      $match: { species: { $eq: GREAT_WHITE_SHARK } }
    },
    {
      $lookup: {
        from: 'attacks',
        localField: '_id',
        foreignField: 'sharkId',
        as: 'attacks'
      }
    },
    {
      $unwind: {
        path: '$attacks',
        preserveNullAndEmptyArrays: false
      }
    },
    {
      $group: {
        _id: '$species',
        count: { $sum: 1 }
      }
    },
    {
      $project: {
        _id: 0
      }
    }
  ], (err, sharks) => {
    if (err) { throw new Error(err) }
    console.log('1.9. Get the total count of great white shark attacks')
    //console.log(sharks.filter(shark => shark.attacks.length > 0))
    console.log(sharks)
  })
  
  // 1.10. Get the total count of hammerhead and tiger shark attacks
  await Shark.aggregate([
    {
      $match: { species: { $in: [HAMMERHEAD_SHARK, TIGER_SHARK] } }
    },
    {
      $lookup: {
        from: 'attacks',
        localField: '_id',
        foreignField: 'sharkId',
        as: 'attacks'
      }
    },
    {
      $unwind: {
        path: '$attacks',
        preserveNullAndEmptyArrays: false
      }
    },
    {
      $group: {
        _id: null,
        count: { $sum: 1 }
      }
    },
    {
      $project: { _id: 0 }
    }
  ], (err, sharks) => {
    if (err) { throw new Error(err) }
    console.log('1.10. Get the total count of hammerhead and tiger shark attacks')
    //console.log(sharks.filter(shark => shark.attacks.length > 0))
    console.log(sharks)
  })
})()

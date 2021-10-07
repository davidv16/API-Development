const basketballFieldService = require('../services/basketballFieldService')

module.exports = {
  queries: {
    allBasketballFields: () => basketballFieldService.getAllBasketballFields(),
    basketballField: (parent, args, context) => basketballFieldService.getBasketballField(args.id)
  }
}

//const basketballFieldService = require('../services/basketballFieldService')

module.exports = {
  queries: {
    allBasketballFields: (parent, args, context) => context.services.basketballFieldService.getAllBasketballFields(),
    basketballField: (parent, args, context) => context.services.basketballFieldService.getBasketballField(args.id)
  }
}

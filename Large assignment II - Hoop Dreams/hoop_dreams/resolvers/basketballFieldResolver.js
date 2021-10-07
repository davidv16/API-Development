const basketballFieldService = require('../services/basketballFieldService')

module.exports = {
  queries: {
    allBasketballFields: () => basketballFieldService.getAllBasketballFields(),
    basketballField: (parent, args, context) => {
      console.log(args)

      return basketballFieldService.getBasketballField(args.id)
    }
  }
}

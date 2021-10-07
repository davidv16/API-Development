//const basketballFieldService = require('../services/basketballFieldService')

module.exports = {
  queries: {
    allBasketballFields: async (parent, args, context) => {
      const service = context.services.basketballFieldService
      const basketballFields = await service.getAllBasketballFields()

      console.log(basketballFields)

      for (const field of basketballFields) {
        // TODO: add pickup games to field
        field.pickupGames = [
          { id: 'asdf' }
        ]
      }

      return basketballFields
    },
    basketballField: async (parent, args, context) => {
      const service = context.services.basketballFieldService
      const basketballField = await service.getBasketballField(args.id)

      // TODO: add pickup games to basketball field
      basketballField.pickupGames = [
        { id: 'asdf' }
      ]

      return basketballField
    }
  }
}

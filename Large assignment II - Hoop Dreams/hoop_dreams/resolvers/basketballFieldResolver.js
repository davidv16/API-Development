const errors = require('../errors')

module.exports = {
  queries: {
    allBasketballFields: async (parent, args, { services }) => {
      const service = services.basketballFieldService
      const basketballFields = await service.getAllBasketballFields()

      return basketballFields
    },
    basketballField: async (parent, { id }, { services }) => {
      const service = services.basketballFieldService
      const basketballField = await service.getBasketballField(id)

      console.log(basketballField)

      // Check if basketballField exists
      if (!basketballField) { return new errors.NotFoundError() }

      return basketballField
    }
  },
  types: {
    BasketballField: {
      pickupGames: async ({ id }, args, { db }) => {
        const { PickupGame } = db

        return await PickupGame.find({ basketballFieldId: id, deleted: false })
      }
    }
  }
}

module.exports = {
  queries: {
    allBasketballFields: async (parent, args, { services }) => {
      const service = services.basketballFieldService
      const basketballFields = await service.getAllBasketballFields()

      return basketballFields
    },
    basketballField: async (parent, args, { services }) => {
      const service = services.basketballFieldService
      const basketballField = await service.getBasketballField(args.id)

      // TODO: handle 404 error

      return basketballField
    }
  },
  types: {
    BasketballField: {
      pickupGames: async (parent, args, { db }) => {
        const { PickupGame } = db

        return await PickupGame.find({ locationId: parent.id })
      }
    }
  }
}

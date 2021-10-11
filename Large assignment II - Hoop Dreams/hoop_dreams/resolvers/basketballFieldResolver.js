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
      // done?
      // Check if basketballField exists
      /** 6. A query or mutation which accepts an id as a field argument must check whether the
    resource with the provided id exists */
      if(!basketballField) { return new erros.NotFounderror() }

      return basketballField
    }
  },
  types: {
    BasketballField: {
      pickupGames: async (parent, args, { db }) => {
        const { PickupGame } = db

        return await PickupGame.find({ basketballFieldId: parent.id })
      }
    }
  }
}

const axios = require('axios')

const getAllBasketballFields = () => axios
  .get('https://basketball-fields.herokuapp.com/api/basketball-fields/')
  .then((d) => d.data)

const getBasketballField = (id) => axios
  .get(`https://basketball-fields.herokuapp.com/api/basketball-fields/${id}`)
  .then((d) => d.data)

module.exports = {
  getAllBasketballFields,
  getBasketballField
}

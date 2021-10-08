const inputs = require('./input')
const mutations = require('./mutations')
const queries = require('./queries')
const scalar = require('./scalar')
const types = require('./types')
const enums = require('./enums')

module.exports = `
  ${inputs}
  ${mutations}
  ${queries}
  ${scalar}
  ${types}
  ${enums}
`

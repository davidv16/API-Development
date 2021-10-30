const express = require('express');
const app = express();
const donutService = require('./services/donutService');

const port = process.env.PORT || 5000;

app.get('/api/donuts', async (req, res) => res.json(await donutService.getAllDonuts()));

app.listen(port, () => console.log(`Listening on port ${port}`));

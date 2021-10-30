const loadDatabase = require('./db');

(async () => {
    const db = await loadDatabase();

    // Populate donuts
    const donuts = db.collection('donuts');

    const results = await donuts.insertMany([
        {
            name: 'The Glazer',
            description: 'A glazified mountain of glaze!',
            price: 1.99
        },
        {
            name: 'The Fudger',
            description: 'A total mountain of fudge wrapped in a donut!',
            price: 2.25
        },
        {
            name: 'Cinnamon Twist',
            description: 'A donut with a cinnamon swirl filling topped with caramel',
            price: 2.50
        },
        {
            name: 'Caramel explosion',
            description: 'An all-out caramel donut with a caramel filling and caramel on top, sprinkled with caramel.. mmmm.. caramel..',
            price: 1.85
        },
        {
            name: 'Brownie Donut',
            description: 'A collaboration of a brownie and a donut wrapped in one. Half brownie batter, half donut batter. It\'s amazing!',
            price: 2.45
        }
    ]);

    console.log(results);
})();

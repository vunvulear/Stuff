var express = require('express');
var router = express.Router();

/* GET home page. */
router.get('/', function(req, res, next) {
  // res.cookie("localion","bucharest");
  res.render('index', { title: 'Bucharest.JS meetup' });
});

module.exports = router;

let SlackBot = require('slackbots');
let mongoose = require('mongoose');

let http = require('http');
http.createServer((req, res) => {}).listen(8080); // run on local

/* Create MongoDB Connection */
mongoose.Promise = require('bluebird');
mongoose.connect('mongodb://localhost/slackbot', {
    useUnifiedTopology: true,
    useNewUrlParser: true,
    promiseLibrary: require('bluebird') })
  .then(() =>  console.log('connection succesful'))
  .catch((err) => console.error(err));
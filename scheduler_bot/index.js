let SlackBot = require('slackbots');
let mongoose = require('mongoose');

let bot = new SlackBot({
    token: '',
    name: 'HackaSchedule'
});

bot.on('start', () => {
    // more information about additional params https://api.slack.com/methods/chat.postMessage
    let params = {
    };
    
    // define channel, where bot exist. You can adjust it there https://my.slack.com/services 
    bot.postMessageToChannel(
        'general',
        'Hi!',
        params);
});

bot.on('error', () => {
    console.log(err);
});

bot.on('message', data => {
    if (data.type !== 'message') {
        return;
    }

    bot.postMessageToChannel(
        'general',
        data);
});


/*
let http = require('http');
http.createServer((req, res) => listen(8080, '127.0.0.1')); // run on local

mongoose.Promise = require('bluebird');
mongoose.connect('mongodb://localhost/slackbot', {
    useUnifiedTopology: true,
    useNewUrlParser: true,
    promiseLibrary: require('bluebird') })
  .then(() =>  console.log('connection succesful'))
  .catch((err) => console.error(err));
  */
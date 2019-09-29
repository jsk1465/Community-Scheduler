const SlackBot = require('slackbots');
const mongoose = require('mongoose');
const fs = require('fs');

let output = {
    name: "hello",
    id: "45"
}

const content = JSON.stringify(output);

console.log(content);

let bot = new SlackBot({
    token: '',
    name: 'HackaSchedule'
});

fs.writeFile('users_slots.json', content, (err) => {
    return;
}); 

bot.on('start', () => {
    // more information about additional params https://api.slack.com/methods/chat.postMessage
    let params = {
    };
    
    // define channel, where bot exist. You can adjust it there https://my.slack.com/services 
    bot.postMessageToChannel(
        'testform',
        'Hi!',
        params);
});

bot.on('error', () => {
    console.log(err);
});

bot.on('message', data => {
    let params = {
            
    }

    if (data.bot_id ||data.type !== 'message') {
        console.log("Bot said something");
        return;
    }

    // bot.postMessageToChannel(
    //     'testform',
    //     "Hello",
    //     params);
    console.log(data);
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
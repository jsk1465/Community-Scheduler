const SlackBot = require('slackbots');
const mongoose = require('mongoose');
const fs = require('fs');
let rawData = fs.readFileSync('../../cred.json');
rawData = JSON.parse(rawData).token;
console.log(rawData)

let bot = new SlackBot({
    token: rawData,
    name: 'HackaSchedule'
});

let formData = {
    "text": "Would you like to play a game?",
    "attachments": [
        {
            "text": "Choose a game to play",
            "fallback": "You are unable to choose a game",
            "callback_id": "wopr_game",
            "color": "#3AA3E3",
            "attachment_type": "default",
            "actions": [
                {
                    "name": "game",
                    "text": "Chess",
                    "type": "button",
                    "value": "chess"
                },
                {
                    "name": "game",
                    "text": "Falken's Maze",
                    "type": "button",
                    "value": "maze"
                },
                {
                    "name": "game",
                    "text": "Thermonuclear War",
                    "style": "danger",
                    "type": "button",
                    "value": "war",
                    "confirm": {
                        "title": "Are you sure?",
                        "text": "Wouldn't you prefer a good game of chess?",
                        "ok_text": "Yes",
                        "dismiss_text": "No"
                    }
                }
            ]
        }
    ]
}


bot.on('start', () => {
    // more information about additional params https://api.slack.com/methods/chat.postMessage
    let params = {};
    console.log('started');
    
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
    if (data.type !== 'message') {
        return;
    }
    let msg = data.text;
    let usr = data.user;
    if(msg=='\\h'){
        console.log('found someone to help!')
        msg = "test"
        bot.postMessageToChannel(
            'testform',
            msg,
            formData);
    }
    console.log(data);
});
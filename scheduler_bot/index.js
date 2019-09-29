const SlackBot = require('slackbots');
const mongoose = require('mongoose');
const fs = require('fs');
let rawData = fs.readFileSync('../../cred.json');
rawData = JSON.parse(rawData).token;
// console.log(rawData)

let bot = new SlackBot({
    token: rawData,
    name: 'HackaSchedule'
});

let atchData = JSON.parse(fs.readFileSync('../InputForms.json'));
let formData = atchData;
console.log(formData.attachments);

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
    if(msg==='\\h'){
        console.log('found someone to help!')
        msg = "test"
        bot.postMessageToChannel(
            'testform',
            msg,
            formData); 
    }
    else{
        console.log(data);
    }
});
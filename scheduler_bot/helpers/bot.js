let SlackBot = require('slackbots');
let mongoose = require('mongoose');
let Badword = require('../models/user_schedules.js');

let bot = new SlackBot({
    token: 'BOT_API_KEY',
    name: 'SlackBot'
});

exports.run = () => {
  bot.on('start', onStart);
  bot.on('message', onMessage);
}

let onStart = () => {
  console.log('Bot started');
  bot.postMessageToChannel(channel.name,
      'I have joined',
      {as_user: true});
}

let onMessage = (message) => {
    users = [];
    channels = [];
    let botUsers = bot.getUsers();
    users = botUsers._value.members;
    let botChannels = bot.getChannels();
    channels = botChannels._value.channels;

    // Do something
}
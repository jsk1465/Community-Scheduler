let SlackBot = require('slackbots');
let mongoose = require('mongoose');
let UserSchedules = require('../models/user_schedules.js');

let bot = new SlackBot({
    token: 'xoxb-776876304389-776520912580-yxYi8KWfEQcpWqmXksDJE4eM',
    name: 'hackaschedule'
});

exports.run = () => {
  bot.on('start', onStart);
  bot.on('message', onMessage);
}

let onStart = () => {
  console.log('Bot started');
  bot.postMessageToChannel('general',
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
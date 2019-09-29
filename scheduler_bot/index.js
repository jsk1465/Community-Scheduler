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
// let formData = [{
// 	"blocks": [
// 		{
// 			"type": "actions",
// 			"elements": [
// 				{
// 					"type": "static_select",
// 					"placeholder": {
// 						"type": "plain_text",
// 						"text": "Select an item",
// 						"emoji": true
// 					},
// 					"options": [
// 						{
// 							"text": {
// 								"type": "plain_text",
// 								"text": "Excellent item 1",
// 								"emoji": true
// 							},
// 							"value": "value-0"
// 						},
// 						{
// 							"text": {
// 								"type": "plain_text",
// 								"text": "Fantastic item 2",
// 								"emoji": true
// 							},
// 							"value": "value-1"
// 						},
// 						{
// 							"text": {
// 								"type": "plain_text",
// 								"text": "Nifty item 3",
// 								"emoji": true
// 							},
// 							"value": "value-2"
// 						},
// 						{
// 							"text": {
// 								"type": "plain_text",
// 								"text": "Pretty good item 4",
// 								"emoji": true
// 							},
// 							"value": "value-3"
// 						}
// 					]
// 				},
// 				{
// 					"type": "static_select",
// 					"placeholder": {
// 						"type": "plain_text",
// 						"text": "Select an item",
// 						"emoji": true
// 					},
// 					"options": [
// 						{
// 							"text": {
// 								"type": "plain_text",
// 								"text": "Excellent item 1",
// 								"emoji": true
// 							},
// 							"value": "value-0"
// 						},
// 						{
// 							"text": {
// 								"type": "plain_text",
// 								"text": "Fantastic item 2",
// 								"emoji": true
// 							},
// 							"value": "value-1"
// 						},
// 						{
// 							"text": {
// 								"type": "plain_text",
// 								"text": "Nifty item 3",
// 								"emoji": true
// 							},
// 							"value": "value-2"
// 						},
// 						{
// 							"text": {
// 								"type": "plain_text",
// 								"text": "Pretty good item 4",
// 								"emoji": true
// 							},
// 							"value": "value-3"
// 						}
// 					]
// 				},
// 				{
// 					"type": "static_select",
// 					"placeholder": {
// 						"type": "plain_text",
// 						"text": "Select an item",
// 						"emoji": true
// 					},
// 					"options": [
// 						{
// 							"text": {
// 								"type": "plain_text",
// 								"text": "Excellent item 1",
// 								"emoji": true
// 							},
// 							"value": "value-0"
// 						},
// 						{
// 							"text": {
// 								"type": "plain_text",
// 								"text": "Fantastic item 2",
// 								"emoji": true
// 							},
// 							"value": "value-1"
// 						},
// 						{
// 							"text": {
// 								"type": "plain_text",
// 								"text": "Nifty item 3",
// 								"emoji": true
// 							},
// 							"value": "value-2"
// 						},
// 						{
// 							"text": {
// 								"type": "plain_text",
// 								"text": "Pretty good item 4",
// 								"emoji": true
// 							},
// 							"value": "value-3"
// 						}
// 					]
// 				},
// 				{
// 					"type": "static_select",
// 					"placeholder": {
// 						"type": "plain_text",
// 						"text": "Select an item",
// 						"emoji": true
// 					},
// 					"options": [
// 						{
// 							"text": {
// 								"type": "plain_text",
// 								"text": "Excellent item 1",
// 								"emoji": true
// 							},
// 							"value": "value-0"
// 						},
// 						{
// 							"text": {
// 								"type": "plain_text",
// 								"text": "Fantastic item 2",
// 								"emoji": true
// 							},
// 							"value": "value-1"
// 						},
// 						{
// 							"text": {
// 								"type": "plain_text",
// 								"text": "Nifty item 3",
// 								"emoji": true
// 							},
// 							"value": "value-2"
// 						},
// 						{
// 							"text": {
// 								"type": "plain_text",
// 								"text": "Pretty good item 4",
// 								"emoji": true
// 							},
// 							"value": "value-3"
// 						}
// 					]
// 				}
// 			]
// 		}
// 	]
// }]

formData = {attachments:formData}

console.log(formData.attachments);

bot.on('start', () => {
    // more information about additional params https://api.slack.com/methods/chat.postMessage
    let params = {};
    console.log('started');
    // console.log(bot.getUsers()._value.members)
    // define channel, where bot exist. You can adjust it there https://my.slack.com/services 
    // bot.postMessageToChannel(
    //     'testform',
    //     'Hi!',
    //     params);
});

bot.on('message', data => {
    if (data.type !== 'message') {
        return;
    }
    let msg = data.text;
    let usr = data.user;
    if(msg==='\\h'){
        console.log('found someone to help!')
        msg = "Hello person :)"
        bot.postMessageToChannel(
            'testform',
            msg,
            formData); 
    }
    else{
        console.log(data);
    }
});

bot.on('open', data => {
    // console.log(data);
    console.log('open biiiiii');
});
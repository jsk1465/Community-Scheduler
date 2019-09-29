let mongoose = require('mongoose');

let scheduleSchema = new mongoose.Schema({
  user_id: String,
  gender: String,
  start: { type: Date},
  end: {type: Date},
});

module.exports = mongoose.model('User Schedule', UserScheduleSchema);
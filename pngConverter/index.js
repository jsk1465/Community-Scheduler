var PdfPrinter = require('./src/printer');
var fs = require('fs');


var fonts = {
	Roboto: {
		normal: 'fonts/Roboto-Regular.ttf',
		bold: 'fonts/Roboto-Medium.ttf',
		italics: 'fonts/Roboto-Italic.ttf',
		bolditalics: 'fonts/Roboto-MediumItalic.ttf'
	}
};



function getAnomSlot(){
    return {text:'XXXXXXXXXX',link:'Anomynous',alignment:'center'}
}

function getPersonSlot(fullName){
    // var names = fullName.split(' ');
    // var fName = names[0]
    // var lName = names[1]
    // return {text:fName[0] + lName[0],link:fullName,alignment:'center'};
    return {text:fullName,link:fullName,alignment:'center'};
}

function Y(numInputs){
    let inputs = JSON.parse(fs.readFileSync('../scheduler_server/SampleInputs/inputs'+numInputs+'.json'));
    let outputs = JSON.parse(fs.readFileSync('../scheduler_server/SampleInputs/outputs'+numInputs+'.json'));
    X(numInputs,outputs);
    for(i=0;i<inputs.length;i++){
        ZeroIsMyHero(numInputs,inputs[i]);
    }
    
}

function X(numInputs,data){
    var printer = new PdfPrinter(fonts);
    var bod = [[ '',{text:'Monday',alignment:'center'},{text:'Tuesday',alignment:'center'}, {text:'Wednesday',alignment:'center'},{text:'Thursday',alignment:'center'},{text:'Friday',alignment:'center'}]];
    let cur = 9*60;
    var daysOfTheWeek = ['Monday','Tuesday','Wednesday','Thursday','Friday'];
    while(cur<17*60){
        // cc = []
        bod.push([])
        if(cur%60==0){
            w = cur/60;
            bod[bod.length-1].push(w+':00');
        }
        else{
            bod[bod.length-1].push(' ');
        }
        for(var i=0;i<daysOfTheWeek.length;i++){
            // console.log(data.schedule[daysOfTheWeek[i]])
            var sp = data.schedule[daysOfTheWeek[i]][0].start.split('T');
            var hr = sp[1].split(':')[0]
            var min = sp[1].split(':')[1].split(':')[0];
            if(parseInt(hr)*60+parseInt(min)==cur){
                name = data.schedule[daysOfTheWeek[i]][0].user.id;
                data.schedule[daysOfTheWeek[i]].reverse();
                data.schedule[daysOfTheWeek[i]].pop();
                data.schedule[daysOfTheWeek[i]].reverse();
                bod[bod.length - 1].push(getPersonSlot(name));
            }
            else{
                bod[bod.length - 1].push('');
            }
        }
        cur+=30;
        // console.log(cur);
        // console.log(bod[bod.length-1].length);
        // console.log(bod[1]);
    }

    var dd = {
        content:[
            {text:"Master Calender",alignment:'center',fontSize:25},
            {
                layout: {
                    hLineColor: function (rowIndex, node, columnIndex) {
                        return (rowIndex % 2 === 0) && rowIndex != 0 && rowIndex !=18 ? '#CCCCCC' : null;
                    }
                },
                table: {
                  // headers are automatically repeated if the table spans over multiple pages
                  // you can declare how many rows should be treated as headers
                  headerRows: 1,
                  widths: [50, '*', '*', '*', '*' ,'*'],
                  body: bod,
                }
              }
        ],
        // pageSize: 'A5',
        pageSize: {
            width: 595.28,
            height: 'auto'
        },
        orientation:'landscape'
    };
    var pdfDoc = printer.createPdfKitDocument(dd);
    pdfDoc.pipe(fs.createWriteStream('final_calender'+numInputs+'.pdf'));
    pdfDoc.end();
}

function ZeroIsMyHero(numInputs,data){
    console.log(data)
    var printer = new PdfPrinter(fonts);
    var bod = [[ '',{text:'Monday',alignment:'center'},{text:'Tuesday',alignment:'center'}, {text:'Wednesday',alignment:'center'},{text:'Thursday',alignment:'center'},{text:'Friday',alignment:'center'}]];
    let cur = 9*60;
    var daysOfTheWeek = ['Monday','Tuesday','Wednesday','Thursday','Friday'];
    let name = data.user.id;
    while(cur<17*60){
        // cc = []
        bod.push([])
        if(cur%60==0){
            w = cur/60;
            bod[bod.length-1].push(w+':00');
        }
        else{
            bod[bod.length-1].push(' ');
        }
        for(var i=0;i<daysOfTheWeek.length;i++){
            // console.log(data.schedule[daysOfTheWeek[i]])
            try{
                var sp = data.timeslots[daysOfTheWeek[i]][0].start.split('T');
            }
            catch(e){
                bod[bod.length - 1].push('');
                continue;
            }

            var hr = sp[1].split(':')[0];
            var min = sp[1].split(':')[1].split(':')[0];
            if(parseInt(hr)*60+parseInt(min)==cur){
                data.timeslots[daysOfTheWeek[i]].reverse();
                data.timeslots[daysOfTheWeek[i]].pop();
                data.timeslots[daysOfTheWeek[i]].reverse();
                bod[bod.length - 1].push(getPersonSlot(name));
            }
            else{
                bod[bod.length - 1].push('');
            }
        }
        cur+=30;
        console.log(cur);
        console.log(bod[bod.length-1].length);
        console.log(bod[1]);
    }

    var dd = {
        content:[
            {text:"Ideal Calender for " + name,alignment:'center',fontSize:25},
            {
                layout: {
                    hLineColor: function (rowIndex, node, columnIndex) {
                        return (rowIndex % 2 === 0) && rowIndex != 0 && rowIndex !=18 ? '#CCCCCC' : null;
                    }
                },
                table: {
                  // headers are automatically repeated if the table spans over multiple pages
                  // you can declare how many rows should be treated as headers
                  headerRows: 1,
                  widths: [50, '*', '*', '*', '*' ,'*'],
                  body: bod,
                }
              }
        ],
        // pageSize: 'A5',
        pageSize: {
            width: 595.28,
            height: 'auto'
        },
        orientation:'landscape'
    };
    var pdfDoc = printer.createPdfKitDocument(dd);
    pdfDoc.pipe(fs.createWriteStream('ideal_calender_' + name + '_' + numInputs + '.pdf'));
    pdfDoc.end();
}
Y(20);
Y(40);
Y(80);
Y(200);

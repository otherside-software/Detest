var page = require('webpage').create();

var i = 0;

function log() {
    console.log('try ' + i++);
    if (i > 10) {
        phantom.exit();
    }

    setTimeout(log, 1000);
}

setTimeout(log, 1000);
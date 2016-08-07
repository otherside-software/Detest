var page = require('webpage').create();

page.onConsoleMessage = function (msg) {
    try {
        var deserialized = JSON.parse(msg);
        if (deserialized.close) {
            phantom.exit();
        }
        console.log(msg);
    }
    catch (exc) { //ignore
    }
};

page.open("harness.html", function (status) {
    if (status !== "success") {
        console.error("failed to open test harness, exiting");
        phantom.exit();
    }
});

//TODO:
// - unhardcode harness.html?
// - debounce loop to force a close?
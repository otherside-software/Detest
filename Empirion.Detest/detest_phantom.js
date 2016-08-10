var page = require("webpage").create(),
    system = require("system"),
    timeoutHandle;

if (system.args.length < 2) {
    console.error("not enough parameters");
    phantom.exit(2);
}

page.onConsoleMessage = function (msg) {
    try {
        var deserialized = JSON.parse(msg);
        if (deserialized.close) {
            phantom.exit();
        }
        console.log(msg);

        window.clearTimeout(timeoutHandle);
        timeoutHandle = setTimeout(function () {
            phantom.exit(4);
        }, 10000);
    }
    catch (exc) { //ignore
    }
};

page.open(system.args[1], function (status) {
    if (status !== "success") {
        console.error("failed to open test harness, exiting");
        phantom.exit(3);
    }

    timeoutHandle = setTimeout(function () {
        phantom.exit(4);
    }, 10000);
});
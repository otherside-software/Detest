define([
    "mocha",
    "detest_interop"
], function ( localMocha, interop ) {
    "use strict";

    mocha.setup("bdd");
    console.log(mocha, localMocha)

    var reporter = function (runner) {
        runner.on("pass", function (test) {
            interop.report({
                suite: test.parent.fullTitle(),
                test: test.title,
                passed: true,
                duration: test.duration
            });
        });

        runner.on("fail", function (test, err) {
            interop.report({
                suite: test.parent.fullTitle(),
                test: test.title,
                passed: false,
                duration: test.duration
            });

        });

        runner.on("end", function () {
            interop.report({
                close: true
            });
        });
    }

    mocha.reporter(reporter);

    return function (files) {
        require(files, function () {
            mocha.run();
        });

    }
});
var filename = process.argv[2];
if (!filename) {
    console.error("missing filename of file to analyze");
}

var analyzer = require("../esprima_analyzer");

analyzer.analyze(filename, "describe", "it");
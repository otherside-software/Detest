var fs = require("fs");
var esprima = require("esprima");
var estraverse = require("estraverse");

var filename = process.argv[2];
if (!filename) {
    console.error("missing filename of file to analyze");
}

var ast = esprima.parse(fs.readFileSync(filename));

function isNamedExpression( node, name ) {
    return node.type === "ExpressionStatement"
        && node.expression.callee
        && node.expression.callee.name === name;
}

function indentString(number) {
    var str = "";
    for(var i = 0; i < number; i++) {
        str += "  ";
    }
    return str;
}
var stack = [];

estraverse.traverse(ast, {
    enter: function (node) {
        if (isNamedExpression(node, "describe")) {
            stack.push(node.expression.arguments[0].value);
        }

        if (isNamedExpression(node, "it")) {
            console.log(stack.join(" ") + " " + node.expression.arguments[0].value);
        }
    },
    leave: function (node) {
        if (isNamedExpression(node, "describe")) {
            stack.pop();
        }
    }
})
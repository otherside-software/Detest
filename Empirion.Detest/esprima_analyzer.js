var fs = require("fs");
var esprima = require("esprima");
var estraverse = require("estraverse");
var interop = require("./detest_interop");

exports.analyze = function ( filename, suiteExpressionIdentifier, testExpressionIdentifier ) {

    var ast = esprima.parse(fs.readFileSync(filename), {
        loc: true
    });

    var stack = [];

    function isNamedExpression(node, name) {
        return node.type === "ExpressionStatement"
            && node.expression.callee
            && node.expression.callee.name === name;
    }

    estraverse.traverse(ast, {
        enter: function (node) {
            if (isNamedExpression(node, suiteExpressionIdentifier)) {
                stack.push(node.expression.arguments[0].value);
            }

            if (isNamedExpression(node, testExpressionIdentifier)) {
                interop.report({
                    suite: stack,
                    test: node.expression.arguments[0].value,
                    line: node.expression.loc.start.line
                });
            }
        },
        leave: function (node) {
            if (isNamedExpression(node, suiteExpressionIdentifier)) {
                stack.pop();
            }
        }
    })
}
(function (factory) {
    'use strict';
    if (typeof define === 'function' && define.amd) {
        // Register as an anonymous AMD module:
        define([], factory);
    } else if (typeof exports === 'object') {
        // Node/CommonJS:
        module.exports = factory();
    } else {
        console.error("neither AMD nor CommonJS found");
    }
}(function () {

    return {
        report: function (obj) {
            console.log(JSON.stringify(obj));
        }
    };
}));
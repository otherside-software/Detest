define([
	"chai",
	"can",
	"app/shared/defines"
], function (chai, can, defines) {

    var expect = chai.expect;

    describe("math", function () {
        describe("addition", function () {
            it("6+7 should equal 13", function () {
                expect(13).to.equal(6 + 7);
            });

            it("3+3 should equal 6", function () {
                expect(6).to.equal(3 + 3);
            });
        });

        describe("multiplication", function () {
            it("6*7 should equal 42", function () {
                expect(42).to.equal(6 * 7);
            });
        });

    });
});
define([
	"chai"
], function (chai) {

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
            it("6*7 should equal 42", function (done) {
                expect(42).to.equal(6 * 7);
                setTimeout(function () {
                    done();
                }, 75);
            });

            it("should fail this test", function () {
                expect(1).to.equal(0);
            })
        });

    });
});
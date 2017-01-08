var AttributeShiftDecoratorEffect = function (native, decorated) {
    this.native = native;
    this.decorated = decorated;
}

AttributeShiftDecoratorEffect.prototype.do = function () {
    decorated.do();
    native.do();
}

AttributeShiftDecoratorEffect.prototype.undo = function () {
    decorated.undo();
    native.undo();
}

module.exports = AttributeShiftDecoratorEffect;
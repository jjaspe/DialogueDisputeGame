var RollShiftDecoratorEffect = function(native, decorated){
    this.native = native;
    this.decorated =decorated;
}

RollShiftDecoratorEffect.prototype.do = function(){
    this.decorated.do();
    this.native.do();
}

RollShiftDecoratorEffect.prototype.undo = function(){
    this.decorated.undo();
    this.native.undo();
}

module.exports = RollShiftDecoratorEffect;
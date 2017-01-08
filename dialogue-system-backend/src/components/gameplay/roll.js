function Roll(numberOfDice, diceType) {
    this.diceType = diceType;
    this.numberOfDice = numberOfDice;
}

Roll.prototype.roll = function () {
    var total = 0;
    var nDice = this.numberOfDice;
    var negative = false;
    if(this.diceType<0){
        negative = true;
        this.diceType = -this.diceType;
    }
    for(i=0;i<nDice;i++){
        min = Math.ceil(1);
        max = Math.floor(this.diceType);
        var randResult = Math.floor(Math.random() * (max - min)) + min;
        total += randResult;
    };
    return negative?-total:total;
}

module.exports = Roll;


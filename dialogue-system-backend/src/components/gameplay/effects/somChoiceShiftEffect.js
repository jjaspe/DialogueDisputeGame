var SoMShiftEffect = require('./somShiftEffect');

var SoMChoiceShiftEffect = function (amount, somState) {
    this.listening = false;
    this.value = amount;
    this.som = somState;
}

SoMChoiceShiftEffect.prototype.do = function(){
    this.listening = true;
}

SoMChoiceShiftEffect.prototype.undo = function(){
    this.listening = false;
}

SoMChoiceShiftEffect.prototype.update = function(somDirection){
    console.log({'got som direction':somDirection});
    if(this.listening){
        this.somEffect = new SoMShiftEffect('',this.value,this.som);
        this.somEffect.do();
        this.listening = false;
    }
}

module.exports = SoMChoiceShiftEffect;


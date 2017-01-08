

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
    if(this.listening){
        this.somEffect = new SoMChoiceEffect('',this.value,this.som);
        this.somEffect.do();
        this.listening = false;
    }
}

module.exports = SoMChoiceShiftEffect;


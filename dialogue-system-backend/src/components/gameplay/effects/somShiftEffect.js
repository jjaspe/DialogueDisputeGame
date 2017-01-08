var somDirections = require('../../character/stateOfMind');

var SoMShiftEffect = function (som, direction, amount){
    this.som = som;
    this.direction = direction;
    this.amount = amount;        
}

SoMShiftEffect.prototype.do = function () {
    switch(this.direction){
        case somDirections.Fear:
            this.som.axis1 +=this.value;
            break;
        case somDirections.Anger:
            this.som.axis1 -= this.value;
            break;
        case somDirections.Joy:
            this.som.axis2 += this.value;
            break;
        case somDirections.Sadness:
            this.some.axis2 -= this.value;
            break;
        default:
            break;
    }
}

module.exports = SoMShiftEffect;
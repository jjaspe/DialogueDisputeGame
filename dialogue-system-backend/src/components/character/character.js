var StateOfMind = require('./stateOfMind').StateOfMind;

var Player = function (persuasion, intimidation, subterfuge, perception,
    selfControl, fortitude, resistance) {
    this.perception = perception;
    this.persuasion = persuasion;
    this.intimidation = intimidation;
    this.subterfuge = subterfuge;
    this.selfControl = selfControl;
    this.fortitude = fortitude;
    this.resistance = resistance;

    this.somChoiceObservers = [];
    this.stateOfMind = new StateOfMind(0, 0);
    this.subterfugeArguments = [];

    this.active = false;
}

Player.prototype.chooseSoM = function (somDirection) {
    this.somChoiceObservers.forEach(a => a.update(somDirection));
}

Player.prototype.setActive = function(){
    this.active = true;
}

Player.prototype.addObserver = function (observer) {
    this.somChoiceObservers.push(observer);
}

module.exports = Player;
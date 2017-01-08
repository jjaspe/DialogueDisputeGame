var StateOfMind = require('./stateOfMind').StateOfMind;
var Skill = require('./skill');

var Player = function (persuasion, intimidation, subterfuge, perception,
    selfControl, fortitude, resistance) {
    this.perception = new Skill(perception);
    this.persuasion = new Skill(persuasion);
    this.intimidation = new Skill(intimidation);
    this.subterfuge = new Skill(subterfuge);
    this.selfControl = new Skill(selfControl);
    this.fortitude = new Skill(fortitude);
    this.resistance = new Skill(resistance);

    this.somChoiceObservers = [];
    this.stateOfMind = new StateOfMind(0, 0);
    this.subterfugeArguments = [];

    this.active = false;
}

Player.prototype.chooseSoM = function (somDirection) {
    this.somChoiceObservers.forEach(a => a.update(somDirection));
}

Player.prototype.setActive = function () {
    this.active = true;
}

Player.prototype.addObserver = function (observer) {
    this.somChoiceObservers.push(observer);
}

module.exports = Player;
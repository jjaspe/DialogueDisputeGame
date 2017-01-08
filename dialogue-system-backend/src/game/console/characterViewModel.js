var CharacterViewModel = function (persuasion, intimidation, subterfuge, perception,
    selfControl, fortitude, resistance) {
    this.perception = perception;
    this.persuasion = persuasion;
    this.intimidation = intimidation;
    this.subterfuge = subterfuge;
    this.selfControl = selfControl;
    this.fortitude = fortitude;
    this.resistance = resistance;
    this.stateOfMindX = 0;
    this.stateOfMindY = 0;

    this.active = false;
}

module.exports = CharacterViewModel;
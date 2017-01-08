var AttributeShiftEffect = function (attribute, value, bonus, roll) {
    this.attribute = attribute;
    this.bonus = bonus;
    this.roll = roll;
    this.value = value;
}

AttributeShiftEffect.prototype.do = function () {
    this.lastRoll = this.roll.roll();
    this.attribute.value += this.lastRoll + this.bonus.value
         + this.value;
}

AttributeShiftEffect.prototype.undo = function () {
    this.attribute.value -= lastRoll + this.bonus.value;
}

module.exports = AttributeShiftEffect;
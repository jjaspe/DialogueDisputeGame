function Bonus(value, creator) {
    this.value = value;
    this.creator = creator;
    this.turns = -1;
}

function Bonus(value, creator, turns) {
    this.value = value;
    this.creator = creator;
    this.turns = turns;
}

module.exports = Bonus;
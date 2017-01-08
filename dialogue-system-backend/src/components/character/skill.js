var Bonus = require('../gameplay/bonus.js');

function Skill(initialValue) {
    this.value = initialValue ? initialValue : 0;
    this.bonuses = [];
}

Skill.prototype.getValue = function () {
    return this.bonuses.reduce((p, c) => {
        return p + c;
    }, 0) + this.value;
}

module.exports = Skill;
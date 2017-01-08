var Bonus = require('../gameplay/bonus.js');

function Skill() {
    this.value = 0;
    this.bonuses = [];
}

Skill.prototype.getValue = function(){
    return bonuses.reduce( (p,c) => {
        return p+c;
    },0) + this.value;
}

module.exports = Skill;
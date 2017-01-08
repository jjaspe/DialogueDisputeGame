var RollShiftEffect = function (bonus, arguments) {
    this.arguments = arguments;
    this.bonus = bonus;
}

RollShiftEffect.prototype.do = function(){
    this.arguments.forEach( a=>{
        a.condition.bonuses.push(bonus);
    });
}

RollShiftEffect.prototype.undo = function(){
    this.arguments.forEach( a=>{
        a.condition.bonuses.splice(bonus);
    });
}

module.exports = RollShiftEffect;
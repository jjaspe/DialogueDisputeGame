var ArgumentTypes = require('./argumentTypes');
var Argument = require('./argument');
var ResultTypes = require('../conditions/rollResults');
var DoNothingEffect = require('../effects/doNothingEffect');
var SoMChoiceShiftEffect = require('../effects/somChoiceShiftEffect');
var AttributeShiftEffect = require('../effects/attributeShiftEffect');
var AttributeShiftDecoratorEffect = require('../effects/attributeShiftDecoratorEffect');
var GoAgainArgumentDecorator = require('../effects/goAgainArgumentDecorator');
var AttributeCheckCondition = require('../conditions/attributeCheckCondition');
var SoMCheckCondition = require('../conditions/somCheckCondition');
var RollShiftEffect = require('../effects/rollShiftEffect');
var Roll = require('../roll');
var Bonus = require('../bonus');

var standardRoll = new Roll(2, 6);
var emptyRoll = new Roll(0,0);
var emptyBonus = new Bonus(0,'');

var ArgumentFactory = function (attacker, defender, match) {
    this.attacker = attacker;
    this.defender = defender;
    this.match = match;
}

ArgumentFactory.prototype.manipulate = function () {
    console.log('creating manipulate');
    var attributeCheckCondition = new AttributeCheckCondition(this.attacker.persuasion,
        this.defender.selfControl, standardRoll, [], ResultTypes);

    var failureEffect = new DoNothingEffect();
    var successEffect = new SoMChoiceShiftEffect(1, this.defender.som);
    var greatSuccessEffect = new SoMChoiceShiftEffect(2, this.defender.som);

    this.attacker.addObserver(successEffect);
    this.attacker.addObserver(greatSuccessEffect);

    return new Argument("Manipulate",attributeCheckCondition, failureEffect, successEffect, greatSuccessEffect);
}

ArgumentFactory.prototype.charm = function () {
    var attributeCheckCondition = new AttributeCheckCondition(this.attacker.persuasion,
        this.defender.fortitude, standardRoll, [], ResultTypes);

    var failureEffect = new DoNothingEffect();
    var successEffect = new AttributeShiftEffect(this.match.tone,2,emptyBonus,emptyRoll);
    var greatSuccessEffect = new AttributeShiftEffect(this.match.tone,3,emptyBonus,emptyRoll); 

    return new Argument("Charm",attributeCheckCondition, failureEffect, 
        successEffect, greatSuccessEffect);
}

ArgumentFactory.prototype.convince = function () {
    var attributeCheckCondition = new AttributeCheckCondition(this.attacker.persuasion,
        this.defender.fortitude, standardRoll, [], ResultTypes);

    var failureEffect = new AttributeShiftEffect(this.match.tone,-1,emptyBonus,emptyRoll);
    var successEffect = new AttributeShiftEffect(this.defender.resistance,0,
        emptyBonus,new Roll(1, -6));
    var greatSuccessEffect = new AttributeShiftEffect(this.defender.resistance,-2,
        emptyBonus,new Roll(1, -6));

    return new Argument("Convince",attributeCheckCondition, failureEffect, 
        successEffect, greatSuccessEffect);
}

ArgumentFactory.prototype.empathy = function () {
    var somCheckCondition = new SoMCheckCondition(this.attacker.som,
        this.defender.som, ResultTypes);
    var failureEffect = new DoNothingEffect();
    var successEffect = new AttributeShiftEffect(this.defender.resistance,0,
        emptyBonus,new Roll(1, -6));
    var greatSuccessEffect = new AttributeShiftEffect(this.defender.resistance,-2,
        emptyBonus,new Roll(1, -6));  

    return new Argument("Empathy",somCheckCondition, failureEffect, 
        successEffect, greatSuccessEffect);
}

ArgumentFactory.prototype.scare = function () {
    var attributeCheckCondition = new AttributeCheckCondition(this.attacker.intimidation,
        this.defender.selfControl, standardRoll, [], ResultTypes);
    var failureEffect = new DoNothingEffect();
    var successEffect = new AttributeShiftEffect(this.defender.resistance,0,
        emptyBonus,new Roll(1, -6));
    var greatSuccessEffect = new AttributeShiftEffect(this.defender.resistance,-2,
        emptyBonus,new Roll(1, -6));  

    return new Argument("Scare",attributeCheckCondition, failureEffect, 
        successEffect, greatSuccessEffect);
} 

ArgumentFactory.prototype.taunt = function () {
    var attributeCheckCondition = new AttributeCheckCondition(this.attacker.intimidation,
        this.defender.selfControl, standardRoll, [], ResultTypes);

    var failureEffect = new AttributeShiftEffect(this.match.tone,-2,emptyBonus,emptyRoll);
    var successEffect = new SoMChoiceShiftEffect(1, this.defender.som);
    var greatSuccessEffect = new SoMChoiceShiftEffect(2, this.defender.som);

    return new Argument("Taunt",attributeCheckCondition, failureEffect, 
        successEffect, greatSuccessEffect);
} 

ArgumentFactory.prototype.coerce = function () {
    var attributeCheckCondition = new AttributeCheckCondition(this.attacker.intimidation,
        this.defender.fortitude, standardRoll, [], ResultTypes);

    var failureEffect = new AttributeShiftEffect(this.match.tone,-1,emptyBonus,emptyRoll);
    var successEffect = new AttributeShiftEffect(this.defender.resistance,0,
        emptyBonus,new Roll(1, -6));
    var greatSuccessEffect = new AttributeShiftEffect(this.defender.resistance,-2,
        emptyBonus,new Roll(1, -6));  

    return new Argument("Coerce",attributeCheckCondition, failureEffect, 
        successEffect, greatSuccessEffect);
} 

ArgumentFactory.prototype.trick = function (callback) {
    var attributeCheckCondition = new AttributeCheckCondition(this.attacker.subterfuge,
        this.defender.perception, standardRoll, [], ResultTypes);

    var failureEffect = new AttributeShiftDecoratorEffect(
        new AttributeShiftEffect(this.match.tone,-1,emptyBonus,emptyRoll),
        new RollShiftEffect(new Bonus(-1,'Trick Failure'),
            this.attacker.subterfugeArguments));
    var successEffect = new GoAgainArgumentDecorator(
        new RollShiftEffect(new Bonus(2,'Trick Success',1),this.attacker.subterfugeArguments),
        callback);
    var greatSuccessEffect = new GoAgainArgumentDecorator(
        new RollShiftEffect(new Bonus(3,'Trick Great Success',1),this.attacker.subterfugeArguments),
        callback);
    return new Argument("Trick",attributeCheckCondition, failureEffect, 
        successEffect, greatSuccessEffect);
}

ArgumentFactory.prototype.bluff = function (callback) {
    var attributeCheckCondition = new AttributeCheckCondition(this.attacker.subterfuge,
        this.defender.perception, standardRoll, [], ResultTypes);

    var failureEffect = new RollShiftEffect(new Bonus(-1,'Bluff Failure'),
            this.attacker.subterfugeArguments);
    var successEffect = new AttributeShiftEffect(this.attacker.resistance,0,emptyBonus,
        new Roll(1,6));
    var greatSuccessEffect = new AttributeShiftEffect(this.attacker.resistance,2,emptyBonus,
        new Roll(1,6));
    return new Argument("Bluff",attributeCheckCondition, failureEffect, 
        successEffect, greatSuccessEffect);
}

module.exports = ArgumentFactory;
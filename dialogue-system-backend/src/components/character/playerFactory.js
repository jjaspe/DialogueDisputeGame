var Player = require('./character');
var Match = require('./match');
var ArgumentFactory = require('../gameplay/argument/argumentFactory');


function PlayerFactory(player1, player2, match) {        
    this.player1 = player1?player1:this.buildMockPlayer();
    this.player2 = player2?player2:this.buildMockPlayer();
    this.match = match?match:this.buildMockMatch();
    this.buildPlayerArguments();
}

PlayerFactory.prototype.buildPlayerArguments = function () {
    buildArguments(this.player1, this.player2, this.match);
    buildArguments(this.player2, this.player1, this.match);
}

var buildArguments = function (attacker, defender, match) {
    console.log('building arguments');
    var argFactory = new ArgumentFactory(attacker, defender, match);
    attacker.manipulate = argFactory.manipulate();
    attacker.charm = argFactory.charm();
    attacker.convince = argFactory.convince();
    attacker.empathy = argFactory.empathy();
    attacker.scare = argFactory.scare();
    attacker.taunt = argFactory.taunt();
    attacker.coerce = argFactory.coerce();
    attacker.trick = argFactory.trick(attacker.setActive);
    attacker.bluff = argFactory.bluff();
    attacker.subterfugeArguments.push(attacker.trick);
    attacker.subterfugeArguments.push(attacker.bluff);
}

PlayerFactory.prototype.buildMockPlayer = function () {
    return new Player(1, 1, 1, 1, 1, 1, 10);
}

PlayerFactory.prototype.buildMockMatch = function () {
    return new Match();
}

module.exports = PlayerFactory;




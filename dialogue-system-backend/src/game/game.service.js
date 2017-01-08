var GameModel = require('./game.model');
var PlayerFactory = require('../components/character/playerFactory');
var Match = require('../components/character/match');
var ArgumentTypes= require('../components/gameplay/argument/argumentTypes');

var GameService = function () {
    this.matchStarted = false;
    this.player1Ready = false;
    this.player2Ready = false;
    this.gameModel = new GameModel(null, null, null);
}

GameService.prototype.addController = function (controller) {
    if (!this.controllerPlayer1) {
        this.controllerPlayer1 = controller;
        return 1;
    }
    else {
        this.controllerPlayer2 = controller;
        return 2;
    }
}

var startMatch = function () {
    this.gameModel.match = new Match();
    var playerFactory = new PlayerFactory(this.gameModel.player1,
        this.gameModel.player2,this.gameModel.match);

    console.log(this.gameModel.player1);
    this.matchStarted = true;
    this.controllerPlayer1.matchStarted(this.gameModel);
    this.controllerPlayer2.matchStarted(this.gameModel);
}

GameService.prototype.PlayerReady = function (controller, player) {
    if (controller == this.controllerPlayer1) {
        this.gameModel.player1 = player;
        this.player1Ready = !this.player1Ready;
    }
    if (controller == this.controllerPlayer2) {
        this.gameModel.player2 = player;
        this.player2Ready = !this.player2Ready;
    }

    if (this.player1Ready && this.player2Ready)
        startMatch.call(this);
}

GameService.prototype.executeArgument = function(argument,controller){
    
    var player = getPlayerByController.call(this,controller);
    console.log(player);
    switch(argument){
        case ArgumentTypes.Manipulate:
            player.manipulate.execute();
            break;
        default:
            break;
    }
    return this.gameModel;
}

var getPlayerByController = function (controller){
    return controller == this.playerController1? this.gameModel.player1:
        this.gameModel.player2;
}

module.exports = GameService;
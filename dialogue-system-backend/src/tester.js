var GameService = require('./game/game.service');
var Controller = require('./game/console/controller');
var Character = require('./components/character/character');

var test = function () {
    var service = new GameService();
    var controller1 = new Controller(service);
    var controller2 = new Controller(service);
    controller1.service.addController(controller1);
    controller2.service.addController(controller2);
    var player1 = new Character(1,1,1,1,1,1,10);
    var player2 = new Character(1,1,1,1,1,1,10);
    service.PlayerReady(controller1,player1);
    service.PlayerReady(controller2,player2);

    controller1.executeArgument('Manipulate');
}



module.exports.test = test;
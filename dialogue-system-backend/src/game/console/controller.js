var RollResults = require('../../components/gameplay/conditions/rollResults');

var ConsoleController = function(service){
    this.service = service;
}

ConsoleController.prototype.matchStarted = function (gameModel){
    console.log('Match started');
}

ConsoleController.prototype.executeArgument = function(argument){
    this.service.executeArgument(argument, this);
}

ConsoleController.prototype.chooseSoM = function(som){
    this.service.chooseSoM(som,this);
}

ConsoleController.prototype.notify = function (){
    console.log('notified of needed som');
    this.chooseSoM('Anger');
}

module.exports = ConsoleController;
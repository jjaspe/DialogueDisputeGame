var ConsoleController = function(service){
    this.service = service;
}

ConsoleController.prototype.matchStarted = function (gameModel){
    console.log('Match started');
}

ConsoleController.prototype.executeArgument = function(argument){
    this.service.executeArgument(argument, this);
}

module.exports = ConsoleController;
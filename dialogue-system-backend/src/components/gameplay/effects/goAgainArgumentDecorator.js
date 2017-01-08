var GoAgainArgumentDecorator = function (decorated, callback){
    this.decorated= decorated;
    this.callback = callback;
}

GoAgainArgumentDecorator.prototype.do = function(){
    this.decorated.do();
    this.callback();
}

GoAgainArgumentDecorator.prototype.undo = function(){
    this.decorated.undo();
}

module.exports = GoAgainArgumentDecorator;
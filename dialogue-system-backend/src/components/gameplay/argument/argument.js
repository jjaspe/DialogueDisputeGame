var RollResults = require('../conditions/rollResults');

var Argument = function (name, condition, failureEffect, successEffect, greatSuccessEffect) {
    this.name = name;
    this.condition = condition;
    this.failureEffect = failureEffect;
    this.successEffect = successEffect;
    this.greatSuccessEffect = greatSuccessEffect;
    this.resultObservers = [];
}

var updateObservers = function (result) {
    console.log('updated observers');
    this.resultObservers.forEach(a => a.update(result));
}

Argument.prototype.execute = function () {
    console.log({ 'executing ': this.name });
    var conditionResult = this.condition.check();
    updateObservers.call(this,conditionResult);
    this.handleConditionResultHook(conditionResult);
    console.log({ 'got condition result': conditionResult });
    switch (conditionResult) {
        case RollResults.Failure.result:
            this.failureEffect.do();
            console.log('got failure');
            break;
        case RollResults.Success.result:
            this.successEffect.do();;
            console.log('got success')
            break;
        case RollResults.GreatSuccess.result:
            this.greatSuccessEffect.do();
            console.log('got great success');
            break;
        default:
            break;
    }
}

Argument.prototype.handleConditionResultHook = function (result) {
    console.log('Parent hook');
    return;
}

Argument.prototype.addResultObserver = function (observer) {
    this.resultObservers.push(observer);
}

module.exports = Argument;
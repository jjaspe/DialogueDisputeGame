var Argument = function (name,condition, failureEffect, successEffect, greatSuccessEffect) {
    this.name = name;
    this.condition = condition;
    this.failureEffect = failureEffect;
    this.successEffect = successEffect;
    this.greatSuccessEffect = greatSuccessEffect;
}

Argument.prototype.execute = function () {
    console.log('executing ' + this.name);
    var conditionResult = this.condition.check();
    switch (conditionResult) {
        case RollResults.Failure.result:
            this.FailureEffect.do();
            break;
        case RollResults.Success.result:
            this.SuccessEffect.do();
            break;
        case RollResults.GreatSuccess.result:
            this.GreatSuccessEffect.do();
            break;
        default:
            break;
    }

}

module.exports = Argument;
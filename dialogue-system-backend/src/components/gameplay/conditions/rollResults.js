function RollResult(threshold, result) {
    this.threshold = threshold;
    this.result = result;
}

var RollResults = {
    Failure: new RollResult(6, 'Failure'),
    Success: new RollResult(8, 'Success'),
    GreatSuccess: new RollResult(20, 'GreatSuccess')
}

module.exports = RollResults;


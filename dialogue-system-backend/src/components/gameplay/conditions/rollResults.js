function RollResult(threshold, result) {
    this.threshold = threshold;
    this.result = result;
}

var RollResults = [
    new RollResult(6, 'Failure'),
    new RollResult(8, 'Success'),
    new RollResult(20, 'GreatSuccess')
]

module.exports = RollResults;


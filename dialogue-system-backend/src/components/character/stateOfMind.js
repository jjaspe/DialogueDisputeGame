var StateOfMind = function (axis1, axis2) {
    this.axis1 = axis1;
    this.axis2 = axis2;
}

var StateOfMindDirections = {
    Fear:'Fear',
    Anger: 'Anger',
    Joy: 'Joy',
    Sadness: 'Sadness'
}

module.exports = {
    StateOfMind: StateOfMind,
    StateOfMindDirections: StateOfMindDirections
};
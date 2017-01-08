

var SoMCheckCondition = function (som1, som2, thresholds) {
    this.som1 = som1;
    this.som2 = som2;
    this.thresholds = thresholds.sort((a, b) => a.threshold - b.threshold);;
}

SoMCheckCondition.prototype.check = function () {
    return checkStateDisregardIntensity(this.som1, this.som2) ? this.thresholds[1].result
         : this.thresholds[0].result;
}

var checkStateDisregardIntensity = (val1, val2) => {
    if (val1 === 0)
        return val2 === 0;
    else if (val1 < 0)
        return val2 < 0;
    else
        return val2 > 0;
}

var checkStateAndIntensity = (val1, val2) => {
    return val1 === val2;
}

module.exports = SoMCheckCondition;
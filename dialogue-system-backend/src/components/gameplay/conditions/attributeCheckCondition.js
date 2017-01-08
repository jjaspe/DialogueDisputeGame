function AttributeCheckCondition(attribute1, attribute2, roll, bonuses, thresholds) {
    this.attribute1 = attribute1;
    this.attribute2 = attribute2;
    this.roll = roll;
    this.bonuses = bonuses;
    this.thresholds = thresholds.sort((a, b) => a.threshold - b.threshold);
}

AttributeCheckCondition.prototype.check = function () {
    console.log({'condition check':''});
    var result = this.attribute1.getValue() + this.roll.roll()
        - this.attribute2.getValue();

    this.bonuses.filter(a => a.turns != 0).forEach((a) => {
        result += a.value;
        a.turns--;
    });
    console.log({result:result});
    var resultThreshold=null;
    this.thresholds.forEach( a => { 
        if (!resultThreshold && a.threshold >= result)
            resultThreshold = a;
    });
    return resultThreshold.result;
}

module.exports = AttributeCheckCondition;
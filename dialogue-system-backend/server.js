var express = require("express");
var path = require("path");
var bodyParser = require("body-parser");
var mongodb = require("mongodb");
var dbConnection = require("./dbConnection");
var tester = require("./src/Tester");

var allowCrossDomain = function(req, res, next) {
    res.header('Access-Control-Allow-Origin', '*');
    res.header('Access-Control-Allow-Methods', 'GET,PUT,POST,DELETE,OPTIONS');
    res.header('Access-Control-Allow-Headers', 'Content-Type, Authorization, Content-Length, X-Requested-With');
    
    //console.log(req.method);
    //console.log(req.url);
    if(req.method=='OPTIONS')
    {
        if(!res.statusCode)
            res.sendStatus(200);    
        else
            next();
    }
    else
        next();
};

var app = express();
app.use(express.static(__dirname + "/public"));
app.use(bodyParser.json());
app.use(allowCrossDomain);

if(process)
    var port=process.env.PORT
// Initialize the app.
var server = app.listen(port||8090, function () {
    var port = server.address().port;
    console.log("App now running on port", port);
});

app.get("/Test", function (req, res) {
    jT(tester.test(),res);
});

var jT = (r,res) => {
    res.status(200).json(r);
}




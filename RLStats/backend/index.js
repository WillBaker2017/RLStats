const fs = require('fs');
const express = require('express');
const bodyParser = require('body-parser');
const cors = require('cors');
const app = express();
const http = require('http');
const https = require('https');
const port = 28015;
const fetch = require("node-fetch");

app.listen(port, () => {
    console.log(`WebAPI app listening on port ${port}!`);
});

//I also say HI
app.use(bodyParser.json());
app.use(cors());

//User is requesting RocketLeagueAPI
app.get('/api/RocketLeague',async function (req, res) {
//Get Info Needed for API Call
    var RLPlatform = req.query.platform;
    var RLName = req.query.name;

    //Send GET Request
    const response = await getData('https://api.tracker.gg/api/v2/rocket-league/standard/profile/' + RLPlatform + '/' + RLName)
        .then((data) => {
            if (data.data) {
                //Data Pulled successfully
                console.log("Got Data");
                return {
                    result: true,
                    message: "NA",
                    playerId: data.data.metadata.playerId,
                    Standard3MMR: data.data.segments[4].stats.rating.value,
                    Standard2MMR: data.data.segments[3].stats.rating.value
                };
            } else if(data.errors) {
                console.log(data.errors[0].message);
                return {
                    result: false,
                    message: data.errors[0].message,
                    playerId: 0,
                    Standard3MMR: 0,
                    Standard2MMR: 0
                };
            }else{
                console.log("Unknown Error");
                return {
                    result: false,
                    message: "Unknown Error",
                    playerId: 0,
                    Standard3MMR: 0,
                    Standard2MMR: 0
                };
            }
        });
    return res.status(200).send(await response);
});


//--------------------------------------------------------------------------------------------
async function getData(url = '', data = {}) {
    const response = await fetch(url, {
        method: 'GET', // *GET, POST, PUT, DELETE, etc.
        mode: 'cors', // no-cors, *cors, same-origin
        cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
        credentials: 'same-origin', // include, *same-origin, omit
        headers: {
            // update with your user-agent
            "User-Agent":
                "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.89 Safari/537.36",
            Accept: "application/json; charset=UTF-8",
        },
        redirect: 'follow', // manual, *follow, error
        referrerPolicy: 'no-referrer', // no-referrer, *client
        //  body: JSON.stringify(data) // body data type must match "Content-Type" header
    });
    return response.json(); // parses JSON response into native JavaScript objects
}

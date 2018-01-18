var output = document.getElementById('connecting');

var log = 'Trying to connect';
var iter = 0;

var attemptTime = 5000;

function AddingDots() {
    log += '.';
    output.innerHTML = log;
    setTimeout(AddingDots, attemptTime / 3);
}

function AttemptConnect(ip) {
    log = 'Trying to connect';
    output.innerHTML = log;

    var deviceid = document.getElementById('deviceid').getAttribute('value');
    var playerid = document.getElementById('playerid').getAttribute('value');
    fetch('http://mqtt.fyo.io/api/v1/' + deviceid + '/connect/' + playerid);

    var timer = setTimeout(function() {
        AttemptConnect(ip);
    }, attemptTime);

    fetch('http://' + ip + '/ping').then( function(resp) {
        console.log(resp);
        if(resp.status === 200) {
            clearTimeout(timer);
            window.location = 'http://' + ip;
        }
    });
}

AttemptConnect(output.getAttribute('data-ip'));
AddingDots();
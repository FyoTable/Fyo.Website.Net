var output = document.getElementById('connecting');

var log = '';

function AttemptConnect(ip) {
    log += '<div> Trying to connect... </div>';
    output.innerHTML = log;

    var timer = setTimeout(function() {
        AttemptConnect(ip);
    }, 5000);

    fetch('http://' + ip + '/ping').then( function(resp) {
        console.log(resp);
        if(resp.status === 200) {
            clearTimeout(timer);
            window.location = 'http://' + ip;
        }
    });
}

AttemptConnect(output.getAttribute('data-ip'));
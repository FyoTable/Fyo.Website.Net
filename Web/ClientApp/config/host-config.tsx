let backendHost = 'http://localhost:5000';

const hostname = window && window.location && window.location.hostname;

if(hostname === 'fyo.azurewebsites.net') {
  backendHost = 'http://fyo.azurewebsites.net';
} 

if(hostname === 'fyo.io' || hostname === 'www.fyo.io') {
  backendHost = 'https://fyo.io';
}

export const HOST_ROOT = backendHost;
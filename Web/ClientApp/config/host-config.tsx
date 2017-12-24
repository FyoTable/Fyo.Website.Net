let backendHost = 'http://localhost:5000';

const hostname = window && window.location && window.location.hostname;

if(hostname === 'fyo-dev.azurewebsites.net') {
  backendHost = 'https://fyo-dev.azurewebsites.net';
} 

export const HOST_ROOT = backendHost;
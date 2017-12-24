import { WebAuth, Auth0DecodedHash } from 'auth0-js';
import { AUTH_CONFIG } from './Auth0-Variables';
import { History } from 'history';
import { RouteComponentProps } from 'react-router';
import { HOST_ROOT } from '../../config/host-config';

export class Auth {
  auth0 = new WebAuth({
    domain: AUTH_CONFIG.domain,
    clientID: AUTH_CONFIG.clientId,
    redirectUri: AUTH_CONFIG.callbackUrl,
    audience: AUTH_CONFIG.audience,
    responseType: 'token id_token',
    scope: 'openid'
  });

  constructor() {
    this.login = this.login.bind(this);
    this.logout = this.logout.bind(this);
    this.handleAuthentication = this.handleAuthentication.bind(this);
    this.isAuthenticated = this.isAuthenticated.bind(this);
  }

  login() {
    this.auth0.authorize();
  }

  handleCallbackHash(nextState: RouteComponentProps<any>) { // tslint:disable-line
    if (/access_token|id_token|error/.test(nextState.location.hash)) {
      this.handleAuthentication(nextState.history);
    }
  }

  handleAuthentication(history: History) {
    this.auth0.parseHash((err, authResult) => { // tslint:disable-line
      if (authResult && authResult.accessToken && authResult.idToken) {
        this.setSession(authResult);
        history.replace('/home');
      } else if (err) {
        history.replace('/');
        console.log(err);
        alert(`Error: ${err.error}. Check the console for further details.`);
      }
    });
  }

  setSession(authResult: Auth0DecodedHash) {
    // Set the time that the access token will expire at
    let expiresAt = JSON.stringify((authResult.expiresIn! * 1000) + new Date().getTime());
    localStorage.setItem('access_token', authResult.accessToken!);
    localStorage.setItem('id_token', authResult.idToken!);
    localStorage.setItem('expires_at', expiresAt!);
  }

  logout() {
    // Clear access token and ID token from local storage
    localStorage.removeItem('access_token');
    localStorage.removeItem('id_token');
    localStorage.removeItem('expires_at');
    this.auth0.logout({returnTo: HOST_ROOT});
  }

  isAuthenticated() {
    // Check whether the current time is past the 
    // access token's expiry time
    const expiresAt = localStorage.getItem('expires_at');
    
    if (expiresAt != null) {
      let parsedExpiration = JSON.parse(expiresAt!);
      return new Date().getTime() < parsedExpiration;
    }

    return false;
  }

  getAuthHeaders(){
    if(!this.isAuthenticated()){
      throw new Error("The user is not authenticated!");
    }

    const access_token = localStorage.getItem('access_token');
    
    if(access_token == null){
      throw new Error("No access token found in local storage!");
    }

    return new Headers({
      'Authorization': `Bearer ${access_token}`
    });
  }
}
import { HOST_ROOT } from '../../config/host-config';
import { Auth } from '../../services';

export class SampleDataService {
    private auth = new Auth();
    private endpointRoot = `${HOST_ROOT}/api/SampleData`;
    
    pingSecure(){
        const endpoint = `${this.endpointRoot}/ping/secure`;
        const init: RequestInit = {
            method: 'GET',
            headers: this.auth.getAuthHeaders(),
            mode: 'cors',
            cache: 'default'
        }

        return fetch(endpoint, init).then((response) => {
            if(response.ok) {
              return response.text();
            }
            throw new Error('Network response was not ok.');
        });
    }

    pingInsecure(){
        const endpoint = `${this.endpointRoot}/ping/insecure`;
        const init: RequestInit = {
            method: 'GET',
            mode: 'cors',
            cache: 'default'
        }

        return fetch(endpoint, init).then((response) => {
            if(response.ok) {
              return response.text();
            }
            throw new Error('Network response was not ok.');
        })
    }

    getQuestions(){
        const endpoint = `${this.endpointRoot}/questions`;
        const init: RequestInit = {
            method: 'GET',
            headers: this.auth.getAuthHeaders(),
            mode: 'cors',
            cache: 'default'
        }

        return fetch(endpoint, init).then((response) => {
            if(response.ok) {
              return response.json();
            }
            throw new Error('Network response was not ok.');
        })
    }
}
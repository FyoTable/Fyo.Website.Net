import { HOST_ROOT } from '../../config/host-config';
import { Auth } from '../../services';
import { IHasId } from '../../interfaces'; 

export class BaseDataService<T extends IHasId> {
    protected auth: Auth;
    protected endpointRoot: string;
    protected entityType: string;

    constructor(entityType: string){
        this.auth = new Auth();
        this.entityType = entityType;
        this.endpointRoot = `${HOST_ROOT}/api/${entityType}`;
    }
    
    public get(id: number): Promise<any>{
        const endpoint = `${this.endpointRoot}/${id}`;
        const init = this.buildRequestInit('GET');

        return fetch(endpoint, init).then((response) => {
            if(response.ok) {
              return response.json();
            }
            throw new Error('Network response was not ok.');
        })
    }

    public getAll(): Promise<any>{
        const endpoint = `${this.endpointRoot}`;
        const init = this.buildRequestInit('GET');

        return fetch(endpoint, init).then((response) => {
            if(response.ok) {
                return response.json();
            }
            throw new Error('Network response was not ok.');
        })
    }

    public update(entity: T): Promise<any>{
        const endpoint = `${this.endpointRoot}/${entity.id}`;
        const init = this.buildRequestInit('PUT', entity);

        return fetch(endpoint, init).then((response) => {
            if(response.ok) {
              return response.json();
            }
            throw new Error('Network response was not ok.');
        })
    }

    public create(entity: T): Promise<any>{
        const endpoint = `${this.endpointRoot}`;
        const init = this.buildRequestInit('POST', entity);

        return fetch(endpoint, init).then((response) => {
            if(response.ok) {
                return response.json();
            }
            throw new Error('Network response was not ok.');
        })
    }

    public delete(id: number): Promise<any>{
        const endpoint = `${this.endpointRoot}/${id}`;
        const init = this.buildRequestInit('DELETE');

        return fetch(endpoint, init).then((response) => {
            if(response.ok) {
                return response.json();
            }
            throw new Error('Network response was not ok.');
        })
    }

    protected buildRequestInit(method: string, body?: T): RequestInit {
        let requestInit: RequestInit = {
            method: method,
            mode: 'cors',
            cache: 'default'
        }

        let headers = this.auth.getAuthHeaders();
        
        if(body){
            headers.append('content-type', 'application/json');
            requestInit.body = JSON.stringify(body);
        }

        requestInit.headers = headers;

        return requestInit;
    }
}
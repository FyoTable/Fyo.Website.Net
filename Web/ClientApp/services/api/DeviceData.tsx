import { HOST_ROOT } from '../../config/host-config';
import { BaseDataService } from './BaseDataService';
import { Device } from 'models';

export class DeviceDataService extends BaseDataService<Device> {
    constructor(){
        super("Device");
    }



    protected buildMqqtRequestInit(method: string): RequestInit {
        let requestInit: RequestInit = {
            method: method
        }
        
        return requestInit;
    }

    public isConnected(id: string): Promise<any>{
        const endpoint = `http://mqqt.fyo.io/api/v1/${id}/live`;
        const init = this.buildMqqtRequestInit('GET');

        return fetch(endpoint, init).then((response) => {
            if(response.ok) {
              return response.json();
            }
            throw new Error('Network response was not ok.');
        })
    }

    public command(id: string, cmd: string): Promise<any>{
        const endpoint = `http://mqqt.fyo.io/api/v1/${id}/${cmd}`;
        const init = this.buildMqqtRequestInit('GET');

        return fetch(endpoint, init).then((response) => {
            if(response.ok) {
              return response.json();
            }
            throw new Error('Network response was not ok.');
        })
    }
    
}
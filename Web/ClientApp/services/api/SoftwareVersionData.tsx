import { HOST_ROOT } from '../../config/host-config';
import { BaseDataService } from './BaseDataService';
import { SoftwareVersion } from 'models';
import { Software } from 'models';

export class SoftwareVersionDataService extends BaseDataService<SoftwareVersion> {
    
    constructor(){
        super("SoftwareVersion");
    }   


    public getAllBySoftware(software: Software): Promise<any>{
        const endpoint = `${this.endpointRoot}/BySoftware/${software.id}`;
        const init = this.buildRequestInit('GET');

        return fetch(endpoint, init).then((response) => {
            if(response.ok) {
                return response.json();
            }
            throw new Error('Network response was not ok.');
        })
    }

    public fileUpload(softwareVersion: SoftwareVersion, file: any) {
        const endpoint = `${this.endpointRoot}/${softwareVersion.id}/APK`;

        const formData = new FormData();
        console.log(file);
        formData.append('files', file)


        let requestInit: RequestInit = {
            method: 'POST'
        }

        let headers = this.auth.getAuthHeaders();        
        //headers.append('content-type', 'multipart/form-data');
        //headers.append('content-type', 'application/x-www-form-urlencoded');

        requestInit.body = formData;
        requestInit.headers = headers;


        return fetch(endpoint, requestInit).then((response) => {
            if(response.ok) {
                return response.json();
            }
            throw new Error('Network response was not ok.');
        })
      }
    
}
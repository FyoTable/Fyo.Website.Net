import { HOST_ROOT } from '../../config/host-config';
import { BaseDataService } from './BaseDataService';
import { Software } from 'models';

export class SoftwareDataService extends BaseDataService<Software> {
    constructor(){
        super("Software");
    }   
}
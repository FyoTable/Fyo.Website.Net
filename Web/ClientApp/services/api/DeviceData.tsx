import { HOST_ROOT } from '../../config/host-config';
import { BaseDataService } from './BaseDataService';
import { Device } from 'models';

export class DeviceDataService extends BaseDataService<Device> {
    constructor(){
        super("Device");
    }
}
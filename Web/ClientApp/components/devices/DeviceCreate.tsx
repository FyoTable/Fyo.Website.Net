import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { EditState, EditProps } from '../../interfaces';
import { Device } from '../../models';
import { DeviceDataService } from '../../services';
import { NavLink } from 'react-router-dom';
import { Text, Form, FormState, FormApi } from 'react-form';
import { DeviceForm } from './DeviceForm';

export class DeviceCreate extends React.Component<RouteComponentProps<{}>, {}>{
    private deviceDataService = new DeviceDataService();

    constructor(props: RouteComponentProps<{}>){
        super(props);

        this.save = this.save.bind(this);
    }

    private save(values: any){
        console.log('submitted', values);
        this.deviceDataService.create(values);
    }
    
    public render() {
        return (
            <div>
                <h1>Create Device</h1>
                <DeviceForm submit={this.save}></DeviceForm>
            </div>)
    }
}
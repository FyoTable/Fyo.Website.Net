import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { EditState, EditProps } from '../../interfaces';
import { SoftwareVersion } from '../../models';
import { SoftwareVersionDataService } from '../../services';
import { NavLink } from 'react-router-dom';
import { Text, Form, FormState, FormApi } from 'react-form';
import { SoftwareVersionForm } from './SoftwareVersionForm';

export class SoftwareVersionCreate extends React.Component<RouteComponentProps<{}>, {}>{
    private softwareVersionDataService = new SoftwareVersionDataService();

    constructor(props: RouteComponentProps<{}>){
        super(props);

        this.save = this.save.bind(this);
    }

    private save(values: any){
        console.log('submitted', values);
        this.softwareVersionDataService.create(values);
    }
    
    public render() {
        return (
            <div>
                <h1>Create Software Version</h1>
                <SoftwareVersionForm submit={this.save}></SoftwareVersionForm>
            </div>)
    }
}
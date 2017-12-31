import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { EditState, EditProps } from '../../interfaces';
import { Software } from '../../models';
import { SoftwareDataService } from '../../services';
import { NavLink } from 'react-router-dom';
import { Text, Form, FormState, FormApi } from 'react-form';
import { SoftwareForm } from './SoftwareForm';

export class SoftwareCreate extends React.Component<RouteComponentProps<{}>, {}>{
    private softwareDataService = new SoftwareDataService();

    constructor(props: RouteComponentProps<{}>){
        super(props);

        this.save = this.save.bind(this);
    }

    private save(values: any){
        console.log('submitted', values);
        this.softwareDataService.create(values);
    }
    
    public render() {
        return (
            <div>
                <h1>Create Software</h1>
                <SoftwareForm submit={this.save}></SoftwareForm>
            </div>)
    }
}
import * as React from 'react';
import { Form, Text, Select } from 'react-form';
import { BaseFormProps } from 'interfaces';
import { SoftwareDataService } from '../../services';
import { Software, SoftwareListing } from '../../models';

interface SoftwareOption {
    label: string,
    value: number
}

export class SoftwareVersionForm extends React.Component<BaseFormProps>{
    private softwareDataService = new SoftwareDataService();

    statusOptions: SoftwareOption[] = [ ];

    public componentDidMount(){
        this.softwareDataService.getAll().then((software: SoftwareListing[]) => {
            console.log(software);

            this.statusOptions = [];
            software.map( (s) => {
                this.statusOptions.push({
                    label: s.software.name,
                    value: s.software.id
                });
                
            });

            this.forceUpdate();

        });
    }

    public render(){
        return (
            <Form defaultValues={this.props.defaultValues} onSubmit={submittedValues => this.props.submit(submittedValues)}>
                {formApi => (
                    <form onSubmit={formApi.submitForm} id="entityForm">
                        <div className="form-group">
                            <label htmlFor="name">Software</label>
                            <Select className="form-control" field="softwareId" id="softwareId" options={this.statusOptions}>
                            </Select>
                        </div>
                        
                        <div className="form-group">
                            <label htmlFor="name">Name</label>
                            <Text className="form-control" field="name" id="name" />
                        </div>

                        <div className="form-group">
                            <label htmlFor="name">Version</label>
                            <Text className="form-control" field="version" id="version" />
                        </div>

                        <div className="form-group">
                            <label htmlFor="name">Version</label>
                            <Text className="form-control" field="apk" id="apk" />
                        </div>

                        <button type="submit" className="btn btn-primary">Submit</button>
                    </form>
                )}
            </Form>
        )
    }
}
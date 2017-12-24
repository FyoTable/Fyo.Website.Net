import * as React from 'react';
import { Form, Text } from 'react-form';
import { BaseFormProps } from 'interfaces';

export class DeviceForm extends React.Component<BaseFormProps>{
    public render(){
        return (
            <Form defaultValues={this.props.defaultValues} onSubmit={submittedValues => this.props.submit(submittedValues)}>
                {formApi => (
                    <form onSubmit={formApi.submitForm} id="entityForm">
                        <div className="form-group">
                            <label htmlFor="name">Name</label>
                            <Text className="form-control" field="name" id="name" />
                        </div>

                        <button type="submit" className="btn btn-primary">Submit</button>
                    </form>
                )}
            </Form>
        )
    }
}
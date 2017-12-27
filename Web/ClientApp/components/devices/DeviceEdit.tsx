import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { EditState, EditProps } from '../../interfaces';
import { Device } from '../../models';
import { DeviceDataService } from '../../services';
import { NavLink } from 'react-router-dom';
import { Text, Form, FormState, FormApi } from 'react-form';
import { TopNavMenu } from '../navigation/TopNavMenu';

export class DeviceEdit extends React.Component<RouteComponentProps<EditProps>, EditState<Device>>{
    private deviceDataService = new DeviceDataService();
    private isConnected: boolean = false;

    constructor(props: RouteComponentProps<EditProps>){
        super(props);

        this.state = {
            originalEntity: undefined
        };
    }

    public componentDidMount(){
        const id = this.props.match.params.id;

        if(id){
            this.deviceDataService.get(id).then((device: Device) => {
                console.log(device);
                this.setState({ originalEntity: device });

                this.deviceDataService.isConnected(device.uniqueIdentifier).then((state: boolean) => {
                    this.isConnected = state;
                })
            });
        }
    }

    private save(values: any){
        this.deviceDataService.update(values);
    }
    
    public render() {
        if(!this.state.originalEntity){
            return <div>we should put a loading thing here</div>
        }
        
        return (<div>
            
            <TopNavMenu open={true}>
            
            </TopNavMenu>

            <div className="content-header">
                <div className="content-container">
                    <NavLink to={ '/device' }>
                        <span className='glyphicon glyphicon-triangle-left'></span> back
                    </NavLink>  
                <h1>Edit Device</h1>
                </div>
            </div>
            
            <div className="content-body">
                <div className="content-container">
                    <div className="form-group">
                        <label htmlFor="name">Is Connected: { this.isConnected ? 'Yes' : 'No' }</label>
                    </div>

                    <Form defaultValues={this.state.originalEntity} onSubmit={submittedValues => this.save(submittedValues)}>
                        {formApi => (
                            <form onSubmit={formApi.submitForm} id="entityForm">
                                <div className="form-group">
                                    <label htmlFor="name">Name</label>
                                    <Text className="form-control" field="name" id="name" />
                                </div>
                                <div className="form-group">
                                    <label htmlFor="code">Unique Identifier</label>
                                    <Text className="form-control" field="uniqueIdentifier" id="uniqueIdentifier" />
                                </div>

                                <button type="submit" className="btn btn-primary">Submit</button>
                            </form>
                        )}
                    </Form>
                </div>
            </div>
            </div>)
    }
}
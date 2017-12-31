import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { EditState, EditProps } from '../../interfaces';
import { Device, DeviceSoftwareVersion } from '../../models';
import { Software } from '../../models';
import { SoftwareVersion, SoftwareListing } from '../../models';
import { DeviceDataService } from '../../services';
import { SoftwareDataService } from '../../services';
import { NavLink } from 'react-router-dom';
import { Text, Form, Select, FormState, FormApi } from 'react-form';
import { TopNavMenu } from '../navigation/TopNavMenu';
import { TopNavMenuItem } from '../navigation/TopNavMenuItem';
import { version } from 'react';

interface DeviceEditState {
    originalEntity: Device | undefined;
    softwareVersions: DeviceSoftwareVersion[];
    software: SoftwareListing[];
    selected: number,
    softwareToAdd: number
}
interface SoftwareVersionOption {
    label: string;
    value: number;
}

export class DeviceEdit extends React.Component<RouteComponentProps<EditProps>, DeviceEditState>{
    private deviceDataService = new DeviceDataService();
    private softwareDataService = new SoftwareDataService();
    private isConnected: boolean = false;

    constructor(props: RouteComponentProps<EditProps>){
        super(props);

        this.state = {
            originalEntity: undefined,
            softwareVersions: [],
            software: [],
            selected: -1,
            softwareToAdd: -1
        };
    }

    public componentDidMount(){
        const id = this.props.match.params.id;

        if(id){
            this.deviceDataService.get(id).then((device: Device) => {
                console.log(device);
                this.setState({ originalEntity: device });

                this.deviceDataService.isConnected(device.uniqueIdentifier).then((data: any) => {
                    this.isConnected = data.state;
                    this.forceUpdate();
                });

                this.deviceDataService.softwareVersions(device).then((data: DeviceSoftwareVersion[]) => {
                    console.log(data);
                    this.setState({ softwareVersions: data });



                    this.softwareDataService.getAll().then((software: SoftwareListing[]) => {
                        console.log(software);

                        var results: SoftwareListing[] = [];
                        software.map( (s) => {
                            var keep = true;
                            this.state.softwareVersions.map( (sv) => {
                                if(sv.softwareId == s.software.id) {
                                    keep = false;
                                }
                            });
                            if(keep) {
                                results.push(s);
                            }
                        });

                        if(results.length > 0) {
                            this.setState({
                                software: results,
                                selected: 0
                            });
                            this.forceUpdate();
                        }

                    });
                });

            });
        }
    }

    private save(values: any){
        this.deviceDataService.update(values);
    }
    

    private sendCommand(cmd: string) {
        if(!this.state.originalEntity) {
            return;
        }
        this.deviceDataService.command(this.state.originalEntity.uniqueIdentifier, cmd);
    }

    private sendCommandBinder(cmd: string): ((event: React.MouseEvent<HTMLElement>) => void) {
        return this.sendCommand.bind(this, cmd);
    } 

    private addSoftware() {
        console.log(this.state.softwareToAdd);
        if(!this.state.originalEntity) {
            return;
        }

        this.deviceDataService.addSoftware(this.state.originalEntity, this.state.softwareToAdd).then((data: any) => {
                
            if(!this.state.originalEntity) {
                return;
            }

            this.deviceDataService.softwareVersions(this.state.originalEntity).then((data: DeviceSoftwareVersion[]) => {
                console.log(data);
                this.setState({ softwareVersions: data });
                this.forceUpdate();
            });
        });
    }

    private addSoftwareBind(): ((event: React.MouseEvent<HTMLElement>) => void) {
        return this.addSoftware.bind(this);
    } 

    private changeSoftware(event: any) {
        console.log(this.state.softwareToAdd, event);
        this.setState({
            softwareToAdd: event.currentTarget.value
        })
    }

    private changeSoftwareBinder(): ((event: React.FormEvent<HTMLSelectElement>) => void) {
        return this.changeSoftware.bind(this);
    } 
    

    public render() {
        if(!this.state.originalEntity){
            return <div>we should put a loading thing here</div>
        }
        
        return (<div>
            
            <TopNavMenu open={true}>
                <TopNavMenuItem>
                    <NavLink to={ '/device/create' }><span className="glyphicon glyphicon-floppy-disk"></span></NavLink>
                </TopNavMenuItem>
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



                                <hr />

                                <h3>Software</h3>
                                <div>

                                    <table className="table table-striped">
                                        <thead>
                                            <tr>
                                                <th>Name</th>
                                                <th>Version</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            {
                                                this.state.softwareVersions.map((entity, index) => {
                                                    return <tr key={ index }>
                                                        <td>{entity.softwareName}</td>
                                                        <td>
                                                            <select className="form-control" value={entity.id}>
                                                                {
                                                                    entity.allVersions.map((v) => { 
                                                                        return <option value={v.id}>{v.version}</option>;
                                                            })}</select>
                                                        </td>
                                                    </tr>
                                                })
                                            }
                                            <tr>
                                                <td>
                                                    <select className="form-control" onChange={this.changeSoftwareBinder()} value={this.state.softwareToAdd}>
                                                        <option value="-1"> </option>
                                                        {this.state.software.map((v) => { return <option key={v.software.id} value={v.software.id}>{v.software.name}</option>;
                                                    })}</select>
                                                </td>
                                                <td>
                                                    <a className="btn btn-default form-control" onClick={this.addSoftwareBind()}>Add</a>
                                                </td>
                                            </tr>
                                        </tbody>

                                    </table>

                                </div>

                                <br />

                                <button type="submit" className="btn btn-primary">Save</button>

                            </form>
                        )}
                    </Form>

                    <hr />

                    <h3>Commands</h3>
                    <table className="table table-striped">
                    <thead>
                        <tr>
                            <th>
                                Command
                            </th>
                            <th>
                                Description
                            </th>
                            <th>
                                Action
                            </th>
                        </tr>
                        </thead>
                        <tbody>
                        <tr>
                            <td>Update</td>
                            <td>Causes the device to git pull, and install any updated configs</td>
                            <td><a onClick={this.sendCommandBinder("update")} className="btn btn-primary">Update</a></td>
                        </tr>
                        <tr>
                            <td>Reboot</td>
                            <td>Causes the device to restart</td>
                            <td><a onClick={this.sendCommandBinder("reboot")} className="btn btn-primary">Reboot</a></td>
                        </tr>
                        </tbody>
                    </table>
                    


                </div>
            </div>
            </div>)
    }
}
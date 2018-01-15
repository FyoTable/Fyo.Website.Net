import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { EditState, EditProps } from '../../interfaces';
import { Software } from '../../models';
import { SoftwareVersion } from '../../models';
import { SoftwareDataService } from '../../services';
import { SoftwareVersionDataService } from '../../services';
import { NavLink } from 'react-router-dom';
import { Text, Form, FormState, FormApi } from 'react-form';
import { TopNavMenu } from '../navigation/TopNavMenu';

interface SoftwareEditState {
    originalEntity: Software | undefined;
    versions: SoftwareVersion[];
    originalVersionEntity: SoftwareVersion | undefined;
}

export class SoftwareEdit extends React.Component<RouteComponentProps<EditProps>, SoftwareEditState>{
    private softwareDataService = new SoftwareDataService();
    private softwareVersionDataService = new SoftwareVersionDataService();
    private isConnected: boolean = false;
    private versions: SoftwareVersion[];

    constructor(props: RouteComponentProps<EditProps>){
        super(props);

        this.state = {
            originalEntity: undefined,
            versions: [],
            originalVersionEntity: undefined
        }
    }

    public componentDidMount(){

        const id = this.props.match.params.id;

        if(id){
            this.softwareDataService.get(id).then((software: Software) => {
                console.log(software);
                this.setState({ originalEntity: software });

                this.softwareVersionDataService.getAllBySoftware(software).then((versions: SoftwareVersion[]) => {
                    console.log(versions);
                    this.setState({ versions: versions });
                })
            });
        }
    }

    private save(values: any){
        console.log(values);
        this.softwareDataService.update(values);
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
                    <NavLink to={ '/software' }>
                        <span className='glyphicon glyphicon-triangle-left'></span> back
                    </NavLink>  
                <h1>Edit Software</h1>
                </div>
            </div>
            
            <div className="content-body">
                <div className="content-container">
                    <Form defaultValues={this.state.originalEntity} onSubmit={submittedValues => this.save(submittedValues)}>
                        {formApi => (
                            <form onSubmit={formApi.submitForm} id="entityForm">
                                <div className="form-group">
                                    <label htmlFor="name">Name</label>
                                    <Text className="form-control" field="name" id="name" />
                                </div>

                                <div className="form-group">
                                    <label htmlFor="name">Package</label>
                                    <Text className="form-control" field="package" id="package" />
                                </div>
                                

                                <button type="submit" className="btn btn-primary">Save</button>

                            </form>
                        )}
                    </Form>

                    <hr />

                    <h3>Versions</h3>
                    <div>

                        <table className="table table-striped">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                {this.state.versions.map((version, index) => {
                                    return <tr key={ index }>
                                        <td>{version.version}</td>
                                        <td>
                                            <NavLink to={ `/softwareversion/${version.id}` }><span className='glyphicon glyphicon-pencil'></span></NavLink>
                                        </td>
                                    </tr>
                                })}
                            </tbody>
                        </table>
                    </div>
                    <NavLink to={ '/softwareversion/create' }>Create Version <span className="glyphicon glyphicon-plus"></span></NavLink>


                </div>
            </div>
            </div>)
    }
}
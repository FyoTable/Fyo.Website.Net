import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { EditState, EditProps } from '../../interfaces';
import { SoftwareVersion } from '../../models';
import { SoftwareVersionDataService } from '../../services';
import { NavLink } from 'react-router-dom';
import { Text, Form, FormState, FormApi } from 'react-form';
import { TopNavMenu } from '../navigation/TopNavMenu';

export class SoftwareVersionEdit extends React.Component<RouteComponentProps<EditProps>, EditState<SoftwareVersion>>{
    private softwareVersionDataService = new SoftwareVersionDataService();
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
            this.softwareVersionDataService.get(id).then((software: SoftwareVersion) => {
                console.log(software);
                this.setState({ originalEntity: software });
            });
        }
    }

    private save(values: any){
        this.softwareVersionDataService.update(values);
    }

    private onFileChange(event: any) {
        console.log(event);

        if(!this.state.originalEntity) {
            return;
        }

        this.softwareVersionDataService.fileUpload(this.state.originalEntity, event.target.files[0]);
    //   this.setState({
    //       file: event.target.files[0]
    //     });
    }

    private onFileChangeBinder(): ((event: React.ChangeEvent<HTMLInputElement>) => void) {
        return this.onFileChange.bind(this);
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
                <h1>Edit Software Version: {this.state.originalEntity.version}</h1>
                </div>
            </div>
            
            <div className="content-body">
                <div className="content-container">
                    <Form defaultValues={this.state.originalEntity} onSubmit={submittedValues => this.save(submittedValues)}>
                        {formApi => (
                            <form onSubmit={formApi.submitForm} id="entityForm">
                                <div className="form-group">
                                    <label htmlFor="apk">Apk</label>
                                    <Text className="form-control" field="apk" id="apk" />
                                </div>

                                <button type="submit" className="btn btn-primary">Save</button>

                            </form>
                        )}
                    </Form>


                    <h3>Upload New APK</h3>
                    <div className="form-group">
                        <label htmlFor="file">File</label>
                        <input type="file" name="file" onChange={this.onFileChangeBinder()} />
                    </div>
                </div>
            </div>
            </div>)
    }
}
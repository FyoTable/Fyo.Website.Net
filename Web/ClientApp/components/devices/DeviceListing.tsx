import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { ListingState } from '../../interfaces';
import { Device } from '../../models';
import { DeviceDataService } from '../../services';
import { NavLink } from 'react-router-dom';
import { TopNavMenu } from '../navigation/TopNavMenu';
import { TopNavMenuItem } from '../navigation/TopNavMenuItem';

export class DeviceListing extends React.Component<RouteComponentProps<{}>, ListingState<Device>>{
    private deviceDataService = new DeviceDataService();

    constructor(props: RouteComponentProps<{}>){
        super(props);

        this.state = { 
            entities: new Array<Device>(), 
        };
    }
    
    public componentDidMount() {
        this.deviceDataService.getAll().then((devices: Device[]) => { 
            this.setState(
                { entities: devices }
            );
        });
    }

    private delete(entity: Device) {
        var confirmed = confirm(`Are you sure you want to delete ${entity.name}`);

        if(confirmed){
            console.log('proceed with delete');
            this.setState({ 
                entities: this.state.entities.filter((device: Device) => {
                    return device.id != entity.id;
                })
            })     
        }
    }

    private deleteBinder(entity: Device): ((event: React.MouseEvent<HTMLElement>) => void) {
        return this.delete.bind(this, entity);
    } 

    public render() {
        return (<div>
            
            <TopNavMenu open={true}>
                <TopNavMenuItem>
                    <NavLink to={ '/device/create' }><span className="glyphicon glyphicon-plus"></span></NavLink>
                </TopNavMenuItem>
            </TopNavMenu>

            <div className="content-header">
                <div className="content-container">
                <h1>Devices</h1>
                </div>
            </div>
            
            <div className="content-body">
                <div className="content-container">
                <table className="table table-striped">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.entities.map((entity, index) => {
                            return <tr key={ index }>
                                <td>{entity.name}</td>
                                <td>
                                    <NavLink to={ `/device/${entity.id}` }><span className='glyphicon glyphicon-pencil'></span></NavLink>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <span onClick={ this.deleteBinder(entity) } className='glyphicon glyphicon-trash'></span>
                                </td>
                            </tr>
                        })}
                    </tbody>
                </table>
                </div>
            </div>
            </div>)
    }
}
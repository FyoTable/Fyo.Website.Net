import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { ListingState } from '../../interfaces';
import { SoftwareVersion } from '../../models';
import { SoftwareVersionDataService } from '../../services';
import { NavLink } from 'react-router-dom';
import { TopNavMenu } from '../navigation/TopNavMenu';
import { TopNavMenuItem } from '../navigation/TopNavMenuItem';

export class SoftwareVersionListing extends React.Component<RouteComponentProps<{}>, ListingState<SoftwareVersion>>{
    private softwareVersionDataService = new SoftwareVersionDataService();

    constructor(props: RouteComponentProps<{}>){
        super(props);

        this.state = { 
            entities: new Array<SoftwareVersion>(), 
        };
    }
    
    public componentDidMount() {
        this.softwareVersionDataService.getAll().then((softwareVersions: SoftwareVersion[]) => { 
            this.setState(
                { entities: softwareVersions }
            );
        });
    }

    private delete(entity: SoftwareVersion) {
        var confirmed = confirm(`Are you sure you want to delete ${entity.version}`);

        if(confirmed){
            console.log('proceed with delete');
            this.setState({ 
                entities: this.state.entities.filter((software: SoftwareVersion) => {
                    return software.id != entity.id;
                })
            })     
        }
    }

    private deleteBinder(entity: SoftwareVersion): ((event: React.MouseEvent<HTMLElement>) => void) {
        return this.delete.bind(this, entity);
    } 

    public render() {
        return (<div>
            
            <TopNavMenu open={true}>
                <TopNavMenuItem>
                    <NavLink to={ '/software/create' }><span className="glyphicon glyphicon-plus"></span></NavLink>
                </TopNavMenuItem>
            </TopNavMenu>

            <div className="content-header">
                <div className="content-container">
                <h1>Software</h1>
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
                                <td>{entity.version}</td>
                                <td>
                                    <NavLink to={ `/software/${entity.id}` }><span className='glyphicon glyphicon-pencil'></span></NavLink>
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
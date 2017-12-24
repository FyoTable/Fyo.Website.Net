import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { SampleDataService } from '../services';
import { Auth } from '../services';
import { TopNavMenu } from './navigation/TopNavMenu';
import { TopNavMenuItem } from './navigation/TopNavMenuItem';

export class Home extends React.Component<RouteComponentProps<{}>, { }> {
    private sampleDataService = new SampleDataService();
    private auth = new Auth();

    constructor(props: RouteComponentProps<{}>){
        super(props);

    }

    public componentDidMount() {
        
    }

    public render() {         
        return <div>
            
            <TopNavMenu open={true}>
                <TopNavMenuItem>
                    <span className="glyphicon glyphicon-sunglasses"></span>
                </TopNavMenuItem>
            </TopNavMenu>

            <div className="content-header">
                <div className="content-container">            
                    <h1>Home</h1>
                </div>
            </div>
            
            <div className="content-body">
                <div className="content-container"> 
                    <h3>All the content</h3>
                </div>
            </div>
        </div>;
    }
}

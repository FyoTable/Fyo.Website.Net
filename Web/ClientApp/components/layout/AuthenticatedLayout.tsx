import * as React from 'react';
import { SideNavMenu } from '../navigation/SideNavMenu';
import { TopNavMenu } from '../navigation/TopNavMenu';
import { Auth } from '../../services';

export class AuthenticatedLayout extends React.Component<{}, {}> {
    private auth = new Auth();
    
    public render() {
        return (
            <div>
                { this.props.children }
            </div>
        );
    }
}

import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';
import { AuthProps } from '../../interfaces';

export class UserInfo extends React.Component<AuthProps, {}> {
    constructor (props: AuthProps){
        super(props);  
    }

    private logout() {
        this.props.auth.logout();
    }

    private logoutBinder(): ((event: React.MouseEvent<HTMLElement>) => void) | undefined {
        return this.logout.bind(this);
    } 

    public render() {
        return (
            <li><a onClick={this.logoutBinder()} href="#">Logout</a></li> 
        );
    }
}

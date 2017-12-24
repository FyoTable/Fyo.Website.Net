import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';
import { NavItems } from './NavItems';
import { AuthProps } from '../../interfaces';

interface SideNavProps {
    auth: AuthProps, 
    open: boolean
};

export class SideNavMenu extends React.Component<any> {
    constructor (props: any){
        super(props);  
    }
    
    private login() {
        this.props.auth.login();
    }

    private loginBinder(): ((event: React.MouseEvent<HTMLElement>) => void) | undefined {
        return this.login.bind(this);
    }
    
    public render() {
        const { isAuthenticated } = this.props.auth;

        return (
            <div className='side-nav-container'>
            { isAuthenticated() && 
                (<div className='side-nav'>
                    <div className='navbar navbar-inverse'>
                        <ul className='nav navbar-nav'>
                            <div className="profile-image">
                                <img src="/images/fyo.svg" />
                            </div>
                            <div className="profile-menu">
                                Garrett Hoofman <span className="glyphicon glyphicon-chevron-down"></span>
                            </div>

                            <div className="nav-items">
                                { NavItems() }
                            </div>
                        </ul>
                    </div>
                </div>) 
            }
            { !isAuthenticated() && 
                (<div className='side-nav'>
                    <div className='navbar navbar-inverse'>
                        <ul className='nav navbar-nav'>
                        <li><a onClick={this.loginBinder()} href="#">Login</a></li>
                        </ul>
                    </div>
                </div>) 
            }
            </div>
        );
    }
}

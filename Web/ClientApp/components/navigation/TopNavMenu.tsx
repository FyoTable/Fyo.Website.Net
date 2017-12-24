import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';
import { UserInfo } from '../auth/UserInfo';
import { NavItems } from './NavItems';
import { AuthProps } from '../../interfaces';

export class TopNavMenu extends React.Component<any> {
    constructor (props: any){
        super(props);
    }

    private toggleClick() {
        window.dispatchEvent(new CustomEvent('side-menu-toggle'));
    }

    private toggleClickHandler() {
        return this.toggleClick.bind(this);
    }

    public render() {

        return (
            <div className='top-nav'>
                <div className='top-nav-row'>
                    <div className='box' onClick={this.toggleClickHandler()}>
                        <span className='sr-only'>Toggle navigation</span>
                        <span className='glyphicon glyphicon-menu-hamburger'></span>
                    </div>

                    <div className="top-nav-right">
                        { this.props.children }
                    </div>
                </div>
            </div>
        );
    }
}

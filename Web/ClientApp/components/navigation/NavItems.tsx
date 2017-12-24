import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';

export const NavItems = () => 
    [
        <div key='seperator-main' className="separator"><div className="separator-inner"></div></div>,
        <h3 key='header-main'>Main</h3>,
        <li key='home'>
            <NavLink to={ '/home' } exact activeClassName='active'>
                <span className='glyphicon glyphicon-home'></span>&nbsp;&nbsp;Home
            </NavLink>
        </li>,
        <li key='device'>
            <NavLink to={ '/device' } exact activeClassName='active'>
                <span className='glyphicon glyphicon-list-alt'></span> Devices
            </NavLink>
        </li>,
        <li key='software'>
            <NavLink to={ '/software' } exact activeClassName='active'>
                <span className='glyphicon glyphicon-list-alt'></span> Software
            </NavLink>
        </li>
    ];
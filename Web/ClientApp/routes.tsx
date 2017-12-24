import * as React from 'react';
import { Route, Redirect, Switch } from 'react-router-dom';
import { Auth } from './services';
import { Callback, AppLayout, Landing, Home, AuthenticatedLayout, DeviceListing, DeviceEdit, DeviceCreate } from './components';

const auth = new Auth();

const PrivateRoute = 
    ({ component: Component, ...rest }: any) => { 
        return (
            <Route {...rest} render={props => (
            auth.isAuthenticated() ? (
                <Component {...props}/>
            ) : (
                <Redirect to={{
                    pathname: '/'
                }}/>
            )
            )}/>
        )
    };

const PublicRoute = 
    ({ component: Component, ...rest }: any) => {
        return (
            <AppLayout>
                <Route {...rest} render={props => (<Component {...props}/>)}/>
            </AppLayout>
        )
    };

export const routes =
        <Switch>
            <Route exact path='/' component={ Landing } />
            <Route 
                path="/callback" 
                render={(props) => {
                    auth.handleCallbackHash(props);
                    return (
                        <Callback 
                            {...props} 
                        />
                    );
                }}
            />
            <Route 
                path="/logout" 
                render={() => {
                    auth.logout();
                    return (
                        <Redirect 
                            to="/" 
                        />
                    );
                }}
            />
            <AppLayout>
                <AuthenticatedLayout>
                    <PrivateRoute path="/home" component={ Home } />
                    <PrivateRoute exact path='/device' component={ DeviceListing } />
                    <PrivateRoute path='/device/create' component={ DeviceCreate } />
                    <PrivateRoute path='/device/:id' component={ DeviceEdit } />
                </AuthenticatedLayout>
            </AppLayout>
        </Switch>

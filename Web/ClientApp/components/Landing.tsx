import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Auth } from '../services';

export class Landing extends React.Component<RouteComponentProps<{}>, {}> {
    private auth = new Auth();
    
    private login() {
        this.auth.login();
    }

    private loginBinder(): ((event: React.MouseEvent<HTMLElement>) => void) | undefined {
        return this.login.bind(this);
    }

    public render() {
        return <div className="landing">
            <div className="login">
                <a onClick={this.loginBinder()} href="#">Login</a>
            </div>
        </div>
    }
}

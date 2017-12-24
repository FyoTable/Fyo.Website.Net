import * as React from 'react';
import { SideNavMenu } from '../navigation/SideNavMenu';
import { Auth } from '../../services';
import { LayoutProps } from '../../interfaces';
import { TopNavMenu } from '../navigation/TopNavMenu';


const ScrollToTop = () => {
    window.scrollTo(0, 0);
    return null;
};

export class AppLayout extends React.Component<any> {
    private auth = new Auth();
    private sideMenuOpen: boolean = true;
    private preload: boolean = true;
    
    constructor (props: any){
        super(props);
    }

    componentDidMount() {
        window.addEventListener('side-menu-toggle', this.sideMenuToggle.bind(this), false);
        this.preload = false;
    }
    
    componentWillUnmount() {
        window.removeEventListener('side-menu-toggle', this.sideMenuToggle.bind(this), false);
    }

    private sideMenuToggle() {
        console.log('toggle meny');
        this.sideMenuOpen = !this.sideMenuOpen;
        this.forceUpdate();
    }

    public render() {
        var containerClass = 'react-app-container';
        if(this.preload) {
            containerClass += ' preload';
        }
        if(!this.sideMenuOpen) {
            containerClass += ' full';
        }

        return (
            <div className={containerClass}>
                <ScrollToTop />
                <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,600,700" rel="stylesheet" />
                <link rel="stylesheet" href="/css/site.css" />
                <SideNavMenu auth={this.auth} open={this.sideMenuOpen} />
                <div>
                    { this.props.children }
                </div>
            </div>
        );
    }
}

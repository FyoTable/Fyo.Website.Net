import * as React from 'react';

export class TopNavMenuItem extends React.Component<any> {
    public render() {

        return (
            <div className='box'>
                { this.props.children }
            </div>
        );
    }
}

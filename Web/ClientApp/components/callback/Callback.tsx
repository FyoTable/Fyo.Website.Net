import * as React from 'react';
import './Callback.css';
const loading = require('./loading.svg') as string;

export class Callback extends React.Component {
  render() {
    return (
      <div className="auth0-callback">
        <img src={loading} alt="loading"/>
      </div>
    );
  }
}
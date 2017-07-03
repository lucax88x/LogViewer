import './App.css';
import { logRow } from './models/logRow';
import * as React from 'react';
import 'whatwg-fetch';

interface AppState {
  logRows: logRow[];
}

class App extends React.Component<{}, AppState> {

  constructor() {
    super();
    this.state = {
      logRows: []
    };
  }
  render() {

    let rows = [];

    for (let i = 0; i < this.state.logRows.length; i++) {
      let row = this.state.logRows[i];
      rows.push(
        <div>
          <div className="col-6">{row.clientIp}</div>
          <div className="col-6">{row.numberOfCalls}</div>
        </div>
      );
    }

    return (
      <div className="App">
        <p className="App-intro">
          LogViewer Extractor
        </p>
        <div className="container">
          <div className="table">
            <div className="table-header">
              <div className="col-6">Client Ip</div>
              <div className="col-6">Number of Calls</div>
              <div className="clearfix" />
            </div>
            <div className="table-body">
              {rows}
            </div>
          </div>
        </div>
      </div>
    );
  }

  componentDidMount() {
    fetch('http://localhost:53678/api/log')
      .then(response => response.json())
      .then((data: logRow[]) => {
        this.setState({
          logRows: data
        });
      });
  }
}

export default App;

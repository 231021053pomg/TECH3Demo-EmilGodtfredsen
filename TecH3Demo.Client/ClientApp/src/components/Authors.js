import React, { Component } from 'react';
import axios from 'axios';
import Utils from './Common/Utils';
import Table from 'reactstrap/lib/Table';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';


export class Authors extends Component {
  static displayName = Authors.name;

  constructor(props) {
    super(props);
    this.state = {
      authors: undefined,
    };
  }

  componentDidMount() {
    this.getAllAuthors()
  }

  getAllAuthors() {
    axios({
      url: 'https://localhost:5001/api/author',
      method: 'GET',
    }).then(response => {
      console.log(response.data)
      this.setState({
        authors: response.data
      });
    }).catch((error) => {
      this.handleAlert(Utils.handleAxiosError(error), 'danger')
    })
  }

  renderAuthorTable = () => {
    if (this.state.authors === undefined) {
      return (
        <p className='text-center'>
          <FontAwesomeIcon icon='sync-alt' size="lg" spin={true} />
        </p>
      )
    } else {
      return (
        <Table className="table table-striped table-hover table-borderless rounded sortable">
          <thead className="thead-dark">
            <tr>
              <th>First Name</th>
              <th>Last Name</th>
              <th>Books</th>
            </tr>
          </thead>
          <tbody>
            {this.state.authors.map((author, i) => {
              return (
                <tr key={i} className='font-weight-bold'>
                  <td>{author.firstName}</td>
                  <td>{author.lastName}</td>
                  
                </tr>

              )
            })}
          </tbody>

        </Table>
      )
    }
  }

  render() {
    return (
      <div>
        <h1>Authors</h1>
        {this.renderAuthorTable()}
      </div>
    );
  }
}

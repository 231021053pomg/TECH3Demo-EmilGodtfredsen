import React, { Component } from 'react';
import axios from 'axios';
import Utils from './Common/Utils';
import { Button, Table, Jumbotron, Row, Col, Form } from 'react-bootstrap'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';


export class Authors extends Component {
  static displayName = Authors.name;

  constructor(props) {
    super(props);
    this.state = {
      authors: undefined,
      firstName: '',
      lastName: '',
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
              <th scope="col" className='text-nowrap'>First Name</th>
              <th scope="col" className='text-nowrap'>Last Name</th>
              <th scope="col" className='text-nowrap'>Books</th>
              <th></th>
            </tr>
          </thead>
          <tbody id="tableRow">
            {this.state.authors.map((author, i) => {
              return (
                <tr key={i} className='font-weight-bold'>
                  <td>{author.firstName}</td>
                  <td>{author.lastName}</td>
                  <td><Button variant='dark'><FontAwesomeIcon icon="book" fixedWidth /> Show Books</Button></td>
                  <td><Button variant='danger'><FontAwesomeIcon icon='trash-alt' fixedWidth /> Delete Author</Button></td>
                </tr>
              )
            })}
          </tbody>

        </Table>
      )
    }
  }

  handleAddAuthor = () =>{
    axios({
      url: 'https://localhost:5001/api/author',
      method: 'POST',
      data: {
        firstName: this.state.firstName,
        lastName: this.state.lastName
      }
    }).then(response => {
      console.log(response)
    }).catch((error) => {
      this.handleAlert(Utils.handleAxiosError(error), 'danger')
    })
  }

  handleChange = e => {
    this.setState({
      [e.target.name]: e.target.value,
    })

  }

  render() {
    return (
      <>
        <Row>
          <Col>
            <Jumbotron className="mb-3">
              <h1 className="title">
                <Row>
                  <Col>
                    Authors
                <FontAwesomeIcon icon="feather-alt" fixedWidth />
                  </Col>
                </Row>

              </h1>
            </Jumbotron>
          </Col>
        </Row>
        <Row>
          <Col>
            {this.renderAuthorTable()}
          </Col>
        </Row>

        <Form>
          <Form.Group controlId="formBasicFirstName">
            <Form.Control
              placeholder="Enter First Name"
              name='firstName'
              onChange={this.handleChange.bind(this)}
            />
          </Form.Group>
          <Form.Group controlId="formBasicLastName">
            <Form.Control
              placeholder="Enter Last Name"
              name='lastName'
              onChange={this.handleChange.bind(this)}
            />
          </Form.Group>

          <Button onClick={() => this.handleAddAuthor()} variant='success'><FontAwesomeIcon icon='plus' fixedWidth /> Add Author </Button>

        </Form>
      </>
    );
  }
}

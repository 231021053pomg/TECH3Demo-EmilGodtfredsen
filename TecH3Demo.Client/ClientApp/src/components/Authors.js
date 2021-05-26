import React, { Component } from 'react';
import axios from 'axios';
import Utils from './Common/Utils';
import { Button, Table, Jumbotron, Row, Col, Form } from 'react-bootstrap'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import ConfirmModal from './ConfirmModal.js';
import EditModal from './EditModal.js';


export class Authors extends Component {
  static displayName = Authors.name;

  constructor(props) {
    super(props);
    this.state = {
      authors: undefined,
      firstName: '',
      lastName: '',
      showConfirmModal: false,
      authorId: '',
      showEditModal: false,
      author: '',
    };
  }

  componentDidMount() {
    this.getAllAuthors()
  }

  handleAlert = (comment, variant) => {
    this.setState({
      messageComment: comment,
      messageVariant: variant,
    })
    this.timer = setTimeout(() => {
      this.setState({
        messageComment: '',
        messageVariant: '',
      })
    }, 3000)
  }

  getAllAuthors() {
    axios({
      url: 'https://localhost:5001/api/author',
      method: 'GET',
    }).then(response => {
      this.setState({
        authors: response.data,
        firstName: '',
        lastName: '',
      });
    }).catch((error) => {
      this.handleAlert(Utils.handleAxiosError(error), 'danger')
    })
  }
  // +++ CREATE new author with firstname and lastname +++
  handleAddAuthor = () => {
    axios({
      url: 'https://localhost:5001/api/author',
      method: 'POST',
      data: {
        firstName: this.state.firstName,
        lastName: this.state.lastName
      }
    }).then(response => {
      this.getAllAuthors()
    }).catch((error) => {
      this.handleAlert(Utils.handleAxiosError(error), 'danger')
    })
  }
  // +++ UPDATE existing author (firstname and lastname) +++
  editAuthor = () => {
    axios({
      url: 'https://localhost:5001/api/author/' + this.state.authorId,
      method: 'PUT',
      data: {
        firstName: this.state.firstName,
        lastName: this.state.lastName
      }
    }).then(response => {
      this.getAllAuthors()
    }).catch((error) => {
      this.handleAlert(Utils.handleAxiosError(error), 'danger')
    })
  }

  handleDeleteAuthor = (id) => {
    this.setState({
      authorId: id,
      showConfirmModal: true,
    })
  }

  handleChange = e => {
    this.setState({
      [e.target.name]: e.target.value,
    })

  }

  handleEditAuthor = (author) => {
    this.setState({
      showEditModal: true,
      author: author,
      authorId: author.id,
    })
  }

  closeModal() {
    this.setState({
      showConfirmModal: false,
      showEditModal: false,
    })
  }

  // +++ DELETE existing author(id) +++
  okClicked() {
    this.setState({
      showConfirmModal: false,
    }, () =>
      axios({
        url: 'https://localhost:5001/api/author/' + this.state.authorId,
        method: 'DELETE',
      }).then(response => {
        this.getAllAuthors()
      }).catch((error) => {
        this.handleAlert(Utils.handleAxiosError(error), 'danger')
      }))

  }

  okEditModalClicked() {
    this.setState({
      showEditModal: false,
    }, () => this.editAuthor())
    
  }

  // +++ renders table with existing authors if any is present +++
  renderAuthorTable = () => {
    if (this.state.authors === undefined) {
      return (
        <p className='text-center'>
          <FontAwesomeIcon icon='sync-alt' size="lg" spin={true} />
        </p>
      )
    } else if (this.state.authors.length === 0) {
      return (
        <h5 className="title"><p className="badge badge-dark bg-secondary">None</p></h5>
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
                  <td>
                    {author.firstName}
                  </td>
                  <td>
                    {author.lastName}
                  </td>
                  <td><Button variant='dark'><FontAwesomeIcon icon="book" fixedWidth /> Show Books</Button></td>
                  <td>
                    <Button className='mr-2' onClick={() => this.handleEditAuthor(author)} variant='success'><FontAwesomeIcon icon='edit' fixedWidth /> Edit Author</Button>
                    <Button onClick={() => this.handleDeleteAuthor(author.id)} variant='danger'><FontAwesomeIcon icon='trash-alt' fixedWidth /> Delete Author</Button>
                  </td>
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
      <>
        <ConfirmModal
          show={this.state.showConfirmModal}
          handleOk={this.okClicked.bind(this)}
          closeModal={this.closeModal.bind(this)}
        />
        <EditModal
          show={this.state.showEditModal}
          handleOk={this.okEditModalClicked.bind(this)}
          closeModal={this.closeModal.bind(this)}
          author={this.state.author}
          handleChange={this.handleChange.bind(this)}
        />
        <Row>
          <Col>
            <Jumbotron className="mb-3">
              <h1 className="title">
                <FontAwesomeIcon icon="feather-alt" fixedWidth />
                    Authors
              </h1>
            </Jumbotron>
          </Col>
        </Row>
        <Row>
          <Col>
            {this.renderAuthorTable()}
          </Col>
        </Row>
        <Row>
          <Col className="col-4">
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
          </Col>
        </Row>
      </>
    );
  }
}

import React, { Component } from 'react';
import ReactDOM from "react-dom";
import axios from 'axios';
import Utils from './Common/Utils';
import ShowMessage from './Common/ShowMessage';
import { Jumbotron, Row, Col, Form, Card, DropdownButton, Dropdown, InputGroup, Button } from 'react-bootstrap'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

export class Books extends Component {
    static displayName = Books.name;

    constructor(props) {
        super(props);
        this.state = {
            messageComment: '',
            messageVariant: '',
            authors: undefined,
            listOfBooks: undefined,
            books: undefined,
            tDate: '',
            tTitle: '',
            selectedAuthor: '',
        };
    }

    componentDidMount() {
        this.getAuthors()
        this.getBooks()
    }

    handleDeleteBook(id) {
        axios({
            url: 'https://localhost:5001/api/book/' + id,
            method: 'DELETE',
        }).then(response => {
            this.getBooks()
        }).catch((error) => {
            this.handleAlert(Utils.handleAxiosError(error), 'danger')
        })
    }

    getBooks() {
        axios({
            url: 'https://localhost:5001/api/book',
            method: 'GET',
        }).then(response => {
            this.setState({
                listOfBooks: response.data,
            });
        }).catch((error) => {
            this.handleAlert(Utils.handleAxiosError(error), 'danger')
        })
    }

    getAuthors() {
        axios({
            url: 'https://localhost:5001/api/author',
            method: 'GET',
        }).then(response => {
            this.setState({
                authors: response.data,
            });
        }).catch((error) => {
            this.handleAlert(Utils.handleAxiosError(error), 'danger')
        })
    }

    handleAddBook() {
        axios({
            url: 'https://localhost:5001/api/book',
            method: 'POST',
            data: {
                title: this.state.tTitle,
                published: this.state.tDate,
                authorId: this.state.selectedAuthor.id
            }
        }).then(response => {
            this.handleReset();
        }).catch((error) => {
            this.handleAlert(Utils.handleAxiosError(error), 'danger')
        })
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

    renderBooks() {
        if (this.state.listOfBooks === undefined) {
            return (
                <p className='text-center'>
                    <FontAwesomeIcon icon='sync-alt' size="lg" spin={true} />
                </p>
            )
        } else if (this.state.listOfBooks.length > 0) {
            return (
                this.state.listOfBooks.map((book, i) => {
                    return (
                        <div key={i}>
                            <Row>
                                <Col className='col-4'>
                                    <h5 className="title"><p className="badge badge-dark bg-secondary">Title: </p> {book.title}</h5>
                                </Col>
                                <Col className='col-4'>
                                    <h5 className="title"><p className="badge badge-dark bg-secondary">Written By: </p> {book.author.firstName + ' ' + book.author.lastName}</h5>
                                </Col>
                                <Col className='col-4'>
                                    <Button onClick={() => this.handleDeleteBook(book.id)} variant='danger'><FontAwesomeIcon icon='trash-alt' fixedWidth /> Delete Book </Button>
                                </Col>
                            </Row>
                        </div>

                    )
                })
            )
        } else {
            return (
                <h5 className="title"><p className="badge badge-dark bg-secondary">No Books</p></h5>
            )
        }

    }

    handleReset() {
        window.location.reload();
        ReactDOM.findDOMNode(this.messageForm).value = '';
        this.setState({
            selectedAuthor: '',
        })
    }

    handleSelectAuthor = (author) => {
        this.setState({
            selectedAuthor: author,
        })
    }

    handleChange = e => {
        this.setState({
            [e.target.name]: e.target.value,
        })
    }

    render() {
        if (this.state.messageComment) {
            return (
                <ShowMessage
                    comment={this.state.messageComment}
                    variant={this.state.messageVariant}
                />
            )
        } else {
            return (
                <>
                    <Row>
                        <Col>
                            <Jumbotron className="mb-3">
                                <h1 className="title">
                                    <FontAwesomeIcon icon="book" fixedWidth />
                                Books
                                </h1>
                            </Jumbotron>
                        </Col>
                    </Row>
                    <Row className='mb-3'>
                        <Col>
                            <Card>
                                <Card.Header className="text-uppercase title">
                                    <div className="p-2 font-weight-bold d-flex"> Add Book
                                          <FontAwesomeIcon icon='plus' size='lg' className='ml-auto' fixedWidth />
                                    </div>
                                </Card.Header>
                                <Card.Body>
                                    <Row className='mb-3'>
                                        <Col className="col-3">
                                            <InputGroup className="mb-2">
                                                <InputGroup.Prepend>
                                                    <InputGroup.Text>{this.state.selectedAuthor.firstName} </InputGroup.Text>
                                                </InputGroup.Prepend>
                                                <DropdownButton
                                                    variant="dark"
                                                    title="Select Author"
                                                    onSelect={this.handleSelectAuthor.bind(this)}
                                                >

                                                    {this.state.authors === undefined ?
                                                        <>
                                                            <p className='text-center'>
                                                                <FontAwesomeIcon icon='sync-alt' size="lg" spin={true} />
                                                            </p>
                                                        </>
                                                        :
                                                        <>
                                                            {this.state.authors.map((author, i) => {
                                                                return (
                                                                    <Dropdown.Item key={i} onSelect={() => this.handleSelectAuthor(author)}>
                                                                        {author.firstName + ' ' + author.lastName}
                                                                    </Dropdown.Item>
                                                                )
                                                            })}
                                                        </>
                                                    }



                                                </DropdownButton>
                                            </InputGroup>



                                        </Col>
                                        <Col className="col-4">
                                            <InputGroup>
                                                <InputGroup.Prepend>
                                                    <InputGroup.Text>Title</InputGroup.Text>
                                                </InputGroup.Prepend>
                                                <Form.Control
                                                    ref={form => this.messageForm = form}
                                                    type='text'
                                                    name='tTitle'
                                                    title='Title'
                                                    onChange={this.handleChange.bind(this)}
                                                    placeholder="Enter Book Title"
                                                />
                                            </InputGroup>
                                        </Col>
                                        <Col className="col-4">
                                            <InputGroup>
                                                <InputGroup.Prepend>
                                                    <InputGroup.Text>Published</InputGroup.Text>
                                                </InputGroup.Prepend>
                                                <Form.Control
                                                    ref={form => this.messageForm = form}
                                                    type='date'
                                                    name='tDate'
                                                    title="Date"
                                                    onChange={this.handleChange.bind(this)}
                                                />
                                            </InputGroup>
                                        </Col>
                                    </Row>
                                    <Row>
                                        <Col className='col-2'>
                                            <Button
                                                variant='dark'
                                                onClick={() => this.handleReset()}
                                            >
                                                <FontAwesomeIcon
                                                    icon='power-off'
                                                    fixedWidth /> Reset
                                                 </Button>
                                            <span> </span>

                                        </Col>
                                        <Col>
                                            <Button
                                                variant='success'
                                                onClick={() => this.handleAddBook()}
                                            >
                                                <FontAwesomeIcon icon='plus' fixedWidth />
                                        Add Book
                                        </Button>
                                        </Col>
                                    </Row>

                                </Card.Body>
                            </Card>
                        </Col>
                    </Row>
                    <Row className='mb-3'>
                        <Col>
                            <Card>
                                <Card.Header className="text-uppercase title">
                                    <div className="p-2 font-weight-bold d-flex"> Show Books
                                          <FontAwesomeIcon icon='list' size='lg' className='ml-auto' fixedWidth />
                                    </div>
                                </Card.Header>
                                <Card.Body>
                                    {this.renderBooks()}
                                </Card.Body>
                            </Card>
                        </Col>
                    </Row>
                </>
            );
        }
    }
}

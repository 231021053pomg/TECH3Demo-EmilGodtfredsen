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
            books: undefined,
            tDate: '',
            tTitle: '',
            selectedAuthor: '',
        };
    }

    componentDidMount() {
        this.getAuthors()
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

    handleAddBook(){
        axios({
            url: 'https://localhost:5001/api/book',
            method: 'POST',
            data: {
              title: this.state.tTitle,
              published: this.state.tDate,
              authorId: this.state.selectedAuthor.id
            }
          }).then(response => {
            console.log(response)
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

    handleReset() {
        ReactDOM.findDOMNode(this.messageForm).value = '';
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
                    <Row>
                        <Col>
                            <Card>
                                <Card.Header className="text-uppercase title">
                                    <div className="p-2 font-weight-bold d-flex"> Add Book
                                          <FontAwesomeIcon icon='list' size='lg' className='ml-auto' fixedWidth />
                                    </div>
                                </Card.Header>
                                <Card.Body>
                                    <Row className='mb-3'>
                                        <Col className="col-2">
                                            <DropdownButton variant='dark' title="Select Author">
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
                    <Row>
                        <Col className="col-4">
                            <Form>

                            </Form>
                        </Col>
                    </Row>
                </>
            );
        }
    }
}

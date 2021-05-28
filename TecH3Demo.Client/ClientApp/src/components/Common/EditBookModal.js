import React, { Component } from 'react';
import { Button, Modal, Form, InputGroup, Row } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

class EditBookModal extends Component {

    handleCloseModal = () => {
        this.props.closeModal()
    }

    handleOk = () => {
        this.props.handleOk()
    }

    handleChange = e => {
        this.props.handleChange(e)
    }

    render() {
        return (
            <>
                <Modal
                    show={this.props.show}
                    backdrop="static"
                    keyboard={false}
                    animation={false}
                >
                    <Modal.Header>
                        <h5>
                            <FontAwesomeIcon icon='edit' fixedWidth />
                        </h5>
                    </Modal.Header>
                    <Modal.Body>
                        {this.props.book === undefined ?
                            <p className='text-center'>
                                <FontAwesomeIcon icon='sync-alt' size="lg" spin={true} />
                            </p>
                            :
                            
                            <Form>
                                <Row className='mb-3'>
                                    <InputGroup>
                                        <InputGroup.Prepend>
                                            <InputGroup.Text>Title</InputGroup.Text>
                                        </InputGroup.Prepend>
                                        <Form.Control
                                            placeholder={this.props.book.title}
                                            name='tTitle'
                                            onChange={this.handleChange.bind(this)}
                                        />
                                    </InputGroup>
                                </Row>
                                <Row>
                                    <InputGroup>
                                        <InputGroup.Prepend>
                                            <InputGroup.Text>Published</InputGroup.Text>
                                        </InputGroup.Prepend>
                                        <Form.Control
                                            type='date'
                                            name='tDate'
                                            title='Date'
                                            onChange={this.handleChange.bind(this)}
                                        />

                                    </InputGroup>
                                </Row>

                            </Form>
                        }
                    </Modal.Body>
                    <Modal.Footer>
                        <Button
                            variant='success'
                            onClick={this.handleOk.bind(this)}
                        >
                            <FontAwesomeIcon icon='check' fixedWidth /> Finished Editing
                                </Button>
                        <Button
                            variant='danger'
                            onClick={this.handleCloseModal.bind(this)}
                        >
                            <FontAwesomeIcon icon='times' fixedWidth /> Cancel
                                </Button>
                    </Modal.Footer>
                </Modal>
            </>
            
        )
    }
}

export default EditBookModal;

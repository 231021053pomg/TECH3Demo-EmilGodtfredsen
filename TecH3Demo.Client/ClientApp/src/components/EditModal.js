import React, { Component } from 'react';
import { Button, Modal, Form } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

class EditModal extends Component {

    handleCloseModal = () => {
        this.props.closeModal()
    }

    handleOk = () => {
        this.props.handleOk()
    }

    handleChange = e =>{
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
                        <Form>
                            <Form.Group controlId="formBasicFirstName">
                                <Form.Control
                                    placeholder={this.props.author.firstName}
                                    name='firstName'
                                    onChange={this.handleChange.bind(this)}
                                />
                            </Form.Group>
                            <Form.Group controlId="formBasicLastName">
                                <Form.Control
                                    placeholder={this.props.author.lastName}
                                    name='lastName'
                                    onChange={this.handleChange.bind(this)}
                                />
                            </Form.Group>
                        </Form>

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

export default EditModal;

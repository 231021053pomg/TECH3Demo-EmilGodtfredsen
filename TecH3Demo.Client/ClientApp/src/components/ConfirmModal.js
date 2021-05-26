import React, { Component } from 'react';
import { Button, Modal } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

class ConfirmModal extends Component {

    handleCloseModal = () => {
        this.props.closeModal()
    }

    handleOk = () =>{
        this.props.handleOk()
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
                            <FontAwesomeIcon icon='info-circle' fixedWidth />
                        </h5>
                    </Modal.Header>
                    <Modal.Body>
                        <p>Please confirm cancellation</p>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button
                            variant='dark'
                            onClick={this.handleOk.bind(this)}
                        >
                            <FontAwesomeIcon icon='check' fixedWidth /> OK
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

export default ConfirmModal;

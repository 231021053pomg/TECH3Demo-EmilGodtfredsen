import React, { Component } from 'react';
import { Button, Modal, Table } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import ConfirmModal from './ConfirmModal.js';

class BooksModal extends Component {

    constructor(props) {
        super(props);
        this.state = {
            showConfirmModal: false,
            deleteId: '',
        }
    }

    handleCloseModal = () => {
        this.props.closeModal()
    }

    handleOk = () => {
        this.props.handleOk()
    }

    handleChange = e => {
        this.props.handleChange(e)
    }

    handleDeleteBook(id) {
        this.setState({
            deleteId: id,
            showConfirmModal: true,
        })
    }

    handleEditBookClicked (id){
        this.props.editBook(id)
    }

    closeModal() {
        this.setState({
            showConfirmModal: false,
        })
    }

    okClicked = () => {
        this.setState({
            showConfirmModal: false,
        }, () => this.props.deleteBook(this.state.deleteId))

    }

    renderTable() {
        if (this.props.books === undefined || this.props.books === '') {
            return (
                <p className='text-center'>
                    <FontAwesomeIcon icon='sync-alt' size="lg" spin={true} />
                </p>
            )
        } else {
            if (this.props.books.length > 0) {
                return (
                    <Table className="table table-striped table-hover table-borderless rounded sortable">
                        <thead className="thead-dark">
                            <tr>
                                <th scope="col" className='text-nowrap'>Title</th>
                                <th scope="col" className='text-nowrap'>Published</th>
                                <th></th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody id="tableRow">
                            {this.props.books.map((book, i) => {

                                return (
                                    <tr key={i} className='font-weight-bold'>
                                        <td>
                                            {book.title}
                                        </td>
                                        <td>
                                            {book.published}
                                        </td>
                                        <td>
                                            <Button onClick={() => this.handleEditBookClicked(book.id)} className='mr-2' variant='success'><FontAwesomeIcon icon='edit' fixedWidth /> Edit Book</Button>
                                            <Button onClick={() => this.handleDeleteBook(book.id)} variant='danger'><FontAwesomeIcon icon='trash-alt' fixedWidth /> Delete Book</Button>
                                        </td>
                                    </tr>
                                )
                            })}
                        </tbody>

                    </Table>
                )
            }else{
                return(
                <h5 className="title"><p className="badge badge-dark bg-secondary">No Books</p></h5>
                )
            }

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
                <Modal
                    show={this.props.show}
                    size="lg"
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
                        {this.renderTable()}


                    </Modal.Body>
                    <Modal.Footer>
                        <Button
                            variant='dark'
                            onClick={this.handleOk.bind(this)}
                        >
                            <FontAwesomeIcon icon='check' fixedWidth /> Ok
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

export default BooksModal;

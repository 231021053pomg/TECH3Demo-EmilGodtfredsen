import React, { Component } from 'react';
import { Alert, Fade } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

class ShowMessage extends Component {
  render() {
    if (this.props.comment) {
      return (
        <Fade appear={true}>
          <Alert variant={this.props.variant}>
              <FontAwesomeIcon icon="info-circle" fixedWidth/> {this.props.comment}
          </Alert>
        </Fade>
      )
    } else {
      return(null);
    }
  }
}

export default ShowMessage;

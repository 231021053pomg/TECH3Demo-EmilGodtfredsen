import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Authors } from './components/Authors';
import { Books } from './components/Books';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/authors' component={Authors} />
        <Route path='/books' component={Books} />

      </Layout>
    );
  }
}

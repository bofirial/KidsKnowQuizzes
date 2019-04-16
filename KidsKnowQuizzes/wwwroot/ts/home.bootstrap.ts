///<reference types="webpack-env" />

import { Home } from './home';

Home.startApplication();

if (module.hot) {
    module.hot.accept('./home', () => {
        Home.startApplication();
    });
}
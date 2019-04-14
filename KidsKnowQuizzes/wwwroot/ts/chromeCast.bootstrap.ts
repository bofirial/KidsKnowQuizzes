///<reference types="webpack-env" />

import { ChromeCast } from './ChromeCast';

ChromeCast.startApplication();

if (module.hot) {
    module.hot.accept('./ChromeCast', () => {
        ChromeCast.startApplication();
    });
}
///<reference types="webpack-env" />

import { Site } from './Site';

Site.startApplication();

if (module.hot) {
    module.hot.accept('./Site', () => {
        Site.startApplication();
    });
}
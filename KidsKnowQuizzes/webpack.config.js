const path = require('path');
var merge = require('extendify')({ isDeep: true, arrays: 'concat' });
var devConfig = require('./webpack.config.dev');
var prodConfig = require('./webpack.config.prod');
var isDevelopment = process.env.ASPNETCORE_ENVIRONMENT === 'Development';

module.exports = merge(
    {
        entry: {
            site: [
                './wwwroot/ts/site.bootstrap.ts'
            ],
            chromeCast: [
                './wwwroot/ts/chromeCast.bootstrap.ts'
            ]
        },
        devtool: 'source-map',
        module: {
            rules: [
                {
                    test: /\.ts(x?)$/,
                    use: {
                        loader: 'ts-loader',
                        options: {
                            transpileOnly: true
                        }
                    },
                    exclude: /node_modules/
                }]
        },
        resolve: {
            extensions: ['.tsx', '.ts', '.js']
        },
        output: {
            path: path.join(__dirname, 'wwwroot', 'dist'),
            filename: '[name].js',
            publicPath: '/dist/'
        },
        plugins: []
    },
    isDevelopment ? devConfig : prodConfig);
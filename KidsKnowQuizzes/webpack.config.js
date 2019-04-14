const path = require('path');
var merge = require('extendify')({ isDeep: true, arrays: 'concat' });
var devConfig = require('./webpack.config.dev');
var prodConfig = require('./webpack.config.prod');
const CleanWebpackPlugin = require('clean-webpack-plugin');

module.exports = env => {
    var isDeployment = env && env.prod;

    return merge(
        {
            entry: {
                home: [
                    './wwwroot/ts/home.bootstrap.ts'
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
            plugins: [
                new CleanWebpackPlugin({
                    verbose: true,
                    dry: false
                })],
            optimization: {
                splitChunks: {
                    chunks: 'async',
                    cacheGroups: {
                        vendors: {
                            test: /[\\/]node_modules[\\/]/,
                            priority: -10
                        },
                        default: {
                            minChunks: 2,
                            priority: -20,
                            reuseExistingChunk: true
                        }
                    }
                }
            }
        },
        !isDeployment ? devConfig : prodConfig);
};
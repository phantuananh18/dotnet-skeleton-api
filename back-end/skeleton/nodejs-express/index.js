import express from 'express';
import Logger from './src/utils/logger.util.js';
import helmet from 'helmet';
import cors from 'cors';
import i18nextMiddleware from 'i18next-http-middleware';
import swaggerUi from 'swagger-ui-express';
import YAML from 'yaml';
import fs from 'fs';

import errorHandler from './src/middlewares/errorHandler.middleware.js';
import morganMiddleware from './src/middlewares/morgan.middleware.js';
import interceptorJsonResponseMiddleware from './src/middlewares/interceptorJsonResponse.middleware.js';
import i18next from './src/configs/i18n/i18n.config.js';
import router from './src/routes/index.js';

import 'reflect-metadata';

const app = express();
const port = +process.env.PORT || 3000;
const logger = Logger.getInstance();

app.use(cors({
    allowedHeaders: '*',
    methods: '*',
    origin: '*'
}));

app.use(express.json());
app.use(
    express.urlencoded({
        extended: true
    })
);

// i18n
app.use(i18nextMiddleware.handle(i18next, {
    removeLngFromUrl: true
}));

// Setup api document
const file = fs.readFileSync('./swagger.yml', 'utf8');
const swaggerDocument = YAML.parse(file);
app.use(
    '/api-docs',
    swaggerUi.serve,
    swaggerUi.setup(swaggerDocument)
);

app.use(helmet());

// Response interceptor
app.use(interceptorJsonResponseMiddleware);

// Log request on dev enviroment
app.use(morganMiddleware);

// Add route here
app.use('/api/v1', router);

// Error handler middleware
app.use(errorHandler);

app.listen(port, () => {
    logger.info(`The server is running on port ${port}`);
}).setTimeout(30000);
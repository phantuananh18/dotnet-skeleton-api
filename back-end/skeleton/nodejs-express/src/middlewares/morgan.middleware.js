import morgan from 'morgan';
import Logger from '../utils/logger.util.js';

const logger = Logger.getInstance();

const stream = {
    // Use the http severity
    write: (message) => logger.info(message)
};

const skip = () => {
    const env = process.env.NODE_ENV || 'development';
    return env !== 'development';
};

morgan.token('input', (req) => {
    let input;

    if (req.method === 'GET') {
        input = { ...req.query };
    } else {
        input = { ...req.body };
    }

    // Hide password value
    if(input?.password) {
        input.password = '**************';
    }

    if(input?.passwordConfirmation) {
        input.passwordConfirmation = '**************';
    }

    return JSON.stringify(input);
});

export default morgan(
    ':remote-addr :method :url - Input :input',
    { stream, skip, immediate: true }
);

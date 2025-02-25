import Logger from '../utils/logger.util.js';
import HttpResponse from '../utils/httpResponse.util.js';

const logger = Logger.getInstance();

export default (err, req, res, next) => {
    if(err) {
        logger.error(err.message, err);
        const response = HttpResponse.serverError('serverError');
        res.status(response.status).json(response);
    }
    
    next();
};
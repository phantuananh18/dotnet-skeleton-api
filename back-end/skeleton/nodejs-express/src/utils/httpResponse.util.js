import HTTPStatus from 'http-status';

/**
 * Defined some functions to build the response body
 */
export default class HttpResponse {
    /**
     * Create response with status code 200 success
     * 
     * @param {object | undefined} data The data object to be returned in the data field
     * @returns An object with code and data fields
     */
    static ok (data) {
        const options = {
            status: HTTPStatus.OK,
            code: HTTPStatus['200_NAME'],
            data
        };

        return this.#createResponse(options);
    }

    /**
     * Create response with status 500 internal server error
     * 
     * @param {string | undefined} message The brief return message
     * @param {object} error The error object
     * @returns An object with only code field is populate with INTERNAL_SERVER_ERROR
     */
    static serverError (message, error) {
        const options = {
            status: HTTPStatus.INTERNAL_SERVER_ERROR,
            code: HTTPStatus['500_NAME'],
            message: message || 'An unexpected server error occurred, please try again.',
            error
        };

        return this.#createResponse(options);
    }

    /**
     * Create response with status 401 unauthorized
     * 
     * @param {string | undefined} message The brief return message
     * @returns An object with only code field is populate with UNAUTHORIZED
     */
    static unauthorized (message) {
        const options = {
            status: HTTPStatus.UNAUTHORIZED,
            code: HTTPStatus['401_NAME'],
            message: message || 'You have to authorize before invoking this API call.'
        };

        return this.#createResponse(options);
    }

    /**
     * Create response with status 400 bad request
     * 
     * @param {string | undefined} message The brief return message
     * @param {object} error The error object
     * @returns An object with only code field is populate with BAD_REQUEST
     */
    static badRequest (message, error) {
        const options = {
            status: HTTPStatus.BAD_REQUEST,
            code: HTTPStatus['400_NAME'],
            message: message || 'Your submitted request body is missing parameters. Please double check and try again.',
            error
        };

        return this.#createResponse(options);
    }

    /**
     * Create response with status 403 forbidden
     * 
     * @param {*} message The brief return message
     * @returns An object with only code field is populate with FORBIDDEN
     */
    static forbidden (message) {
        const options = {
            status: HTTPStatus.FORBIDDEN,
            code: HTTPStatus['403_NAME'],
            message: message || 'You are not allowed to perform this action.'
        };

        return this.#createResponse(options);
    }

    /**
     * Create response with status 404 not found
     * 
     * @param {*} message The brief return message
     * @returns An object with only code field is populate with NOT_FOUND
     */
    static notFound (message) {
        const options = {
            status: HTTPStatus.NOT_FOUND,
            code: HTTPStatus['404_NAME'],
            message: message || 'Resource does not exist'
        };

        return this.#createResponse(options);
    }

    /**
     * Create a response object with specifing some attributes (e.g. code, message, data, etc.)
     * 
     * @param {Object} options Specify some options to populate response object.
     * @returns An object that populate from provided parameters
     */
    static #createResponse(options) {
        return {
            status: options.status,
            code: options.code,
            data: options.data,
            message: options.message,
            error: options.error
        };
    }
}
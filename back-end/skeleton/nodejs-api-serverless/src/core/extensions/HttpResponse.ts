import HTTPStatus from 'http-status';
const config = require(`../../../config/${process.env.NODE_ENV}.json`);

/**
 * Define some function to build the lambda response
 */
export default class HttpResponse {
    /**
     * Return OK status
     * @static
     * @param data The data response
     * @returns The response object
     */
    static ok (data: any) {
        const options = {
            status: HTTPStatus.OK,
            code: HTTPStatus['200_NAME'],
            data
        };

        return this._createResponse(options);
    }

    /**
     * Return Created status
     * @static
     * @param data The data response
     * @returns The response object
     */
    static created (data: any) {
        const options = {
            status: HTTPStatus.CREATED,
            code: HTTPStatus['201_NAME'],
            data
        };

        return this._createResponse(options);
    }

    /**
     * Return Internal Server Error status
     * @static
     * @param message The messasge response
     * @param error The error reponse
     * @returns The response object
     */
    static serverError (message: string | void, error?: object | void) {
        const options = {
            status: HTTPStatus.INTERNAL_SERVER_ERROR,
            code: HTTPStatus['500_NAME'],
            message: message || 'An unexpected server error occurred, please try again.',
            error
        };

        return this._createResponse(options);
    }

    /**
     * Return Unauthorized status
     * @static
     * @param message The messasge response
     * @returns The response object
     */
    static unauthorized (message: string | void) {
        const options = {
            status: HTTPStatus.UNAUTHORIZED,
            code: HTTPStatus['401_NAME'],
            message: message || 'You have to authorize before invoking this API call.'
        };

        return this._createResponse(options);
    }

    /**
     * Return Bad Request status
     * @static
     * @param message The messasge response
     * @param error The error reponse
     * @returns The response object
     */
    static badRequest (message: string | void, error?: object | void) {
        const options = {
            status: HTTPStatus.BAD_REQUEST,
            code: HTTPStatus['400_NAME'],
            message: message || 'Your submitted request body is missing parameters. Please double check and try again.',
            error
        };

        return this._createResponse(options);
    }

    /**
     * Return Forbidden status
     * @static
     * @param message The messasge response
     * @returns The response object
     */
    static forbidden (message: string | void) {
        const options = {
            status: HTTPStatus.FORBIDDEN,
            code: HTTPStatus['403_NAME'],
            message: message || 'You are not allowed to perform this action.'
        };

        return this._createResponse(options);
    }

    /**
     * Return Not Found status
     * @static
     * @param message The messasge response
     * @param error The error reponse
     * @returns The response object
     */
    public static notFound(message: string | void, error?: object | void): any {
        const options = {
            status: HTTPStatus.NOT_FOUND,
            message: message || 'Resource does not exist.',
            code: HTTPStatus['404_NAME'],
            error
        };

        return this._createResponse(options);
    }

    /**
     * Build response
     * @static
     * @param options The options
     * @returns The response object
     */
    private static _createResponse(options) {
        const body = {
            status: options.status,
            code: options.code,
            data: options.data,
            message: options.message,
            error: options.error
        };

        const headers = {
            'Access-Control-Allow-Headers' : config?.apig?.headers,
            'Access-Control-Allow-Origin': config?.apig?.origin,
            'Access-Control-Allow-Methods': config?.apig?.methods,
            'Content-Type': 'application/json'
        };

        return {
            statusCode: body.status,
            headers,
            body: JSON.stringify(body)
        };
    }
}
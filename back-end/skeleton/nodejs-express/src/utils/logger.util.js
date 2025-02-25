import { createLogger, transports, format } from 'winston';

/**
 * Logger utility
 */
export default class Logger {
    #baseLogger;

    constructor(logOption) {
        this.#baseLogger = this.#createLogger(logOption);
    }

    /**
     * Create a winston logger
     * 
     * @param {Object} logOption The log option
     * @returns The winston logger instance
     */
    #createLogger(logOption) {
        return createLogger({
            transports: [
                new transports.Console(logOption)
            ]
        });
    }

    /**
     * Get existing logger instance. If instance is not existed, create one
     * 
     * @returns The logger instance
     */
    static getInstance() {
        if(!Logger._instance) {
            const logOption = {
                format: format.combine(format.simple(), format.colorize({ message: true })),
                level: process.env.LOG_LEVEL || 'info',
                timestamp: true
            };

            Logger._instance = new Logger(logOption);
        }
        return Logger._instance;
    }

    /**
     * Log information
     * 
     * @param {string} message The message to log
     * @param {object} option The optional object
     */
    info(message, option) {
        this.#log(message, option, 'info');
    }

    /**
     * Log error
     * 
     * @param {string} message The message to log
     * @param {object} option The optional object
     */
    error(message, option) {
        this.#log(message, option, 'error');
    }

    /**
     * Log debug
     * 
     * @param {string} message The message to log
     * @param {object} option The optional object
     */
    debug(message, option) {
        this.#log(message, option, 'debug');
    }

    /**
     * Log the message from log level and provided option
     * 
     * @param {string} message The message to log
     * @param {object} option The optional object
     * @param {string} logLevel The log level
     */
    #log(message, option, logLevel) {
        let printMessage = message || '';

        if (option instanceof Error) {
            printMessage += '\n\r' + option.stack || option;
        }

        this.#baseLogger[logLevel](printMessage);
    }
}
import { createLogger, transports, format, Logger as WinstonLogger } from 'winston';

/**
 * Logger
 */
export default class Logger {
    private _baseLogger: WinstonLogger;
    private static _instance: Logger;

    constructor(logOption) {
        this._baseLogger = this.createLogger(logOption);
    }

    /**
     * Create logger 
     * @param logOption The log options
     * @returns 
     */
    private createLogger(logOption) {
        return createLogger({
            transports: [
                new transports.Console(logOption)
            ]
        });
    }

    /**
     * Get exiting logger instance. If it does not exist, create one
     * @returns 
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
     * Log info
     * @param message The message
     * @param option The option
     */
    info(message: string | null | void, option: object | null | void) {
        this._log(message, option, 'info');
    }

    /**
     * Log error
     * @param message The message
     * @param option The option
     */
    error(message: string | null | void, option: object | null | void) {
        this._log(message, option, 'error');
    }

    /**
     * Log debug
     * @param message The message
     * @param option The option
     */
    debug(message: string | null | void, option: object | null | void) {
        this._log(message, option, 'debug');
    }

    /**
     * Log with log level
     * @param message The message
     * @param option The option
     * @param logLevel The log level
     */
    private _log(message: string | null | void, option: object | null | void, logLevel: string) {
        let printMessage = message || '';

        if (option instanceof Error) {
            printMessage += '\n\r' + option.stack || option;
        }

        this._baseLogger[logLevel](printMessage);
    }
}
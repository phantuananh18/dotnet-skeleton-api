import nodemailer from 'nodemailer';
import hbs from 'nodemailer-express-handlebars';
import path from 'path';

/**
 * Config mailer that use Handlebars engine
 */
export default class Mailer {
    #transporter;

    /**
     * 
     * @param {string} host The hostname or IP address to connect to
     * @param {string} port The port to connect to
     * @param {string} user The Username
     * @param {string} password The password
     */
    constructor(host, port, user, password) {
        this.#transporter = nodemailer.createTransport({
            host,
            port,
            auth: {
                user,
                pass: password
            },
            secure: process.env.NODE_ENV === 'production'
        });

        this.#transporter.use('compile', hbs({
            extName: '.hbs',
            viewEngine: {
                defaultLayout: false
            },
            viewPath: path.resolve(process.cwd(), './src/templates')
        }));
    }

    /**
     * Send an email with the provided data 
     * @param {object} data The message data
     * @param {string} data.from The sender email
     * @param {string} data.to The receiver email
     * @param {string} data.subject The email subject
     * @param {string} data.text The plaintext version of the message as an Unicode string, Buffer, Stream or an attachment-like object
     * @param {string} data.html The HTML version of the message as an Unicode string, Buffer, Stream or an attachment-like object
     * @param {string} data.template The name of the template file. This file is located in the `templates` folder
     * @param {string} data.context This will be passed to the view engine to replace the expressions with value from context
     * @returns The promise represent for the sending result
     */
    sendMail(data) {
        return this.#transporter.sendMail({
            from: data.from,
            to: data.to,
            subject: data.subject,
            text: data.text,
            html: data.html,
            template: data.template,
            context: data.context
        });
    }   
}
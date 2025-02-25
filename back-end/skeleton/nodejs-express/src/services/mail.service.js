import Mailer from '../configs/mailer/mailer.config.js';

/**
 * The mail service
 */
export default class MailService {
    static #instance;

    /**
     * Get existing service instance. If it does not exist, create one
     * 
     * @returns {MailService} The service instance
     */
    static getInstance () {
        if(!MailService.#instance) {
            MailService.#instance = new MailService();
        }

        return MailService.#instance;
    }

    /**
     * Using the template to send email
     * 
     * @param {string} templateFileName The template file name. This file is located in the `templates` folder
     * @param {object} templateData The data used in the template is to replace the expressions with values from this data
     * @param {string} sender The email adress of sender
     * @param {string} receiver The email adderss of receiver
     * @param {string} subject The subject email
     * @returns The promise represents the sending email result
     */
    sendEmailByUsingTemplate(templateFileName, templateData, sender, receiver, subject) {
        const mailer = new Mailer(process.env.NOREPLY_MAIL_HOST, +process.env.NOREPLY_MAIL_PORT, process.env.NOREPLY_MAIL_USER, process.env.NOREPLY_MAIL_PASSWORD);

        return mailer.sendMail({
            from: sender,
            to: receiver,
            subject,
            template: templateFileName,
            context: templateData
        });
    }
}
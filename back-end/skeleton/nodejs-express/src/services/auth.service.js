import bcrypt from 'bcrypt';
import jwt from 'jsonwebtoken';

import HttpResponse from '../utils/httpResponse.util.js';
import Logger from '../utils/logger.util.js';
import { UserField, RoleType, QueryOperator } from '../common/constant.js';

import UserRepository from '../repositories/user.repository.js';
import RefreshTokenRepository from '../repositories/refreshtoken.repository.js';
import RoleRepository from '../repositories/role.repository.js';
import MailService from './mail.service.js';
import * as Constants from '../common/constant.js';

const logger = Logger.getInstance();

/**
 * The service used to authentiacate
 */
export default class AuthService {
    static #instance;

    #roleRepository;
    #userRepository;
    #refreshtokenRepository;
    #mailRepository;

    constructor() {
        this.#userRepository = UserRepository.getInstance();
        this.#refreshtokenRepository = RefreshTokenRepository.getInstance();
        this.#roleRepository = RoleRepository.getInstance();
        this.#mailRepository = MailService.getInstance();
    }

    /**
     * Get existing service instance. If it does not exist, create one
     * 
     * @returns {AuthService} The service instance
     */
    static getInstance() {
        if (!AuthService.#instance) {
            AuthService.#instance = new AuthService();
        }

        return AuthService.#instance;
    }

    /**
     * Sign up a new user
     * 
     * @param {object} param The user info used to sign up
     * @param {string} param.username The username
     * @param {string} param.password The password
     * @param {string} param.email The email address
     * @param {string} param.firstName The first name of user
     * @param {string} param.lastName The last name of user
     * @param {string} param.mobilePhone The mobile phone
     * @param {string} param.role The user role
     * @returns The promise represent an object that contains the response status and the created user data
     */
    async signUp({ username, password, email, firstName, lastName, mobilePhone, role }) {
        try {
            // Check if user exists
            const existingUser = await this.#checkUserExistence([
                { field: UserField.USERNAME, value: username },
                { field: UserField.EMAIL, value: email },
                { field: UserField.MOBILE_PHONE, value: mobilePhone }
            ]);

            if (existingUser) {
                if (existingUser.isDeleted) {
                    return HttpResponse.badRequest('deactivatedAccount');
                }
                return HttpResponse.badRequest('accountAlreadyExists');
            }

            // Validate role
            const existingRole = await this.#roleRepository.findRoleByName(role ?? RoleType.CLIENT);
            if (!existingRole || existingRole.isDeleted) {
                return HttpResponse.badRequest('invalidRole');
            }

            // Hash password asynchronously
            const hashedPassword = await bcrypt.hash(password, +process.env.BCRYPT_SALT_ROUND);
            const user = await this.#userRepository.save({ username, password: hashedPassword, email, firstName, lastName, mobilePhone, role: existingRole });

            if (user) {
                delete user.password;
            }

            return !user
                ? HttpResponse.badRequest('somethingWentWrong')
                : HttpResponse.ok(user);
        }
        catch (error) {
            logger.error('[SignUp] Exception: ', error);
            throw error;
        }
    }

    /**
     * Check user is existing or not
     * 
     * @private
     * @param {Array<object>} criteriaList The list of criteria
     * @returns The promise represent an user data
     */
    async #checkUserExistence(criteriaList) {
        const user = await this.#userRepository.findUserByCriteria(criteriaList, QueryOperator.OR);
        const verificationResult = user && !user.isDeleted;
        if (verificationResult) {
            return user;
        }

        return null;
    }

    /***
     * Sign in with username and password. Generate access token and refresh token
     * 
     * @param {object} param The info to sign in
     * @param {string} param.username: The username of credential
     * @param {string} param.password: The password of credential
     * @returns The promise represent an object that contains the response status and the tokens (access token and refresh token)
     */
    async signIn({ username, password }) {
        try {
            const user = await this.#userRepository.findUserAndRelatedDataByCriteria(UserField.USERNAME, username);
            if (!user || user.isDeleted) {
                return HttpResponse.badRequest('accountNotFound');
            }

            if (!await this.#verifyPassword(password, user.password)) {
                return HttpResponse.unauthorized('incorrectPassword');
            }

            const accessToken = await this.#generateAccessToken(user.userId, user.role.name);
            const refreshToken = await this.#generateRefreshToken(user.userId);
            const userInformation = {
                userId: user.userId,
                username: user.username,
                email: user.email,
                firstName: user.firstName,
                lastName: user.lastName,
                mobilePhone: user.mobilePhone,
                role: user.role.name
            };

            if (!accessToken || !refreshToken) {
                logger.error('Token is null or empty');
                return HttpResponse.serverError('somethingWentWrong');
            }

            await this.#refreshtokenRepository.save({ userId: user.userId, token: refreshToken, createdBy: user.userId });
            return HttpResponse.ok({ accessToken, refreshToken, userInformation });
        }
        catch (error) {
            logger.error('[SignIn] Exception: ', error);
            throw error;
        }
    }

    /**
     * Verify password
     * 
     * @param {string} password The raw password
     * @param {string} hashedPassword The hashed password
     * @returns The promise represents the camparison result
     */
    async #verifyPassword(password, hashedPassword) {
        return await bcrypt.compare(password, hashedPassword);
    }

    /**
     * Generete a new access token
     * 
     * @param {number} userId The id of user
     * @param {string} roleName The name of role
     * @returns The promise represents the new access token
     */
    async #generateAccessToken(userId, roleName) {
        return jwt.sign({ id: userId, role: roleName }, process.env.JWT_SECRET_KEY, { expiresIn: process.env.JWT_EXPIRATION_TIME });
    }

    /**
     * Generete a new refresh token
     * 
     * @param {number} userId The id of user
     * @returns The promise represents the new refresh token
     */
    async #generateRefreshToken(userId) {
        return jwt.sign({ id: userId }, process.env.REFRESH_TOKEN_SECRET_KEY, { expiresIn: process.env.REFRESH_EXPIRATION_TIME });
    }

    /**
     * Generates a new token using a refresh token.
     * 
     * @param {string} refreshToken The refresh token used to generate the new token.
     * @returns {string} The promise represent an object that contains the response status and the access token
     */
    async generateAccessTokenFromRefreshToken(refreshToken) {
        try {
            const decoded = jwt.verify(refreshToken, process.env.REFRESH_TOKEN_SECRET_KEY);
            const existingUser = await this.#userRepository.findUserAndRelatedDataByCriteria('userId', decoded.id);
            if (!existingUser || existingUser.isDeleted) {
                return HttpResponse.badRequest('accountNotFound');
            }

            const accessToken = await this.#generateAccessToken(existingUser.userId, existingUser.role.name);
            if (!accessToken) {
                logger.error('Token is null or empty');
                return HttpResponse.serverError('somethingWentWrong');
            }

            return HttpResponse.ok({ accessToken });
        }
        catch (error) {
            switch (error.constructor) {
                case jwt.JsonWebTokenError:
                    logger.error('Refresh token verification failed: ', error);
                    break;
                case jwt.NotBeforeError:
                    logger.error('Refresh token verification failed: ', error);
                    break;
                case jwt.TokenExpiredError:
                    logger.error('Refresh token expired: ', error);
                    break;
            }

            return HttpResponse.serverError('somethingWentWrong');
        }
    }

    /**
     * Send the forgot password email that contains the instructions to reset password.
     * 
     * @param {string} email The user email 
     * @returns The promise represent an object that contains the response status
     */
    async forgotPassword(email) {
        const existingUser = await this.#userRepository.findOneBy({ email, isDeleted: false });

        if (!existingUser) {
            return HttpResponse.badRequest('accountNotFound');
        }

        const token = jwt.sign({ email: existingUser.email }, process.env.RESET_PASSWORD_JWT_SECRET_KEY, { expiresIn: process.env.RESET_PASSWORD_JWT_EXPIRATION_TIME });

        const rs = await this.#mailRepository.sendEmailByUsingTemplate(
            Constants.EmailTemplateFileName.FORGOT_PASSWORD,
            { token },
            process.env.NO_REPLY_EMAIL,
            existingUser.email,
            Constants.EmailSubject.RESET_PASSWORD
        );

        logger.info(`Send email sucessfully. Response: ${JSON.stringify(rs)}`);

        return HttpResponse.ok();
    }

    /**
     * Reset to new password
     * 
     * @param {string} password The password the user would like to update
     * @param {string} token The token that use verify the request
     * @returns The promise represent an object that contains the response status
     */
    async resetPassword(password, token) {
        let decoded = null;
        try {
            decoded = jwt.verify(token, process.env.RESET_PASSWORD_JWT_SECRET_KEY);

        } catch (error) {
            if (error instanceof jwt.TokenExpiredError) {
                return HttpResponse.badRequest('requestExpired');
            }

            if (error instanceof jwt.JsonWebTokenError) {
                return HttpResponse.badRequest('invalidToken');
            }

            throw error;
        }

        const user = await this.#userRepository.findOneBy({ email: decoded.email, isDeleted: false });

        if (!user) {
            return HttpResponse.badRequest('userIsNotExisting');
        }

        user.password = await bcrypt.hash(password, +process.env.BCRYPT_SALT_ROUND);
        await this.#userRepository.save(user);

        const rs = await this.#mailRepository.sendEmailByUsingTemplate(
            Constants.EmailTemplateFileName.RESET_PASSWORD_SUCCESSFUL,
            {},
            process.env.NO_REPLY_EMAIL,
            user.email,
            Constants.EmailSubject.RESET_PASSWORD_SUCCESSFUL
        );

        logger.info(`Send email sucessfully. Response: ${JSON.stringify(rs)}`);

        return HttpResponse.ok();
    }
}
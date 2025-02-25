import AuthService from '../services/auth.service.js';
import HttpResponse from '../utils/httpResponse.util.js';
import { signUpSchema } from '../schemas/user.schema.js';
import { signInSchema, refreshTokenSchema, resetPasswordSchema } from '../schemas/auth.schema.js';

const authService = AuthService.getInstance();

/**
 * Register a new user
 * 
 * @param {Object} req The request object
 * @param {Object} res The response object
 * @param {Function} next The next function
 * @returns The promise represents the data response
 */
async function signUp(req, res, next) {
    try {
        const { error } = signUpSchema.validate(req.body);

        if (error) {
            const response = HttpResponse.badRequest(null, error.details);
            return res.status(response.status).json(response);
        }

        const response = await authService.signUp(req.body);
        return res.status(response.status).json(response);
    }
    catch(err) {
        next(err);
    }
}

/**
 * Sign in by username and password
 * 
 * @param {Object} req The request object
 * @param {Object} res The response object
 * @param {Function} next The next function 
 * @returns The promise represents the data response
 */
async function signIn(req, res, next) {
    try {
        const {error} = signInSchema.validate(req.body);
        if (error) {
            const response = HttpResponse.badRequest(null, error.details);
            return res.status(response.status).json(response);
        }

        const response = await authService.signIn(req.body);
        return res.status(response.status).json(response);
    }
    catch(err) {
        next(err);
    }
}

/**
 * Generate a new access token
 * 
 * @param {Object} req The request object
 * @param {Object} res The response object
 * @param {Function} next The next function
 * @returns The promise represents the data response
 */
async function generateAccessTokenFromRefreshToken(req, res, next) {
    try {
        const {error} = refreshTokenSchema.validate(req.body);
        if (error) {
            const response = HttpResponse.badRequest(null, error.details);
            return res.status(response.status).json(response);
        }

        const response = await authService.generateAccessTokenFromRefreshToken(req.body.refreshToken);
        return res.status(response.status).json(response);
    }
    catch(err) {
        next(err);
    }
}

/**
 * Send the reset password email to reset password
 * 
 * @param {object} req The request object
 * @param {object} res The response object
 * @param {Function} next The next function
 * @returns The promise represents the data response
 */
async function forgotPassword(req, res, next){
    try {
        const email = decodeURIComponent(req.params.email);

        const response = await authService.forgotPassword(email);
        return res.status(response.status).json(response);
    }
    catch(err) {
        next(err);
    }
}

/**
 * Reset the current user password to new password
 * 
 * @param {object} req The request object
 * @param {object} res The response object
 * @param {Function} next The next function
 * @returns The promise represents the data response
 */
async function resetPassword(req, res, next){
    try {
        const { error } = resetPasswordSchema.validate(req.body);
        if (error) {
            const response = HttpResponse.badRequest(null, error.details);
            return res.status(response.status).json(response);
        }

        const response = await authService.resetPassword(req.body.password, req.query.token);
        return res.status(response.status).json(response);
    }
    catch(err) {
        next(err);
    }
}

export {
    signUp,
    signIn,
    generateAccessTokenFromRefreshToken,
    forgotPassword,
    resetPassword
};
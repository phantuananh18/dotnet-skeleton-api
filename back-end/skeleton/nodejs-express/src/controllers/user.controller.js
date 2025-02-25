import UserService from '../services/user.service.js';
import AuthService from '../services/auth.service.js';
import { signUpSchema } from '../schemas/user.schema.js';
import HttpResponse from '../utils/httpResponse.util.js';

const userService = UserService.getInstance();
const authService = AuthService.getInstance();

/**
 * Get the user list that match with the search text
 * 
 * @param {object} req The request object
 * @param {object} res The response object
 * @param {Function} next The next function
 * @returns The promise represents the user list
 */
async function getUsersWithSearch(req, res, next) {
    try {
        const keyword = req.query.search ?? '';
        const response = await userService.getUsersWithSearch(keyword);
        return res.status(response.status).json(response);
    }
    catch(err) {
        next(err);
    }
}

/**
 * Get an existing user by userId
 * 
 * @param {object} req The request object
 * @param {object} res The response object
 * @param {Function} next The next function
 * @returns The promise represents the user
 */
async function getUserByUserId(req, res, next) {
    try {
        const response = await userService.getUserByUserId(req.params.id);
        return res.status(response.status).json(response);
    }
    catch(err) {
        next(err);
    }
}

/**
 * Create a new user
 * 
 * @param {object} req The request object
 * @param {object} res The response object
 * @param {Function} next The next function
 * @returns The promise represents a created user
 */
async function createUser(req, res, next) {
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
 * Update a exsting user
 * 
 * @param {object} req The request object
 * @param {object} res The response object
 * @param {Function} next The next function
 * @returns The promise represents a updated user
 */
async function updateUser(req, res, next) {
    try {
        const userId = req.params.id;
        const { error } = signUpSchema.validate(req.body);
        if (error) {
            const response = HttpResponse.badRequest(null, error.details);
            return res.status(response.status).json(response);
        }

        const response = await userService.updateUser(userId, req.body);
        return res.status(response.status).json(response);
    }
    catch(err) {
        next(err);
    }
}

/**
 * Delete a existing user
 * 
 * @param {object} req The request object
 * @param {object} res The response object
 * @param {Function} next The next function
 * @returns The promise represents a deleted user
 */
async function deleteUser(req, res, next){
    try {
        const userId = req.params.id;
        if (userId <= 0 ) {
            const response = HttpResponse.badRequest();
            return res.status(response.status).json(response);
        }

        const response = await userService.deleteUser(userId);
        return res.status(response.status).json(response);
    }
    catch(err) {
        next(err);
    }
}

export {
    getUsersWithSearch,
    createUser,
    updateUser,
    deleteUser,
    getUserByUserId
};
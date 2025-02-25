import bcrypt from 'bcrypt';

import UserRepository from '../repositories/user.repository.js';
import RoleRepository from '../repositories/role.repository.js';
import HttpResponse from '../utils/httpResponse.util.js';

/**
 * The user service
 */
export default class UserService {
    static #instance;

    #userRepository;
    #roleRepository;
    

    constructor() {
        this.#userRepository = UserRepository.getInstance();
        this.#roleRepository = RoleRepository.getInstance();
    }

    /**
     * Get existing service instance. If it does not exist, create one
     * 
     * @returns {UserService} The service instance
     */
    static getInstance () {
        if(!UserService.#instance) {
            UserService.#instance = new UserService();
        }

        return UserService.#instance;
    }

    /**
     * Get user data by the search text
     * @param {string} keyword The search text
     * @returns The promise represent an object that contains the response status and the user list
     */
    async getUsersWithSearch(keyword) {
        const users = await this.#userRepository.findUserWithSearch(keyword);
        return HttpResponse.ok(users ?? []);
    }

    /**
     * Get an existing user data by userId
     * @param {string} userId The Id of user
     * @returns The promise represent an object that contains the response status and the user
     */
    async getUserByUserId(userId) {
        const user = await this.#userRepository.findOneBy({ userId });

        if (user) {
            delete user.password;
            return HttpResponse.ok(user);
        }

        return HttpResponse.notFound('userIsNotFound');
    }

    /**
     * Update the existing user 
     * 
     * @param {number} userId The id of the existing user
     * @param {object} user The user info used to update
     * @returns The promise represent an object that contains the response status and the updated user
     */
    async updateUser(userId, user) {
        // Check existing user
        const existingUser = await this.#userRepository.findOneByPrimaryId(userId);
        if (!existingUser || existingUser.isDeleted) {
            return HttpResponse.badRequest('accountNotFound');
        }

        // Validate role
        const existingRole = await this.#roleRepository.findRoleByName(user.role);
        if (!existingRole || existingRole.isDeleted) {
            return HttpResponse.badRequest('invalidRole');
        }

        const targetUser = { ...existingUser, ...user };
        targetUser.hashedPassword = await bcrypt.hash(user.password, +process.env.BCRYPT_SALT_ROUND);
        targetUser.role = existingRole;

        const updateUserResult = await this.#userRepository.save(targetUser);

        if (updateUserResult) {
            delete updateUserResult.password;
            return HttpResponse.ok(updateUserResult);
        }

        return HttpResponse.badRequest('somethingWentWrong');
    }

    /**
     * Delete the existing user (sort delete)
     * @param {*} userId The id of the existing user
     * @returns The promise represent an object that contains the response status and the deleted user
     */
    async deleteUser(userId) {
        const targetUser = await this.#userRepository.findOneByPrimaryId(userId);
        if (!targetUser || targetUser.isDeleted) {
            return HttpResponse.badRequest('accountNotFound');
        }

        // Soft delete
        targetUser.isDeleted = true;
        const deleteUserResult = await this.#userRepository.save(targetUser);

        if (deleteUserResult) {
            delete deleteUserResult.password;
            return HttpResponse.ok(deleteUserResult);
        }

        return HttpResponse.badRequest('somethingWentWrong');
    }
}
import BaseRepository from './base.repository.js';
import Role from '../entities/role.entity.js';

/**
 * Repository of Role
 */
export default class RoleRepository extends BaseRepository {
    static #instance;

    constructor() {
        super(Role);
    }

    /**
     * Get existing repository instance. If it does not exist, create one
     * 
     * @returns {Role} The repository instance
     */
    static getInstance() {
        if(!RoleRepository.#instance) {
            RoleRepository.#instance = new RoleRepository();
        }

        return RoleRepository.#instance;
    }

    /**
     * Get role data by role name
     * 
     * @param {number} userId The id of user
     * @returns The promise represents the role data
     */
    async findRoleByName(name) {
        return await this._dataSouceRepository.findOneBy({ name });
    }
}
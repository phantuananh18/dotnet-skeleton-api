import BaseRepository from './base.repository.js';
import User from '../entities/user.entity.js';

/**
 * Repository of User
 */
export default class UserRepository extends BaseRepository {
    static #instance;

    constructor() {
        super(User);
    }

    /**
     * Get existing repository instance. If it does not exist, create one
     * 
     * @returns {UserRepository} The repository instance
     */
    static getInstance() {
        if(!UserRepository.#instance) {
            UserRepository.#instance = new UserRepository();
        }

        return UserRepository.#instance;
    }
    
    /**
     * Get user data by userId
     * 
     * @param {number} userId The id of user 
     * @returns The promise represents the user data
     */
    async findUserByUserId(userId) {
        return await this._dataSouceRepository.findOneBy({
            userId
        });
    }

    /**
     * Get user data by criteria
     * 
     * @param {Array} criteriaList The list of criteria
     * @param {string} condition The condition
     * @returns The promise represents the user data
     */
    async findUserByCriteria(criteriaList, condition = 'and') {
        let query = this._dataSouceRepository.createQueryBuilder('user');
        switch (condition) {
            case 'and':
                for (const criteria of criteriaList) {
                    query = query.andWhere(`user.${criteria.field} = :value`, { value: criteria.value });
                }
                break;
            case 'or':
                query = query.where(builder => {
                    criteriaList.forEach((criteria, index) => {
                        if (index === 0) {
                            builder.orWhere(`user.${criteria.field} = :value`, { value: criteria.value });
                        } else {
                            builder.orWhere(`user.${criteria.field} = :value${index}`, { [`value${index}`]: criteria.value });
                        }
                    });
                });
                break;
        }

        return await query.getOne();
    }

    /**
     * Get user and related data by criteria
     * 
     * @param {*} criteriaName The column name
     * @param {*} criteriaValue The value
     * @returns The promise represents the user data
     */
    async findUserAndRelatedDataByCriteria(criteriaName, criteriaValue) {
        const whereCondition = {};
        whereCondition[criteriaName] = criteriaValue; 
        return await this._dataSouceRepository
            .createQueryBuilder('user')
            .innerJoinAndSelect('user.role', 'role')
            .where(whereCondition)
            .getOne();
    }

    /**
     * Get list of user data by the search text
     * 
     * @param {string} keyword The search text
     * @returns The promise represents the list of user data
     */
    async findUserWithSearch (keyword) {
        return await this._dataSouceRepository
            .createQueryBuilder('user')
            .select(['user.userId', 'user.username', 'user.email', 'user.firstName', 'user.lastName', 'user.mobilePhone, user.department', 'user.jobTitle', 'user.createdDate', 'user.updatedDate', 'user.createdBy', 'user.updatedBy', 'user.isDeleted'])
            .orWhere('user.firstName like :keyword', { keyword:`%${keyword}%` } )
            .orWhere('user.lastName like :keyword', { keyword:`%${keyword}%` } )
            .orWhere('user.email like :keyword', { keyword:`%${keyword}%` })
            .getMany();
    }
}
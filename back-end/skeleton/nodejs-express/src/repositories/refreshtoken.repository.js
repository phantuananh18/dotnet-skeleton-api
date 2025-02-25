import BaseRepository from './base.repository.js';
import RefreshToken from '../entities/refreshToken.entity.js';

/**
 * Repository of Refresh Token
 */
export default class RefreshTokenRepository extends BaseRepository {
    static #instance;

    constructor() {
        super(RefreshToken);
    }

    /**
     * Get existing repository instance. If it does not exist, create one
     * 
     * @returns {RefreshToken} The repository instance
     */
    static getInstance() {
        if(!RefreshTokenRepository.#instance) {
            RefreshTokenRepository.#instance = new RefreshTokenRepository();
        }

        return RefreshTokenRepository.#instance;
    }

    /**
     * Get existing refresh token by userId
     * 
     * @param {number} userId The id of user
     * @returns The promise represents the refresh token data
     */
    async findTokenByUserId(userId) {
        return await this._dataSouceRepository.findOneBy({
            userId
        });
    }
}
import { DataSource } from 'typeorm';
import Logger from '../../utils/logger.util.js';
import User from '../../enities/user.entity.js';
import Role from '../../enities/role.entity.js';
import RefreshToken from '../../enities/refreshToken.entity.js';

const logger = Logger.getInstance();

/**
 * Config MongoDB data source
 */
export default class MongoDBDataSource {
    static #instance;

    /**
     * Get existing data source instance. If instance is not existed, create one
     * 
     * @returns The data source instance
     */
    static getInstance() {
        if (!MongoDBDataSource.#instance) {
            MongoDBDataSource.#instance = new DataSource({
                type: 'mongodb',
                url: process.env.MONGODB_CONNECT_URL,
                entities: [
                    User,
                    Role,
                    RefreshToken
                ],
                logging: 'all',
                logger: 'advanced-console',
                poolSize: 4,
                maxQueryExecutionTime: 3000,
                connectTimeoutMS: 10000,
                reconnectTries: 5
            });

            MongoDBDataSource.#instance.initialize()
                .then(() => {
                    logger.info('Init database sucessfully');
                })
                .catch((error) => logger.error('Init database failed', error));
        }

        return MongoDBDataSource.#instance;
    }
}
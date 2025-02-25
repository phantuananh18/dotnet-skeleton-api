import { DataSource } from 'typeorm';
import Logger from '../../utils/logger.util.js';
import User from '../../entities/user.entity.js';
import Role from '../../entities/role.entity.js';
import RefreshToken from '../../entities/refreshToken.entity.js';

const logger = Logger.getInstance();

/**
 * Config Mysql data source
 */
export default class MySqlDataSource {
    static #instance;

    /**
     * Get existing data source instance. If instance is not existed, create one
     * 
     * @returns The data source instance
     */
    static getInstance() {
        if (!MySqlDataSource.#instance) {
            MySqlDataSource.#instance = new DataSource({
                type: 'mysql',
                host: process.env.MYSQL_HOST,
                port: process.env.MYSQL_PORT || 3306,
                username: process.env.MYSQL_USERNAME,
                password: process.env.MYSQL_PASSWORD,
                database: process.env.MYSQL_DATABASE,
                entities: [
                    User,
                    Role,
                    RefreshToken
                ],
                logging: 'all',
                logger: 'advanced-console',
                poolSize: 4,
                maxQueryExecutionTime: 3000
            });

            MySqlDataSource.#instance.initialize()
                .then(() => {
                    logger.info('Init database sucessfully');
                })
                .catch((error) => logger.error('Init database failed', error));
        }

        return MySqlDataSource.#instance;
    }
}
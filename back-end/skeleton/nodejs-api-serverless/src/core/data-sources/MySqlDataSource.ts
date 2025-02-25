import { DataSource } from 'typeorm';
import User from '../../entities/User';
import Role from '../../entities/Role';
import Metadata from '../../entities/Metadata';
import MetadataCategory from '../../entities/MetadataCategory';
import Campaign from '../../entities/Campaign';

/**
 * Class of mysql data source
 */
export default class MySqlDataSource {
    static _instance: MySqlDataSource;
    private _dataSource: DataSource;

    constructor() {
        this._dataSource = new DataSource({
            type: 'mysql',
            host: process.env.MYSQL_HOST,
            port: process.env.MYSQL_PORT ? +process.env.MYSQL_PORT : 3306,
            username: process.env.MYSQL_USERNAME,
            password: process.env.MYSQL_PASSWORD,
            database: process.env.MYSQL_DATABASE,
            entities: [
                User,
                Role,
                Metadata,
                MetadataCategory,
                Campaign
            ],
            logging: 'all',
            logger: 'advanced-console',
            poolSize: 4,
            maxQueryExecutionTime: 3000
        });
    }

    /**
     * Get existing data source instance. If instance is not existed, create one
     * @static
     * @returns The data source instance
     */
    static getInstance() {
        if (!MySqlDataSource._instance) {
            MySqlDataSource._instance = new MySqlDataSource();
        }

        return MySqlDataSource._instance;
    }

    /**
     * Get mysql data source
     * @returns 
     */
    public getDatasource() {
        return this._dataSource;
    }

    /**
     * Used to in initialize data source
     * @returns The promise
     */
    public initialize() {
        return this._dataSource.initialize();
    }

    /**
     * Check the database connected or not
     * @returns Return true if db connected. Otherwise, return false
     */
    public isDBConnected() {
        return this._dataSource.isInitialized;
    }
}
import MySqlDataSource from '../configs/db/mysql.config.js';

/**
 * Base Repository
 */
export default class BaseRepository {
    constructor (entitySchema) {
        this._dataSouceRepository = MySqlDataSource.getInstance().getRepository(entitySchema);
    }

    /**
     * Finds a single entity by primary column
     * 
     * @param {number} id The value of primary column
     * @returns A promise that resolves to the found entity, or undefined if not found.
     */
    findOneByPrimaryId(id) {
        const primaryColumn = this.getPrimaryColumnName();
        return this._dataSouceRepository.findOneBy({ [`${primaryColumn}`]: id });
    }

    /**
     * Finds a single entity based on the provided criteria.
     * 
     * @param {FindOptionsWhere<T>} criterias - The criteria used to search for the entity.
     * @returns A promise that resolves to the found entity, or undefined if not found.
     */
    findOneBy(criterias) {
        return this._dataSouceRepository.findOneBy(criterias);
    }

    /**
     * Finds a single entity based on the provided criteria.
     * 
     * @param {FindOneOptions<T>} criterias - The criteria used to search for the entity.
     * @returns A promise that resolves to the found entity, or undefined if not found.
     */
    findOne(criterias) {
        return this._dataSouceRepository.findOne(criterias);
    }

    /**
     * Finds entities based on the provided criteria.
     * 
     * @param {FindManyOptions<T>} criterias - The criteria to filter the entities.
     * @returns A promise that resolves to an array of entities matching the criteria.
     */
    find(criterias) {
        return this._dataSouceRepository.find(criterias);
    }

    /**
     * Saves the given entity to the data source repository.
     * 
     * @param {any} entity - The entity to be saved.
     * @param {SaveOptions} options - Optional save options.
     * @returns A promise that resolves to the saved entity.
     */
    save(entity) {
        return this._dataSouceRepository.save(entity);
    }

    /**
     * Get the primary key column name of the current table
     * 
     * @returns The primary key column name of the current table
     */
    getPrimaryColumnName() {
        return this._dataSouceRepository.metadata.columns.find(item => item.isPrimary).propertyName;
    }
}
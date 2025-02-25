import { Repository, FindOptionsWhere, FindManyOptions, SaveOptions, EntityTarget, FindOneOptions } from 'typeorm';

export default class BaseRepository<T> {
    protected dataSouceRepository: Repository<T>;

    constructor (entitySchema: EntityTarget<T>) {
        this.dataSouceRepository = global['datasource'].getRepository(entitySchema);
    }

    /**
     * Finds a single entity based on the provided criteria.
     * 
     * @param {FindOptionsWhere<T>} criterias - The criteria used to search for the entity.
     * @returns A promise that resolves to the found entity, or undefined if not found.
     */
    findOneBy(criterias: FindOptionsWhere<T>) {
        return this.dataSouceRepository.findOneBy(criterias);
    }

    /**
     * Finds a single entity based on the provided criteria.
     * 
     * @param {FindOneOptions<T>} criterias - The criteria used to search for the entity.
     * @returns A promise that resolves to the found entity, or undefined if not found.
     */
    findOne(criterias: FindOneOptions<T>) {
        return this.dataSouceRepository.findOne(criterias);
    }

    /**
     * Finds entities based on the provided criteria.
     * 
     * @param {FindManyOptions<T>} criterias - The criteria to filter the entities.
     * @returns A promise that resolves to an array of entities matching the criteria.
     */
    find(criterias: FindManyOptions<T>) {
        return this.dataSouceRepository.find(criterias);
    }

    /**
     * Saves the given entity to the data source repository.
     * 
     * @param {any} entity - The entity to be saved.
     * @param {SaveOptions} options - Optional save options.
     * @returns A promise that resolves to the saved entity.
     */
    save(entity: any, options: SaveOptions = { transaction: false }) {
        return this.dataSouceRepository.save(entity, options);
    }
}
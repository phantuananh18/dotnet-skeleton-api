import Metadata from '../entities/Metadata';
import BaseRepository from './BaseRepository';

export default class MetadataRepository extends BaseRepository<Metadata> {
    private static _instance: MetadataRepository;
    constructor() {
        super(Metadata);
    }

    /**
     * Returns an instance of the MetadataRepository class.
     * If an instance already exists, it returns the existing instance.
     * If no instance exists, it creates a new instance and returns it.
     * @returns An instance of the MetadataRepository class.
     */
    static getInstance() {
        if(!MetadataRepository._instance) {
            MetadataRepository._instance = new MetadataRepository();
        }

        return MetadataRepository._instance;
    }

    /**
     * Finds metadata by category name.
     * @param {string} categoryName - The name of the category to search for.
     * @returns A Promise that resolves to an array of metadata objects.
     */
    async findMetadataByCategory(categoryName: string) {
        return await this.dataSouceRepository
            .createQueryBuilder('metadata')
            .innerJoinAndSelect('metadata.metadataCategory', 'metadataCategory')
            .where('metadataCategory.name like :categoryName', { categoryName:`%${categoryName}%` } )
            .andWhere('metadata.isDeleted = false')
            .getMany();
    }
}
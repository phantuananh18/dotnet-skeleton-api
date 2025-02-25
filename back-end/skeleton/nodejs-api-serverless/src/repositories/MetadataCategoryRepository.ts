import MetadataCategory from '../entities/MetadataCategory';
import BaseRepository from './BaseRepository';

/**
 * Metadata Category Repository
 * @extends {BaseRepository}
 */
export default class MetadataCategoryRepository extends BaseRepository<MetadataCategory> {
    private static _instance: MetadataCategoryRepository;

    constructor() {
        super(MetadataCategory);
    }

    /**
     * Get existing repository instance. If it does not exist, create one
     * @returns The repository instance
     */
    static getInstance() {
        if(!MetadataCategoryRepository._instance) {
            MetadataCategoryRepository._instance = new MetadataCategoryRepository();
        }

        return MetadataCategoryRepository._instance;
    }
}
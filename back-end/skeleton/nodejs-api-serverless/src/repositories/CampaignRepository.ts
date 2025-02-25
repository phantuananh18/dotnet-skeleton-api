import Campaign from '../entities/Campaign';
import BaseRepository from './BaseRepository';

/**
 * Campaign Repository
 * @extends {BaseRepository}
 */
export default class CampaignRepository extends BaseRepository<Campaign> {
    private static _instance;

    constructor() {
        super(Campaign);
    }

    /**
     * Get existing repository instance. If it does not exist, create a new one
     * @returns The repository instance
     */
    static getInstance() {
        if(!CampaignRepository._instance) {
            CampaignRepository._instance = new CampaignRepository();
        }

        return CampaignRepository._instance;
    }

    /**
     * Get list of campaign with paging by using SP GetCampains
     * @param status The campaign status
     * @param pageNumber The page number
     * @param pageSize The max record in a page
     * @returns The promise that resolve to the result from SP
     */
    public async getCampaignWithPaging(status: string, pageNumber: number = 1, pageSize: number = 5) {
        return await this.dataSouceRepository.query('CALL GetCampains(?, ?, ?, ?)', [`c.Status = "${status}"`, null, pageSize, pageSize * (pageNumber - 1)]);
    }
}
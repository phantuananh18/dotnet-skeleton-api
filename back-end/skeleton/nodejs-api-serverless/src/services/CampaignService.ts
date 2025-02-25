import { MetadataCategoryName } from '../common/Constant';
import HttpResponse from '../core/extensions/HttpResponse';
import S3Util from '../core/aws/S3Util';
import { CampaignStatus } from '../entities/Campaign';
import CampaignRepository from '../repositories/CampaignRepository';
import MetadataCategoryRepository from '../repositories/MetadataCategoryRepository';
import MetadataRepository from '../repositories/MetadataRepository';
import { CLS } from '../core/ContinuationLocalStorage';
import Metadata from '../entities/Metadata';

const config = require(`../../config/${process.env.NODE_ENV}.json`);

/**
 * Campaign Service
 */
export default class CampaignService {
    private static _instance;

    private _campaignRepository: CampaignRepository;
    private _metadataRepository: MetadataRepository;
    private _metadataCategoryRepository: MetadataCategoryRepository;

    constructor() {
        this._campaignRepository = CampaignRepository.getInstance();
        this._metadataRepository = MetadataRepository.getInstance();
        this._metadataCategoryRepository = MetadataCategoryRepository.getInstance();
    }

    /**
     * Get the existing repository instance. If it does not exist, create one 
     * @returns The repository instance
     */
    static getInstance () {
        if(!CampaignService._instance) {
            CampaignService._instance = new CampaignService();
        }

        return CampaignService._instance;
    }

    /**
     * Get list campaign with paging
     * @param status The campaign status
     * @param pageNumber The number of page. Default 1
     * @param pageSize The max records in a page. Default 5
     * @returns The promise that resolves the data response (includes: total of campaigns, total of pages, campaign records)
     */
    async getCampaignWithPaging(status: string, pageNumber: number = 1, pageSize: number = 5) {
        const [campaignsData, totalRecordsData] = await this._campaignRepository.getCampaignWithPaging(status, pageNumber, pageSize);

        return HttpResponse.ok({
            totalRecords: totalRecordsData && totalRecordsData[0] ? +totalRecordsData[0].TotalRecord : 0,
            totalPages: totalRecordsData && totalRecordsData[0] ? Math.ceil(+totalRecordsData[0].TotalRecord / pageSize): 0,
            campaigns: campaignsData ? campaignsData.map(item => ({
                campaignId: item.CampaignId,
                title: item.Title,
                status: item.Status,
                content: item.Content,
                thumbnail: {
                    s3Url: item.MetadataS3Url,
                    name: item.MetadataName
                }
            })) : []
        });
    }

    /**
     * Get the existing campaign by id
     * @param campaignId The id of campaign
     * @returns The promise that resolves the data response
     */
    async getCampaignById(campaignId: number) {
        const campaign = await this._campaignRepository.findOne({ 
            where: {
                campaignId,
                isDeleted: false
            },
            relations: {
                metadata: true
            },
            select: {
                campaignId: true,
                title: true,
                status: true,
                metadata: {
                    s3Url: true,
                    name: true
                }
            }
         });

        if (!campaign) {
            return HttpResponse.notFound('Campaign is not found');
        }

        return HttpResponse.ok({
            campaignId: campaign.campaignId,
            title: campaign.title,
            status: campaign.status,
            content: campaign.content,
            thumnail: {
                s3Url: campaign.metadata.s3Url,
                name: campaign.metadata.name
            }
        });
    }

    /**
     * Create a new campaign
     * @param body The body request
     * @returns The promise that resolves the data response
     */
    async createCampaign (body) {
        const { thumbnail, content, title } = body;

        const metadataCategory = await this._metadataCategoryRepository.findOneBy({ name: MetadataCategoryName.THUMBNAIL });

        const thumbnailBuffer = Buffer.from(thumbnail, 'base64');
        const fileExt = thumbnailBuffer.toString('utf-8').toLowerCase().includes('png') ? 'png' : 'jpeg';
        const fileName = crypto.randomUUID().concat('.', fileExt);

        await S3Util.putObject(config?.s3?.metadataBucketName, `${MetadataCategoryName.THUMBNAIL.toLocaleLowerCase()}/${fileName}`, thumbnailBuffer);

        const newMetadata = await this._metadataRepository.save({
            s3Url: `https://${config?.s3?.metadataBucketName}.s3.amazonaws.com/${MetadataCategoryName.THUMBNAIL.toLocaleLowerCase()}/${fileName}`,
            name: title,
            metadataCategory: metadataCategory,
            createdBy: CLS.getCurrentUser().userId
        });

        const newCampaign = await this._campaignRepository.save({
            title,
            content,
            status: CampaignStatus.OPEN,
            metadata: newMetadata,
            createdBy: CLS.getCurrentUser().userId
        });

        return HttpResponse.created({
            campaignId: newCampaign.campaignId,
            title: newCampaign.title,
            status: newCampaign.status,
            content: newCampaign.content,
            thumnail: {
                s3Url: newMetadata.s3Url,
                name: newMetadata.name
            }
        });
    }

    /**
     * Update the existing campaign
     * @param campaignId The id of campaign
     * @param body The body request
     * @returns The promise that resolves the data response
     */
    async updateCampaign (campaignId: number, body: any) {
        const { thumbnail, content, title, status } = body;
        let newMetadata: Metadata = null;

        const existingCampaign = await this._campaignRepository.findOne({ 
            where: {
                campaignId,
                isDeleted: false
            },
            relations: {
                metadata: true
            }
        });

        if (!existingCampaign) {
            return HttpResponse.badRequest('Invalid campaignId');
        }

        if (thumbnail) {
            const categoryMetadata = await this._metadataCategoryRepository.findOneBy({ name: MetadataCategoryName.THUMBNAIL });

            const thumbnailBuffer = Buffer.from(thumbnail, 'base64');
            const fileExt = thumbnailBuffer.toString('utf-8').toLowerCase().includes('png') ? 'png' : 'jpeg';
            const fileName = crypto.randomUUID().concat('.', fileExt);

            await S3Util.putObject(config?.s3?.metadataBucketName, `${MetadataCategoryName.THUMBNAIL.toLocaleLowerCase()}/${fileName}`, thumbnailBuffer);

            newMetadata = await this._metadataRepository.save({
                s3Url: `https://${config?.s3?.metadataBucketName}.s3.amazonaws.com/${MetadataCategoryName.THUMBNAIL.toLocaleLowerCase()}/${fileName}`,
                name: title,
                categoryMetadata,
                createdBy: CLS.getCurrentUser().userId
            });

            // Soft delete old metadata. No need to await 
            this._metadataRepository.save({ ...existingCampaign.metadata, isDeleted: true, updatedBy: CLS.getCurrentUser().userId });
        }

        const updatedCampaign = await this._campaignRepository.save({
            ...existingCampaign,
            title: title ?? existingCampaign.title,
            content: content ?? existingCampaign.content,
            status: status ?? existingCampaign.status,
            metadata: thumbnail ? newMetadata : existingCampaign.metadata,
            updatedBy: CLS.getCurrentUser().userId
        });

        return HttpResponse.ok({
            campaignId: updatedCampaign.campaignId,
            title: updatedCampaign.title,
            status: updatedCampaign.status,
            content: updatedCampaign.content,
            thumbnail: {
                s3Url: newMetadata ? newMetadata.s3Url : existingCampaign.metadata.s3Url,
                name: newMetadata ? newMetadata.name : existingCampaign.metadata.name
            }
        });
    }

    /**
     * Delete the existing campaign (soft delete)
     * @param campaignId The id of campaign
     * @returns The promise that resolves the data response
     */
    async deleteCampaign(campaignId: number) {
        const existingCampaign = await this._campaignRepository.findOne({
            where: {
                campaignId,
                isDeleted: false
            },
            relations: {
                metadata: true
            }
        });

        if (!existingCampaign || existingCampaign.isDeleted) {
            return HttpResponse.badRequest('Invalid campaignId');
        }
        
        await this._campaignRepository.save({ ...existingCampaign, isDeleted: true, updatedBy: CLS.getCurrentUser().userId });

        // No need to await 
        this._metadataRepository.save({ ...existingCampaign.metadata, isDeleted: true, updatedBy: CLS.getCurrentUser().userId });

        return HttpResponse.ok({
            campaignId
        });
    }
}
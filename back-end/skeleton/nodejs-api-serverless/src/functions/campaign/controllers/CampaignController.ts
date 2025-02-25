import { APIGatewayProxyEvent, Context } from 'aws-lambda';
import HttpResponse from '../../../core/extensions/HttpResponse';
import { CampaignPaging, CreateCampaign, UpdateCampaign } from '../../../schemas/CampaignSchema';
import BaseController from '../../../core/BaseController';
import CampaignService from '../../../services/CampaignService';
import { HttpMethod } from '../../../common/Constant';

/**
 * Metadata Controller
 * @extends {BaseController}
 */
export default class CampaignController extends BaseController {
    static _instance: CampaignController;

    private _campaignService: CampaignService;

    constructor() {
        super();
        this._campaignService = new CampaignService();
    }

    static getInstance() {
        if (!CampaignController._instance) {
            CampaignController._instance = new CampaignController();
        }
       
        return CampaignController._instance;
    }

    /**
     * Executes the appropriate action based on the HTTP method and resource path of the event.
     * @param {APIGatewayProxyEvent} event - The APIGatewayProxyEvent object representing the incoming request.
     * @param {Context} context - The Context object representing the execution context.
     * @returns The response object based on the executed action.
     */
    override async onExecute (event: APIGatewayProxyEvent, context: Context) {
        switch(event.httpMethod) {
            case HttpMethod.GET:
                switch(event.resource) {
                    case `/api/${process.env.API_VERSION}/campaigns`: 
                        return this.getCampaignWithPaging(event);
                    case `/api/${process.env.API_VERSION}/campaigns/{id}`:
                        return this.getCampaignById(event);
                    default:
                        return HttpResponse.notFound('Resource is not found');
                }
            case HttpMethod.POST:
                return this.createCampaign(event);
            case HttpMethod.PUT:
                    return this.updateCampaign(event);
            case HttpMethod.DELETE:
                    return this.deleteCampaign(event);
            default:
                return HttpResponse.notFound('Resource is not found');
        }
    }

    /**
     * Retrieves Campaign with paging
     * 
     * @param {APIGatewayProxyEvent} event - The APIGatewayProxyEvent object containing the request details.
     * @returns A Promise that resolves to a list of campaign
     */
    private async getCampaignWithPaging(event: APIGatewayProxyEvent) {
        const { error } = CampaignPaging.validate(event.queryStringParameters);

        if (error) {
            return HttpResponse.badRequest(null, error.details);
        }

        const pageSize = event.queryStringParameters?.pageSize ? +event.queryStringParameters.pageSize : undefined;
        const pageNumber = event.queryStringParameters?.pageNumber ? +event.queryStringParameters.pageNumber : undefined;
        const status = event.queryStringParameters.status;

        this._logger.info(`Start get campaign with paging. Status: ${status}, PageNumber: ${pageNumber}, PageSize: ${pageSize}`);

        return await this._campaignService.getCampaignWithPaging(status, pageNumber, pageSize);
    }

    /**
     * Retrieves Campaign by id
     * 
     * @param {APIGatewayProxyEvent} event - The APIGatewayProxyEvent object containing the request details.
     * @returns A Promise that resolves a campaign
     */
    private async getCampaignById(event: APIGatewayProxyEvent) {
        const id = event.pathParameters?.id;

        if (isNaN(+id)) {
            return HttpResponse.badRequest('Invalid campaignId');
        }

        this._logger.info(`Start get campaign by id. CampaignId: ${id}`);

        return await this._campaignService.getCampaignById(+id);
    }

    /**
     * Create new Campaign
     * 
     * @param {APIGatewayProxyEvent} event - The APIGatewayProxyEvent object containing the request details.
     * @returns A Promise that resolves the new campaign
     */
    private async createCampaign(event: APIGatewayProxyEvent) {
        const body = JSON.parse(event.body);
        const { error } = CreateCampaign.validate(body);

        if (error) {
            return HttpResponse.badRequest(null, error.details);
        }

        this._logger.info(`Start create new campaign. Campaign title: ${body.title}`);

        return await this._campaignService.createCampaign(body);
    }

    /**
     * Update the existing Campaign
     * 
     * @param {APIGatewayProxyEvent} event - The APIGatewayProxyEvent object containing the request details.
     * @returns A Promise that resolves the campaign is updated
     */
    private async updateCampaign(event: APIGatewayProxyEvent) {
        const campaignId = event.pathParameters?.id ? +event.pathParameters.id : null;
        const body = JSON.parse(event.body);

        if (!campaignId) {
            return HttpResponse.badRequest();
        }

        const { error } = UpdateCampaign.validate(body);

        if (error) {
            return HttpResponse.badRequest(null, error.details);
        }

        this._logger.info(`Start update campaign. CampaignId: ${campaignId}`);

        return await this._campaignService.updateCampaign(campaignId, body);
    }

    /**
     * Update the existing Campaign
     * 
     * @param {APIGatewayProxyEvent} event - The APIGatewayProxyEvent object containing the request details.
     * @returns A Promise that resolves the campaign is deleted
     */
    private async deleteCampaign(event: APIGatewayProxyEvent) {
        const campaignId = event.pathParameters?.id ? +event.pathParameters.id : null;

        if (!campaignId) {
            return HttpResponse.badRequest();
        }

        this._logger.info(`Start delete campaign. CampaignId: ${campaignId}`);
        
        return await this._campaignService.deleteCampaign(campaignId);
    }
}
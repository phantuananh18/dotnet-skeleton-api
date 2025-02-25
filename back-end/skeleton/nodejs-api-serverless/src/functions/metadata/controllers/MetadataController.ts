import { APIGatewayProxyEvent, Context } from 'aws-lambda';
import HttpResponse from '../../../core/extensions/HttpResponse';
import MetadataService from '../../../services/MetadataService';
import BaseController from '../../../core/BaseController';
import * as MetadataSchema from '../../../schemas/MetadataSchema';
import { HttpMethod } from '../../../common/Constant';

/**
 * Metadata Controller
 * @extends {BaseController}
 */
class MetadataController extends BaseController {
    private static _instance: MetadataController;
    private _metadataService: MetadataService;

    constructor() {
        super();
        this._metadataService = new MetadataService();
    }

    /**
     * Get existing controller instance. If it does not exist, create one
     * @returns The controller instance
     */
    static getInstance() {
        if (!MetadataController._instance) {
            MetadataController._instance = new MetadataController();
        }

        return MetadataController._instance;
    }

    /**
     * Executes the appropriate action based on the HTTP method and resource path of the event.
     * @param {APIGatewayProxyEvent} event - The APIGatewayProxyEvent object representing the incoming request.
     * @param {Context} context - The Context object representing the execution context.
     * @returns The response object based on the executed action.
     */
    override async onExecute(event: APIGatewayProxyEvent, context: Context) {
        switch (event.httpMethod) {
            case HttpMethod.GET:
                switch (event.resource) {
                    case `/api/${process.env.API_VERSION}/metadatas`:
                        return this.getMetadatas(event);
                    case `/api/${process.env.API_VERSION}/metadatas/{id}`:
                        return this.getMetadataById(event);
                    default:
                        return HttpResponse.notFound('Resource is not found');
                }
            case HttpMethod.POST:
                return this.addMetadata(event);
            case HttpMethod.PUT:
                switch (event.resource) {
                    case `/api/${process.env.API_VERSION}/metadatas/{id}`:
                        return this.updateMetadata(event);
                    case `/api/${process.env.API_VERSION}/metadatas/order-number`:
                        return this.updateImageOrderNumber(event);
                    default:
                        return HttpResponse.notFound('Resource is not found');
                }
            case HttpMethod.DELETE:
                return this.softDeleteMetadata(event);
            default:
                return HttpResponse.notFound();
        }
    }

    /**
     * Retrieves the metadata for a given event.
     *
     * @param {APIGatewayProxyEvent} event - The APIGatewayProxyEvent object representing the event.
     * @returns A Promise that resolves to the metadata for the event.
     */
    private async getMetadatas(event: APIGatewayProxyEvent) {
        this._logger.info(`Start request to get metadatas: ${JSON.stringify(event.queryStringParameters)}`);

        return await this._metadataService.getMetadatas(event);
    }

    /**
     * Retrieves metadata by its ID.
     * 
     * @param {APIGatewayProxyEvent} event - The APIGatewayProxyEvent object containing the request details.
     * @returns A Promise that resolves to the metadata object.
     */
    private async getMetadataById(event: APIGatewayProxyEvent) {
        this._logger.info(`Start request to get single metadata by Id: ${JSON.stringify(event.pathParameters)}`);

        const metadataId = parseInt(event.pathParameters?.id ?? '0');
        if (!metadataId || metadataId <= 0) {
            return HttpResponse.badRequest();
        }

        return await this._metadataService.getMetadataById(metadataId);
    }

    /**
     * Adds metadata to the system.
     * 
     * @param {APIGatewayProxyEvent} event - The APIGatewayProxyEvent object containing the request details.
     * @returns A Promise that resolves to the result of adding the metadata.
     */
    private async addMetadata(event: APIGatewayProxyEvent) {
        const payload = JSON.parse(event.body);
        const { error } = MetadataSchema.addMetadataSchema.validate(payload);
        if (error) {
            return HttpResponse.badRequest(error.message);
        }

        this._logger.info(`Start request to add metadata. MetadataName: ${payload.metadataName}, FileName: ${payload.fileName}, Hyperlink: ${payload.hyperlink}, IsActive: ${payload.isActive}, CategoryName: ${payload.categoryName}`);
        
        return await this._metadataService.addMetadata(payload.metadataName, payload.fileName, payload.content, payload.hyperlink, payload.isActive, payload.categoryName);
    }

    /**
     * Updates the metadata with the specified ID.
     * 
     * @param {APIGatewayProxyEvent} event - The APIGatewayProxyEvent object containing the request details.
     * @returns A Promise that resolves to the updated metadata.
     */
    private async updateMetadata(event: APIGatewayProxyEvent) {
        const metadataId = parseInt(event.pathParameters?.id ?? '0');
        if (metadataId < 1) {
            return HttpResponse.badRequest();
        }

        const payload = JSON.parse(event.body);
        const { error } = MetadataSchema.updateMetadataSchema.validate(payload);
        if (error) {
            return HttpResponse.badRequest(error.message);
        }

        this._logger.info(`Start request to update metadata. MetadataId: ${metadataId}, Payload: ${JSON.stringify(payload)}`);
        
        return await this._metadataService.updateMetadata(metadataId, payload.metadataName, payload.hyperlink, payload.isActive, payload.categoryName);
    }

    /**
     * Soft deletes metadata by the given ID.
     * 
     * @param {APIGatewayProxyEvent} event - The APIGatewayProxyEvent object.
     * @returns A promise that resolves to the result of the soft delete operation.
     */
    private async softDeleteMetadata(event: APIGatewayProxyEvent) {
        const metadataId = parseInt(event.pathParameters?.id ?? '0');
        if (metadataId < 1) {
            return HttpResponse.badRequest();
        }

        this._logger.info(`Start request to soft delete metadata. MetadataId: ${metadataId}`);
        
        return await this._metadataService.softDeleteMetadata(metadataId);
    }

    /**
     * Updates the order number of an image in the metadata.
     * 
     * @param {APIGatewayProxyEvent} event - The APIGatewayProxyEvent object containing the request details.
     * @returns A Promise that resolves to the result of the updated metadata.
     */
    private async updateImageOrderNumber(event: APIGatewayProxyEvent) {
        const payload = JSON.parse(event.body);
        const { error } = MetadataSchema.updateImageOrderNumberSchema.validate(payload);
        if (error) {
            return HttpResponse.badRequest(error.message);
        }

        this._logger.info(`Start request to update image order number. Payload: ${JSON.stringify(payload)}`);
        
        return await this._metadataService.updateImageOrderNumber(payload);
    }
}

export default MetadataController;
import { APIGatewayProxyEvent } from 'aws-lambda';
import MetadataRepository from '../repositories/MetadataRepository';
import MetadataCategoryRepository from '../repositories/MetadataCategoryRepository';
import HttpResponse from '../core/extensions/HttpResponse';
import S3Util from '../core/aws/S3Util';
import Logger from '../core/extensions/Logger';
import { CLS } from '../core/ContinuationLocalStorage';
import { In } from 'typeorm';

const logger = Logger.getInstance();
const config = require(`../../config/${process.env.NODE_ENV}.json`);
export default class MetadataService {
    private static _instance: MetadataService;
    private _metadataRepository: MetadataRepository;
    private _metadataCategoryRepository: MetadataCategoryRepository;
    private _cls: typeof CLS;
    private _s3Util: typeof S3Util;

    constructor() {
        this._metadataRepository = MetadataRepository.getInstance();
        this._metadataCategoryRepository = MetadataCategoryRepository.getInstance();
        this._s3Util = S3Util;
        this._cls = CLS;
    }

    /**
     * Returns the singleton instance of the MetadataService class.
     * If the instance does not exist, it creates a new instance and returns it.
     * @returns The singleton instance of the MetadataService class.
     */
    static getInstance () {
        if(!MetadataService._instance) {
            MetadataService._instance = new MetadataService();
        }

        return MetadataService._instance;
    }

    /**
     * Retrieves metadata based on the provided event.
     * If the event has a query parameter "category", it retrieves metadata by category.
     * Otherwise, it returns a not found response.
     * 
     * @param {APIGatewayProxyEvent} event - The APIGatewayProxyEvent object representing the incoming request.
     * @returns A Promise that resolves to the retrieved metadata or a not found response.
     */
    async getMetadatas(event: APIGatewayProxyEvent) {
        if (event.queryStringParameters?.category) {
            return await this.getMetadataByCategory(event.queryStringParameters.category);
        }

        return HttpResponse.notFound();
    }

    /**
     * Retrieves metadata by category name.
     * @param {string} categoryName - The name of the category.
     * @returns A promise that resolves to the metadata found for the given category, or an empty array if no metadata is found.
     */
    async getMetadataByCategory(categoryName: string) {
        const metadatas = await this._metadataRepository.findMetadataByCategory(categoryName);
        return HttpResponse.ok(metadatas ?? []);
    }

    /**
     * Retrieves metadata by its ID.
     * @param {number} metadataId - The ID of the metadata to retrieve.
     * @returns A Promise that resolves to the retrieved metadata if found, or an HTTP response indicating that the metadata does not exist.
     */
    async getMetadataById(metadataId: number) {
        const criteria = {
            where: {
                metadataId,
                isDeleted: false
            },
            relations:{
                metadataCategory: true
            }
        };
        const metadata = await this._metadataRepository.find(criteria);
        return metadata
            ? HttpResponse.ok(metadata)
            : HttpResponse.notFound('The metadata does not exist.');
    }

    /**
     * Adds metadata to the AWS S3 and database.
     * 
     * @param {string} metadataName - The name of the metadata.
     * @param {string} fileName - The name of the file.
     * @param {string} content - The content of the metadata. Must be base64 encoded.
     * @param {string} hyperlink - The hyperlink associated with the metadata.
     * @param {Boolean} isActive - Indicates whether the metadata is active or not. Default is true.
     * @param {string} categoryName - The name of the category to which the metadata belongs.
     * @returns A promise that resolves to the added metadata if successful, or an error response if not.
     */
    async addMetadata(metadataName: string, fileName: string, content: string, hyperlink: string, isActive: boolean = true, categoryName: string) {
        let s3Url: string;
        
        // Step 1. Validate category
        const category = await this._metadataCategoryRepository.findOneBy({
            name: categoryName
        });
        if (!category || category.isDeleted) {
            return HttpResponse.notFound('The category does not exist or de-activated. Please choose another.');
        }

        // Step 2. Uploadthe metadata to S3 and generate S3 URL 
        const upload = await this._s3Util.putObject(config?.s3?.metadataBucketName, `${category.name.toLocaleLowerCase()}/${fileName}`, Buffer.from(content, 'base64'));
        if (upload) {
            s3Url = `https://${config?.s3?.metadataBucketName}.s3.amazonaws.com/${category.name.toLocaleLowerCase()}/${fileName}`;
            logger.info(`Upload metadata ${metadataName} to S3 successfully. S3Url: ${s3Url}`);
        }

        if (!s3Url) {
            return HttpResponse.badRequest('Unable to upload this metadata to AWS S3');
        }
        
        // Step 3. Add metadata to database
        const metadata = await this._metadataRepository.save({
            name: metadataName,
            s3Url,
            metadataCategory: category,
            hyperlink,
            isActive,
            createdBy: this._cls.getCurrentUser().userId
        });

        return metadata
            ? HttpResponse.created(metadata)
            : HttpResponse.serverError('Something went wrong. Please try again later.');
    }

    /**
     * Updates the metadata with the specified metadataId.
     * 
     * @param {number} metadataId - The ID of the metadata to update.
     * @param {string} metadataName - The new name for the metadata.
     * @param {string} hyperlink - The new hyperlink for the metadata.
     * @param {boolean} isActive - The new active status for the metadata (default: true).
     * @param {string} categoryName - The name of the category for the metadata.
     * @returns The updated metadata if successful, or an error response if not.
     */
    async updateMetadata(metadataId: number, metadataName: string, hyperlink: string, isActive: boolean = true, categoryName: string) {
        // Step 1. Validate existing metadata
        const metadata = await this._metadataRepository.findOneBy({ metadataId });
        if (!metadata) {
            return HttpResponse.notFound('The metadata does not exist.');
        }

        // Step 2. Validate category
        const category = await this._metadataCategoryRepository.findOneBy({
            name: categoryName
        });
        if (!category || category.isDeleted) {
            return HttpResponse.notFound('The category does not exist or de-activated. Please choose another.');
        }

        // Step 3. Update metadata to the database
        metadata.name = metadataName;
        metadata.metadataCategory = category;
        metadata.hyperlink = hyperlink;
        metadata.isActive = isActive;
        metadata.updatedBy = this._cls.getCurrentUser().userId;

        const updatedMetadata = await this._metadataRepository.save(metadata);

        return updatedMetadata
            ? HttpResponse.ok(updatedMetadata)
            : HttpResponse.serverError('Something went wrong. Please try again later.');
    }

    /**
     * Soft deletes the metadata with the specified metadataId.
     * If the metadata is already marked as soft delete, simply returns success.
     * Otherwise, soft deletes the metadata and returns success if the deletion is successful,
     * or returns an error message if something goes wrong.
     *
     * @param {number} metadataId - The ID of the metadata to be soft deleted.
     * @returns A promise that resolves to an HttpResponse indicating the result of the operation.
     */
    async softDeleteMetadata(metadataId: number) {
        // Step 1. Validate existing metadata
        const metadata = await this._metadataRepository.findOneBy({ metadataId });
        if (!metadata) {
            return HttpResponse.notFound('The metadata does not exist.');
        }

        // Step 2. If the metadata is already marked as soft delete, simply return success to reduce database trip
        if (metadata.isDeleted) {
            return HttpResponse.ok('Success');
        }

        // Step 3. Soft delete the metadata
        metadata.updatedBy = this._cls.getCurrentUser().userId;
        metadata.isDeleted = true;
        const hasDeleted = await this._metadataRepository.save(metadata);

        return hasDeleted
            ? HttpResponse.ok('Success')
            : HttpResponse.serverError('Something went wrong. Please try again later.');
    }

    /**
     * Updates the order number of the metadata items.
     * 
     * @param { Array<{ number, number }> } metadatas - An array of metadata objects containing the metadataId and orderNumber.
     * @returns A Promise that resolves to the updated metadata items or an error response.
     */
    async updateImageOrderNumber(metadatas: { metadataId: number, orderNumber: number }[]) {
        // Step 1. Validate metadataArray
        const metadataArrayValidationResult = await this.validateMetadataArray(metadatas);
        if (!metadataArrayValidationResult.isValid) {
            return metadataArrayValidationResult.response;
        }

        // Step 2. Validate existing metadata
        const existingMetadatas = await this.getMetadatasByIdsOrOrderNumber(metadatas);

        // Check if the number of metadata in the database is the same as the number of metadata in the inputted metadatas
        if (!existingMetadatas || existingMetadatas.length === 0 || metadatas.length !== existingMetadatas.length) {
            return HttpResponse.badRequest('Invalid metadata. Some metadata are not in the "Image" category or have been marked as soft-delete or duplicated.');
        }
        
        // Step 3. Save the order number of the metadata
        const updatedBy = await this._cls.getCurrentUser().userId;
        const targetMetadatas = existingMetadatas.map((metadata, index) => {
            return {
            ...metadata,
            ...metadatas[index],
            updatedBy: updatedBy
            };
        });

        // Separate the metadata into chunks of 10 to avoid overloading the database
        const updateResult = await this._metadataRepository.save(targetMetadatas, {
            chunk: 10,
            transaction: true
        });

        return updateResult
            ? HttpResponse.ok(updateResult)
            : HttpResponse.serverError('Something went wrong. Please try again later.');
    }

    /**
     * Validates an array of metadata objects.
     * 
     * @param { Array<{ number, number }> } metadataArray - The array of metadata objects to validate.
     * @returns An object indicating whether the metadata array is valid and an optional error response.
     */
    private async validateMetadataArray(metadatas: { metadataId: number, orderNumber: number }[]) {
        // Step 1. Validate metadata array length
        if (!metadatas || metadatas.length === 0) {
            return { isValid: false, response: HttpResponse.badRequest('You have submitted an invalid request. Please try again.') };
        }

        // Step 2. Check if the order number is unique
        const duplicateOrderNumbers = new Set(metadatas.map(metadata => metadata.orderNumber));
        if (duplicateOrderNumbers.size !== metadatas.length) {
            return { isValid: false, response: HttpResponse.badRequest('Duplicate order numbers are not allowed.') };
        }

        // Step 3. Check if the metadataId is unique
        const duplicateMetadataIds = new Set(metadatas.map(metadata => metadata.metadataId));
        if (duplicateMetadataIds.size !== metadatas.length) {
            return { isValid: false, response: HttpResponse.badRequest('Duplicate metadata IDs are not allowed.') };
        }

        return { isValid: true, response: null };
    }

    /**
     * Retrieves metadata by their IDs or order numbers.
     * 
     * @param { Array<{ number, number}> } metadatas - An array of objects containing metadata IDs and order numbers.
     * @returns A Promise that resolves to an array of metadata objects.
     */
    private async getMetadatasByIdsOrOrderNumber(metadatas: { metadataId: number, orderNumber: number }[]) {
        return await this._metadataRepository.find({
            select: {
                metadataId: true,
                orderNumber: true,
                metadataCategory: {
                    name: true
                },
                isDeleted: true
            },
            relations: {
                metadataCategory: true
            },
            where: [
                {
                    metadataId: In (metadatas.map(metadata => metadata.metadataId)),
                    metadataCategory: {
                        name: 'Image'
                    },
                    isDeleted: false
                },
                {
                    orderNumber: In(metadatas.map(metadata => metadata.orderNumber)),
                    metadataCategory: {
                        name: 'Image'
                    },
                    isDeleted: false
                }
            ]
        });
    }
}
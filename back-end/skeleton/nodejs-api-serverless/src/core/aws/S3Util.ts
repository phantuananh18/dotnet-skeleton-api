import { S3Client, GetObjectCommand, PutObjectCommand, ObjectCannedACL } from '@aws-sdk/client-s3';
import * as path from 'path';

const client = new S3Client({ region: process.env.AWS_REGION });

/**
 * Class used to interact with AWS S3 Service
 * @static
 */
export default class S3Util {


    /**
     * Retrieves an object from the specified S3 bucket.
     * 
     * @param {string} bucketName - The name of the S3 bucket.
     * @param {string} key - The key of the object to retrieve.
     * @returns {GetObjectCommandOutput} A promise that resolves to the retrieved object or an error if the object is not found.
     */
    public static async getObject(bucketName: string, key: string) {
        const command = new GetObjectCommand({
            Bucket: bucketName,
            Key: key
        });

        return await client.send(command);
    }

    /**
     * Uploads an object to the specified S3 bucket.
     * @param {string} bucketName - The name of the S3 bucket.
     * @param {string} key - The key under which to store the object in the bucket.
     * @param {any} content - The content of the object to upload.
     * @param {ObjectCannedACL} acl - The access controll list.
     * @returns {PutObjectCommandOutput} - A promise that resolves to the result of the upload operation or an error if the upload fails.
     */
    public static async putObject(bucketName: string, key: string, content: any, acl: ObjectCannedACL = 'public-read') {
        const command = new PutObjectCommand({
            Bucket: bucketName,
            Key: key,
            Body: content,
            ACL: acl,
            ContentType: await this.classifyS3ContentType(key)
        });

        return await client.send(command);
    }

    /**
     * Classifies the content type of a given file based on its extension.
     * 
     * @param {string} fileName - The name of the file.
     * @returns The content type of the file.
     */
    private static async classifyS3ContentType (fileName: string) {
        switch (path.extname(fileName)) {
            case '.html':
            case '.htm':
                return 'text/html';
            case '.csv':
                return 'text/csv';
            case '.txt':
                return 'text/plain';
            case '.jpeg':
            case '.jpg':
                return 'image/jpeg';
            case '.png':
                return 'image/png';
            case '.pdf':
                return 'application/pdf';
            case '.json':
                return 'application/json';
            default:
                return '';
        }
    }
}
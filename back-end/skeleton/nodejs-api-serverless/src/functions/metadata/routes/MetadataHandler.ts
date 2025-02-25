import { APIGatewayProxyEvent, Context } from 'aws-lambda';
import LambdaHandler from '../../../core/aws/LambdaHandler';
import MetadataController from '../controllers/MetadataController';

/**
 * Handles the metadata requests for the API Gateway.
 */
class MetadataHandler extends LambdaHandler {
    async execute(event: APIGatewayProxyEvent, context: Context) {
        return await MetadataController.getInstance().execute(event, context);
    }
}

const handler = new MetadataHandler();
export const run = handler.run.bind(handler);
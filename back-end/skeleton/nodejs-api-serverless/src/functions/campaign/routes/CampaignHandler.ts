import { APIGatewayProxyEvent, Context } from 'aws-lambda';
import LambdaHanlder from '../../../core/aws/LambdaHandler';
import CampaignController from '../controllers/CampaignController';

class CampaignHandler extends LambdaHanlder {
    override async execute(event: APIGatewayProxyEvent, context: Context) {
        return await CampaignController.getInstance().execute(event, context);
    }
}

const handler = new CampaignHandler();
export const run = handler.run.bind(handler);

import { APIGatewayProxyEvent, Context } from 'aws-lambda';
import LambdaHandler from '../../../core/aws/LambdaHandler';
import UserController from '../controllers/UserController';

/**
 * User handler
 * @extends {LambdaHandler}
 */
class UserHandler extends LambdaHandler {
    override async execute(event: APIGatewayProxyEvent, context: Context) {
        return await UserController.getInstance().execute(event, context);
    }
}

const handler = new UserHandler();
export const run = handler.run.bind(handler);

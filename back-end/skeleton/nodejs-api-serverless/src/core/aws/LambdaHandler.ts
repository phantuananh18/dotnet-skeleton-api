
import { APIGatewayProxyEvent, Context } from 'aws-lambda';
import Logger from '../extensions/Logger';
import HttpResponse from '../extensions/HttpResponse';
import MySqlDataSource from '../data-sources/MySqlDataSource';

const logger = Logger.getInstance();

/**
 * Base for lambda handler
 */
export default abstract class LambdaHandler {
    /**
     * Excute the main code
     * @param event The Lambda Event object
     * @param context The Lambda Context object
     */
    protected abstract execute(event: APIGatewayProxyEvent, context: Context): Promise<object>;

    /**
     * The entry of lambda function
     * @param event The Lambda Event object
     * @param context The Lambda Context object
     * @returns A lambda response promise
     */
    async run(event: APIGatewayProxyEvent, context: Context) {
        try {
            // Init setup
            await this.init();
            return await this.execute(event, context);
        } catch (err) {
            logger.error('Lambda function has been stopped due to error occurs', err);
            return HttpResponse.serverError();
        }
    }

    /**
     * Load setting(secret manager, init database, ...) before run main code
     */
    private async init() {
        await this.initDatabase();
    }

    /**
     * Init database
     */
    private async initDatabase() {
        const dbInstance = MySqlDataSource.getInstance();
        
        if (!dbInstance.isDBConnected()) {
            await dbInstance.initialize();
            logger.info('Connnect database sucessfully');
            global['datasource'] = dbInstance.getDatasource();
        } else {
            logger.info('Database already connected');
        }
    }
}
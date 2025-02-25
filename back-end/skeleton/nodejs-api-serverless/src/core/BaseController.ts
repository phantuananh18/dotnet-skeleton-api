import { APIGatewayProxyEvent, Context } from 'aws-lambda';
import Logger from './extensions/Logger';
import HttpResponse from './extensions/HttpResponse';
import UserRepository from '../repositories/UserRepository';
import { CLS } from './ContinuationLocalStorage';

/**
 * Base Controller
 * @abstract
 */
export default abstract class BaseController {
    private _userRepository: UserRepository;
    protected _logger: Logger;

    constructor() {
        this._logger = Logger.getInstance();
        this._userRepository = UserRepository.getInstance();
    }

    /**
     * Route the functionality
     * @abstract
     * @param event The Lambda event object
     * @param context The Lambda Context
     */
    protected abstract onExecute(event: APIGatewayProxyEvent, context: Context);

    /**
     * Execute some logic (authorize, ...) before run main code
     * @param event The Lambda event object
     * @param context The Lambda Context
     * @returns 
     */
    async execute(event: APIGatewayProxyEvent, context: Context) {
        let result: any;
        let resolver: any;
        let rejecter: any;
        let currentUser: any;
        const promise: Promise<any> = new Promise((resolve, reject) => {
            resolver = resolve;
            rejecter = reject;
        });

        // No need authorize when run locally
        if (process.env.NODE_ENV == 'local') {
            currentUser = {
                userId: 1
            };

        } else {
            const token = event.headers['Authorization'];

            if (!token) {
                return HttpResponse.unauthorized();
            }

            let claims = event.requestContext?.authorizer?.['claims'];

            if (!claims) {
                this._logger.error('No claims found');
                return HttpResponse.unauthorized('Invalid token');
            }

            if (typeof claims === 'string') {
                claims = JSON.parse(claims);
            }

            currentUser = await this._userRepository.findOneBy({
                email: claims.email
            });
        }
        
        if (!currentUser) {
            this._logger.error('No user found');
            return HttpResponse.unauthorized('Invalid token');
        }

        CLS.setCurrentUser(currentUser, event, async () => {
            try {
                result = await this.onExecute(event, context);
            } catch (err) {
                result = err;
            } finally {
                if (result instanceof Error) {
                    rejecter(result);
                } else {
                    resolver(result);
                }
            }
        });

        return promise;
    }
}

import { APIGatewayProxyEvent, Context } from 'aws-lambda';
import UserService from '../../../services/UserService';
import HttpResponse from '../../../core/extensions/HttpResponse';
import BaseController from '../../../core/BaseController';
import { HttpMethod } from '../../../common/Constant';

/**
 * User Controller
 * @extends {BaseController}
 */
class UserController extends BaseController {
    static _instance: UserController;

    private _userService: UserService;

    constructor() {
        super();
        this._userService = new UserService();
    }

    static getInstance() {
        if (!UserController._instance) {
            UserController._instance = new UserController();
        }
       
        return UserController._instance;
    }

    override async onExecute (event: APIGatewayProxyEvent, context: Context) {
        switch(event.httpMethod) {
            case HttpMethod.GET:
                return this.getUserBySearch(event);
            default:
                return HttpResponse.notFound('Resource is not found');
        }
    }

    private async getUserBySearch(event: APIGatewayProxyEvent) {
        const keyword = event.queryStringParameters?.search ?? '';
        return await this._userService.getUsersWithSearch(keyword);
    }
}

export default UserController;
import UserRepository from '../repositories/UserRepository';
import HttpResponse from '../core/extensions/HttpResponse';


export default class UserService {
    private static _instance: UserService;
    private _userRepository: UserRepository;

    constructor() {
        this._userRepository = UserRepository.getInstance();
    }

    /**
     * Get existing service instance. If it does not exist, create one
     * @returns The repository instance
     */
    static getInstance () {
        if(!UserService._instance) {
            UserService._instance = new UserService();
        }

        return UserService._instance;
    }

    async getUsersWithSearch(keyword: string = '') {
        const users = await this._userRepository.findUserWithSearch(keyword);
        return HttpResponse.ok(users ?? []);
    }
}
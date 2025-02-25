import { APIGatewayProxyEvent } from 'aws-lambda';
import * as  cls from 'cls-hooked';

const NAMESPACE_NAME = 'magictree';
const CURRENT_USER_SESSION = 'currentUser';

/**
 * A class to work with continuation local storage
 */
class ContinuationLocalStorage {
    private _namespace: any;

    /**
     * @constructor
     */
    constructor() {
        if (!this._namespace) {
            this._namespace = cls.createNamespace(NAMESPACE_NAME);
        }
    }

    /**
     * Store a object to the continuation local storage
     * @param key The session key to be store
     * @param value The session value to be store
     * @param event The Lambda event object
     * @param cb The callback function after set data to context
     */
    private _set(key: string, value: any, event: APIGatewayProxyEvent, cb: any): void {
        const namespace = cls.getNamespace(NAMESPACE_NAME);

        // Wrap the events from request and response
        namespace.bind(event);
        namespace.run(() => {
            namespace.set(key, value);
            if (cb) {
                return cb();
            }
        });
    }

    /**
     * Store a user object to the continuation local storage
     * @param user The user object
     * @param event The Lambda event object
     * @param cb The callback function after set data to context
     */
    public setCurrentUser(user: object, event: any, cb: any): void {
        this._set(CURRENT_USER_SESSION, user, event, cb);
    }

    /**
     * Get current user object
     * @returns Current user object
     */
    public getCurrentUser(): any {
        return cls.getNamespace(NAMESPACE_NAME).get(CURRENT_USER_SESSION);
    }
}

const CLS = new ContinuationLocalStorage();

export { CLS };

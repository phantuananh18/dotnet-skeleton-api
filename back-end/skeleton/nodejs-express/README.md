# NodeJS - Express

## Installing Dependencies
1. Make sure you have [NodeJS](https://nodejs.org/en/download/) version >= 20.
2. Run the folowing commands to install all needed dependencies:
   ```bash
   npm install
   ```

## Running Development environment locally
1. Provide the value of key as below in file `.env.development`:

   ```
    NODE_ENV=development                        # Node environment
    PORT=3000                                   # The port the server run
    MYSQL_HOST=***                              # MySQL hostname (omit if using MongoDB)
    MYSQL_PORT=***                              # MySQL port (omit if using MongoDB)
    MYSQL_USERNAME=***                          # MySQL Username (omit if using MongoDB)
    MYSQL_PASSWORD=***                          # MySQL Password (omit if using MongoDB)
    MYSQL_DATABASE=***                          # MySQL Database name (omit if using MongoDB)
    BCRYPT_SALT_ROUND=***                       # Salt round (example: 10)
    JWT_SECRET_KEY=***                          # Secret key is used to generate and verify jwt
    JWT_EXPIRATION_TIME=***                     # Expiration time of jwt (example: 1d, 2h, ...)
    REFRESH_TOKEN_SECRET_KEY=***                # Secret key is used to generate and verify refresh token
    REFRESH_EXPIRATION_TIME=***                 # Expiration time of refresh token (example: 1d, 2h, ...)
    MONGODB_CONNECT_URL=***                     # The url to used to connect to MongoDB (omit if using MySQL)
    NOREPLY_MAIL_HOST=***                       # The host of noreply email
    NOREPLY_MAIL_PORT=***                       # The port of noreply email
    NOREPLY_MAIL_USER=***                       # The user of noreply email
    NOREPLY_MAIL_PASSWORD=***                   # The password of noreply email
    NO_REPLY_EMAIL=***                          # The noreply email address
    RESET_PASSWORD_JWT_SECRET_KEY=***           # Secret key is used to generate and verify jwt that used for reset password 
    RESET_PASSWORD_JWT_EXPIRATION_TIME=***      # Expiration time of token that used for reset password (example: 1d, 2h, ...)
   ```

2. Run the folowing commands to run code:
   ```bash
   npm run dev

   # Run in watch mode
   npm run watch
   ```

## Using MongoDB
Currently, this project is using MySQL. To use MongoDB, import `mongo.config.js` and use method `getMongoRepository` in class BaseRepository.

```js
import MongoDataSource from '../configs/db/mongo.config.js';

export default class BaseRepository {
    constructor (entitySchema) {
        this._dataSouceRepository = MongoDataSource.getInstance().getMongoRepository(entitySchema);
    }

    findOneByPrimaryId(id) {
        const primaryColumn = this.getPrimaryColumnName();
        return this._dataSouceRepository.findOneBy({ [`${primaryColumn}`]: id });
    }
   
    findOneBy(criterias) {
        return this._dataSouceRepository.findOneBy(criterias);
    }

    findOne(criterias) {
        return this._dataSouceRepository.findOne(criterias);
    }

    find(criterias) {
        return this._dataSouceRepository.find(criterias);
    }

    save(entity) {
        return this._dataSouceRepository.save(entity);
    }

    getPrimaryColumnName() {
        return this._dataSouceRepository.metadata.columns.find(item => item.isPrimary).propertyName;
    }
}
```

To know the supported methods for MongoDB, reference to [TypeORM](https://typeorm.io/mongodb#using-mongoentitymanager-and-mongorepository)

## Code Directory Structure
```
/
├── .husky
│   └── pre-commit
├── src
│   ├── common
│   │   └── constant.js
│   ├── configs                             # Config db, ...
│   │   ├── db
│   │   │   ├── mongo.config.js             # Config data source for MongoDB
│   │   │   └── mysql.config.js             # Config data source for MySQL
│   │   └── mailer
│   │       └── mailer.config.js             # Config mailer
│   ├── controllers
│   │   ├── auth.controller.js
│   │   └── user.controller.js
│   ├── entities                            # Database entity
│   │   ├── base.entity.js
│   │   ├── refreshToken.entity.js
│   │   ├── role.entity.js
│   │   └── user.entity.js
│   ├── middlewares                         # Custom middleware
│   │   ├── errorHandler.middleware.js      # Handle error
│   │   ├── morgan.middleware.js            # Log the input of request
│   │   └── verifyToken.middleware.js       # Verify token from incoming request
│   ├── repositories                        # Communication with database
│   │   ├── base.repository.js
│   │   ├── refreshtoken.repository.js
│   │   ├── role.repository.js
│   │   └── user.repository.js
│   ├── routes                              # Route API
│   │   ├── auth.route.js
│   │   ├── index.js
│   │   └── user.route.js
│   ├── schemas                             # Schema use to validate the incoming payload
│   │   ├── auth.schema.js
│   │   └── user.schema.js
│   ├── services                            # Handle logic
│   │   ├── auth.service.js
│   │   ├── email.service.js
│   │   └── user.service.js
│   └── utils                               # Utilities
│       ├── datetime.util.js                # Define some methods for datetime
│       ├── httpResponse.util.js            # Define http response
│       └── logger.util.js                  # Define and custom logger
├── .env.development                        # Credentials and config for Development environment
├── .eslintignore
├── .eslintrc
├── index.js                                # Main entry (setup middleware, route, server, ...)
├── package.json
└── README.md                               # Service description and documentation
```

## Contribution guidelines
* Before committing your changes, you must run Eslint and Unit tests to ensure your new change will pass those checking.
* You are not allowed to commit directly to `master` or `develop` branch, you will need to create new branch with a meaninful name (i.e. using the JIRA ticket created for the change, for an example `DWF-100`)
* Make your changes, and commit the change with meaningful commit message (i.e. using original JIRA ticket KEY: TITLE i.e. `DWF-100: Change database to add new user table` as the commit message, you can add more sub items need - using multiple lines)
* Create new Pull Request to review and merge to `develop` branch. The title of PR will be the same as the first line of commit message (i.e. `DWF-100: Change database to add new user table`).
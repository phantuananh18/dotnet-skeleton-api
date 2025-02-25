import { EntitySchema } from 'typeorm';
import BaseColumnSchemaPart from './base.entity.js';

const RefreshToken = new EntitySchema({
    name: 'RefreshToken',
    tableName: 'RefreshToken',
    columns: {
        refreshTokenId: {
            primary: true,
            type: 'int',
            generated: 'increment',
            name: 'RefreshTokenId'
        },
        userId: {
            type: 'int',
            name: 'UserId'
        },
        token: {
            type: 'varchar',
            name: 'Token'
        },
        ...BaseColumnSchemaPart
    }
});

export default RefreshToken;
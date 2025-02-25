import { EntitySchema } from 'typeorm';
import BaseColumnSchemaPart from './base.entity.js';
import Role from './role.entity.js';

const User = new EntitySchema({
    name: 'user',
    tableName: 'User',
    columns: {
        userId: {
            primary: true,
            type: 'int',
            generated: 'increment',
            name: 'UserId'
        },
        username: {
            type: 'varchar',
            name: 'Username'
        },
        password: {
            type: 'varchar',
            name: 'Password'
        },
        email: {
            type: 'varchar',
            name: 'Email'
        },
        firstName: {
            type: 'varchar',
            name: 'FirstName'
        },
        lastName: {
            type: 'varchar',
            name: 'LastName'
        },
        mobilePhone: {
            type: 'varchar',
            name: 'MobilePhone'
        },
        department: {
            type: 'varchar',
            name: 'Department'
        },
        jobTitle: {
            type: 'varchar',
            name: 'JobTitle'
        },
        ...BaseColumnSchemaPart
    },
    relations: {
        role: {
            type: 'one-to-one',
            target: Role,
            joinColumn: {
                name: 'roleId',
                referencedColumnName: 'roleId'
            },
            cascade: false
        }
    }
});

export default User;
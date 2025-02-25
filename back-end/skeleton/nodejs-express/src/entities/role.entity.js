import { EntitySchema } from 'typeorm';
import BaseColumnSchemaPart from './base.entity.js';

const Role = new EntitySchema({
    name: 'role',
    tableName: 'Role',
    columns: {
        roleId: {
            primary: true,
            type: 'int',
            generated: 'increment',
            name: 'RoleId'
        },
        name: {
            type: 'varchar',
            name: 'Name'
        },
        ...BaseColumnSchemaPart
    }
});

export default Role;
import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';
import BaseEntity from './Base';

@Entity('Role')
export default class Role extends BaseEntity {
    @PrimaryGeneratedColumn({ name: 'RoleId' })
    roleId: number;

    @Column({ name: 'Name' })
    name: string;

    @Column({ name: 'Description' })
    description?: string;
}
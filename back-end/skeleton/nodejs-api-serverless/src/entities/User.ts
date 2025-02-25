import { Entity, PrimaryGeneratedColumn, Column, OneToOne, JoinColumn } from 'typeorm';
import BaseEntity from './Base';
import Role from './Role';

@Entity('User')
export default class User extends BaseEntity {
    @PrimaryGeneratedColumn({ name: 'UserId' })
    userId: number;

    @Column({ name: 'Username' })
    username: string;

    @Column({ name: 'Password' })
    password: string;

    @Column({ name: 'Email' })
    email: string;

    @Column({ name: 'Department' })
    department?: string;

    @Column({ name: 'JobTitle' })
    jobTitle?: string;

    @Column({ name: 'MobilePhone' })
    mobilePhone?: string;

    @OneToOne(() => Role)
    @JoinColumn({ name: 'RoleId' })
    role?: Role;
}
import { Column, CreateDateColumn, UpdateDateColumn } from 'typeorm';

export default abstract class BaseColumns {
    @UpdateDateColumn({ name: 'UpdatedDate', type: 'datetime' })
    updatedDate?: string;

    @CreateDateColumn({ name: 'CreatedDate', type: 'datetime' })
    createdDate: string;

    @Column({ name: 'UpdatedBy', type: 'int' })
    updatedBy?: number;

    @Column({ name: 'CreatedBy', type: 'int' })
    createdBy?: number;

    @Column({ name: 'IsDeleted', type: 'boolean' })
    isDeleted: boolean;
}
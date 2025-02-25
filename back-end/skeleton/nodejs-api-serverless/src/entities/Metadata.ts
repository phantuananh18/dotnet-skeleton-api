import { Entity, PrimaryGeneratedColumn, Column, OneToOne, JoinColumn } from 'typeorm';
import BaseEntity from './Base';
import MetadataCategory from './MetadataCategory';

@Entity('Metadata')
export default class Metadata extends BaseEntity {
    @PrimaryGeneratedColumn({ name: 'MetadataId' })
    metadataId: number;

    @Column({ name: 'Name' })
    name: string;

    @Column({ name: 'S3Url'})
    s3Url?: string;

    @Column({ name: 'Hyperlink'})
    hyperlink?: string;

    @Column({ name: 'IsActive', default: 0 })
    isActive: boolean;

    @Column({ name: 'OrderNumber'})
    orderNumber: number;

    @OneToOne(() => MetadataCategory)
    @JoinColumn({ name: 'MetadataCategoryId'})
    metadataCategory?: MetadataCategory;
}
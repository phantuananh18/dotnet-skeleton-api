import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';
import BaseEntity from './Base';

@Entity('MetadataCategory')
export default class MetadataCategory extends BaseEntity {
    @PrimaryGeneratedColumn({ name: 'MetadataCategoryId' })
    MetadataCategoryId: number;

    @Column({ name: 'Name' })
    name: string;
}
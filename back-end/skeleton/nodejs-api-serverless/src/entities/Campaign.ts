import { Entity, PrimaryGeneratedColumn, Column, OneToOne, JoinColumn } from 'typeorm';
import BaseEntity from './Base';
import Metadata from './Metadata';

@Entity('Campaign')
export default class Campaign extends BaseEntity {
    @PrimaryGeneratedColumn({ name: 'CampaignId' })
    campaignId: number;

    @Column({ name: 'Title' })
    title: string;

    @Column({ name: 'Content' })
    content: string;

    @Column({ name: 'Status' })
    status: CampaignStatus;

    @OneToOne(() => Metadata)
    @JoinColumn({ name: 'MetadataId' })
    metadata: Metadata;
}

export enum CampaignStatus {
    OPEN = 'Open',
    CLOSED = 'Closed'
}
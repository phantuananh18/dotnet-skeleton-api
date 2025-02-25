import Joi from 'joi';
import { CampaignStatus } from '../entities/Campaign';

export const CampaignPaging = Joi.object({
   pageSize: Joi.number().min(1),
   pageNumber: Joi.number().min(1),
   status: Joi.string().valid(...Object.values(CampaignStatus)).required()
}).unknown(true);

export const CreateCampaign = Joi.object({
   title: Joi.string().required(),
   content: Joi.string().required(),
   thumbnail: Joi.string().required()
}).unknown(true);

export const UpdateCampaign = Joi.object({
   title: Joi.string(),
   content: Joi.string(),
   thumbnail: Joi.string(),
   status: Joi.string().valid(...Object.values(CampaignStatus))
}).unknown(true);


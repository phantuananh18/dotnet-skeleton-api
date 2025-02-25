import Joi from 'joi';
import { RoleType } from '../common/constant.js';

const signUpSchema = Joi.object({
    firstName: Joi.string(),
    lastName: Joi.string(),
    username: Joi.string().min(6).required(),
    password: Joi.string().min(8).required(),
    email: Joi.string().email().required(),
    mobilePhone: Joi.string().min(9).pattern(new RegExp('[0-9]')),
    department: Joi.string(),
    jobTitle: Joi.string(),
    role: Joi.string().valid(...Object.values(RoleType))
}).unknown(true);

export {
    signUpSchema
};
import Joi from 'joi';
import { RegexPattern } from '../common/constant.js';

const signInSchema = Joi.object({
    username: Joi.string().min(5).max(30).required(),
    password: Joi.string().pattern(new RegExp(RegexPattern.PASSWORD)).required()
});

const refreshTokenSchema = Joi.object({
    refreshToken: [
        Joi.string(),
        Joi.number()
    ]
});

const resetPasswordSchema = Joi.object({
    password: Joi.string().min(8).required(),
    passwordConfirmation: Joi.any().valid(Joi.ref('password')).required().messages({
        'any.only': 'Must match password field'
    })
}).unknown(true);

export {
    signInSchema,
    refreshTokenSchema,
    resetPasswordSchema
};
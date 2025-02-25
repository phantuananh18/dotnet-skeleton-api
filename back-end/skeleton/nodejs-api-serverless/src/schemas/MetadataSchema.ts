import Joi from 'joi';

const addMetadataSchema = Joi.object({
    metadataName: Joi.string().required(),
    fileName: Joi.string().required().pattern(new RegExp('.+?\\.(png|jpg|jpeg)$')),
    hyperlink: Joi.string(),
    categoryName: Joi.string().required(),
    content: Joi.string().base64().required()
}).unknown(true);

const updateMetadataSchema = Joi.object({
    metadataName: Joi.string(),
    hyperlink: Joi.string(),
    categoryName: Joi.string(),
    isActive: Joi.bool()
}).unknown(true);

const updateImageOrderNumberSchema = Joi.array().items(
    Joi.object({
        metadataId: Joi.number().required(),
        orderNumber: Joi.number().required()
    })
).required();

export {
    addMetadataSchema,
    updateMetadataSchema,
    updateImageOrderNumberSchema
};
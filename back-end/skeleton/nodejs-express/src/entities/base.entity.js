const BaseColumnSchemaPart = {
    // _id: {                   // Primary column is used for MongoDB
    //     objectId: true,
    //     name: '_id'
    // },
    createdDate: {
        name: 'CreatedDate',
        type: 'datetime',
        createDate: true
    },
    updatedDate: {
        name: 'UpdatedDate',
        type: 'datetime',
        updateDate: true
    },
    createdBy: {
        name: 'CreatedBy',
        type: 'int'
    },
    updatedBy: {
        name: 'UpdatedBy',
        type: 'int'
    },
    isDeleted: {
        name: 'IsDeleted',
        type: 'tinyint',
        default: 0
    }
};

export default BaseColumnSchemaPart;
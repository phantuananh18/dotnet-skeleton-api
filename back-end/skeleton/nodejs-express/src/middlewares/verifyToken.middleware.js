import jwt from 'jsonwebtoken';

import HttpResponse from '../utils/httpResponse.util.js';
import UserRepository from '../repositories/user.repository.js';

export const verifyToken = (requiredRoles) => (req, res, next) => {
    const userRepository = UserRepository.getInstance();
    if (req.headers && req.headers.authorization) {
        const [prefix, token] = req.headers.authorization.split(' ');

        if (prefix === 'Bearer') {
            jwt.verify(token, process.env.JWT_SECRET_KEY, async (err, decoded) => {
                if (err) {
                    const response = HttpResponse.unauthorized('Invalid token');
                    return res.status(response.status).json(response);
                }
                
                const existingUser = await userRepository.findUserAndRelatedDataByCriteria('userId', decoded.id);
                if (!existingUser || existingUser.isDeleted) {
                    const response = HttpResponse.forbidden();
                    return res.status(response.status).json(response);
                }

                if (!requiredRoles.includes(existingUser.role.name)) {
                    const response = HttpResponse.forbidden();
                    return res.status(response.status).json(response);
                }
    
                req.currentUser = existingUser;
                next();
            });
            
        } else {
            const response = HttpResponse.unauthorized('Invalid token');
            res.status(response.status).json(response);
        }
    } else {
        const response = HttpResponse.unauthorized('Token is required');
        res.status(response.status).json(response);
    }
};
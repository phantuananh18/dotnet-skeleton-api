import express from 'express';
import { getUsersWithSearch, createUser, updateUser, deleteUser, getUserByUserId } from '../controllers/user.controller.js';
import { verifyToken } from '../middlewares/verifyToken.middleware.js';
import * as Constant from '../common/constant.js';

const router = express.Router({ mergeParams: true });

router.get('/', verifyToken([Constant.RoleType.ADMIN, Constant.RoleType.SYSTEM]), getUsersWithSearch);
router.get('/:id', verifyToken([Constant.RoleType.ADMIN, Constant.RoleType.SYSTEM]), getUserByUserId);
router.post('/', verifyToken([Constant.RoleType.ADMIN, Constant.RoleType.SYSTEM]), createUser);
router.put('/:id', verifyToken([Constant.RoleType.ADMIN, Constant.RoleType.SYSTEM]), updateUser);
router.delete('/:id',verifyToken([Constant.RoleType.ADMIN, Constant.RoleType.SYSTEM]), deleteUser);

export default router;
import express from 'express';
import authRouter from './auth.route.js';
import userRouter from './user.route.js';
import pingRouter from './ping.route.js';

const router = express.Router();

router.use('/user', userRouter);
router.use('/auth', authRouter);
router.use('/ping', pingRouter);

export default router;
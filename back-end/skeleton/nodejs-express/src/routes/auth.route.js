import express from 'express';
import { signUp, signIn, generateAccessTokenFromRefreshToken, forgotPassword, resetPassword } from '../controllers/auth.controller.js';

const router = express.Router({ mergeParams: true });

router.post('/sign-up', signUp);
router.post('/sign-in', signIn);
router.post('/refresh-token', generateAccessTokenFromRefreshToken);

// Forgot password
router.post('/forgot-password/:email', forgotPassword);
router.post('/reset-password', resetPassword);

export default router;


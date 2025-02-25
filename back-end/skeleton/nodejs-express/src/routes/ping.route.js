import express from 'express';
import { ping } from '../controllers/ping.controller.js';

const router = express.Router({ mergeParams: true });

router.get('/', ping);

export default router;
/**
 * Ping the server.
 * 
 * @returns TRetrieves a response indicating that the server is alive. 
 */
async function ping(req, res, next) {
    try {
        return res.status(200).json({ message: 'Pong' });
    }
    catch(err) {
        next(err);
    }
}

export {
    ping
};
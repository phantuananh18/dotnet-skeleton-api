export default (req, res, next) => {
    const originalJson = res.json;
    // Override the json function
    res.json = function (body) {
        // Translate the message
        const modifyBodyResponse = { ...body, message: body.message && req.t(body.message) };
        originalJson.call(this, modifyBodyResponse);
    };
 
    next();
};